using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private Vector3 originalLocalPos;

    void Awake()
    {
        Instance = this;
        originalLocalPos = transform.localPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float t = 0f;

        while (t < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalLocalPos + new Vector3(x, y, 0);

            t += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalLocalPos;

        if (Camera.main != null)
        {
            SimpleCameraStates camState = Camera.main.GetComponent<SimpleCameraStates>();
            if (camState != null)
                camState.ReapplyCurrentState();
        }
    }
}