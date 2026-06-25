using UnityEngine;
using System.Collections;

public class SimpleCameraStates : MonoBehaviour
{
    [Header("Camera")]
    public Camera cam;

    [System.Serializable]
    public class CameraState
    {
        public Vector3 position;
        public float size;
        public Vector3 rotation;
    }

    [Header("States")]
    public CameraState introState;
    public CameraState gameState;

    [Header("Transition")]
    public float transitionTime = 2f;

    private bool isIntro = true;
    private bool isTransitioning = false;

    private Vector3 basePos;
    private Quaternion baseRot;

    void Start()
    {
        ApplyStateInstant(introState);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTransitioning)
        {
            ToggleCamera();
        }
    }

    public void ToggleCamera()
    {
        if (isIntro)
            StartCoroutine(SwitchCamera(gameState));
        else
            StartCoroutine(SwitchCamera(introState));

        isIntro = !isIntro;
    }

    public void SwitchToGameFromCutscene()
    {
        if (isTransitioning) return;

        isIntro = false;
        StartCoroutine(SwitchCamera(gameState));
    }

    public void SwitchToIntroFromCutscene()
    {
        if (isTransitioning) return;

        isIntro = true;
        StartCoroutine(SwitchCamera(introState));
    }
    IEnumerator SwitchCamera(CameraState target)
    {
        isTransitioning = true;

        Vector3 startPos = cam.transform.position;
        float startSize = cam.orthographicSize;
        Quaternion startRot = cam.transform.rotation;

        Vector3 endPos = new Vector3(target.position.x, target.position.y, cam.transform.position.z);
        float endSize = target.size;
        Quaternion endRot = Quaternion.Euler(target.rotation);

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / transitionTime;

            cam.transform.position = Vector3.Lerp(startPos, endPos, t);
            cam.orthographicSize = Mathf.Lerp(startSize, endSize, t);
            cam.transform.rotation = Quaternion.Lerp(startRot, endRot, t);

            yield return null;
        }

        ApplyStateInstant(target);
        isTransitioning = false;
    }

    void ApplyStateInstant(CameraState state)
    {
        basePos = new Vector3(state.position.x, state.position.y, cam.transform.position.z);
        baseRot = Quaternion.Euler(state.rotation);

        cam.transform.position = basePos;
        cam.orthographicSize = state.size;
        cam.transform.rotation = baseRot;
    }

    public void ReapplyCurrentState()
    {
        if (isIntro)
            ApplyStateInstant(introState);
        else
            ApplyStateInstant(gameState);
    }
}