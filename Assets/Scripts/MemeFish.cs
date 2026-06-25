using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.SceneManagement;
using System.Collections;

public class MemeFish : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public BreakableWall wall;

    [Header("Cutscene References")]
    public IchthyosaurMove ichthyosaurMove;

    [Header("Camera")]
    public SimpleCameraStates cameraController;

    [Header("Spline")]
    public SplineContainer path;
    public float duration = 3f;

    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip huhSound;
    public AudioClip bubbleSound;
    public AudioClip dramaSound;
    public AudioClip swimLoop;

    [Header("Game Flow")]
    public GameFlowManager gameFlow;

    private bool isTouching = false;
    private bool isLeaving = false;
    private bool isSwimming = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                OnClick();
            }
        }
    }

    void OnClick()
    {
        if (isLeaving) return;

        if (wall != null && wall.IsOpened)
        {
            StartCoroutine(FollowSpline());
            return;
        }

        Touch();
    }

    void Touch()
    {
        if (isTouching) return;

        isTouching = true;
        animator.SetTrigger("TouchTrigger");
    }

    public void BackToIdle()
    {
        isTouching = false;
    }

    public void Wake()
    {
        Debug.Log("WAKE TRIGGER RECEIVED");

        if (animator != null)
            animator.SetTrigger("WakeTrigger");

        if (ichthyosaurMove != null)
            ichthyosaurMove.StartMove();
    }

    public void CameraChange()
    {
        Debug.Log("CAMERA EVENT RECEIVED");

        if (cameraController != null)
            cameraController.SwitchToGameFromCutscene();
    }

    IEnumerator FollowSpline()
    {
        isLeaving = true;

        animator.SetTrigger("SwimTrigger");

        StartSwimLoop();

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = path.EvaluatePosition(t);
            yield return null;
        }

        StopSwimLoop();

        if (gameFlow != null)
        {
            gameFlow.RestartGame();
        }
        else
        {
            Debug.LogWarning("GameFlowManager není pøiøazen!");
        }

        gameObject.SetActive(false);
    }
    public void HuhSound()
    {
        if (audioSource != null && huhSound != null)
            audioSource.PlayOneShot(huhSound);
    }

    public void BubbleSound()
    {
        if (audioSource != null && bubbleSound != null)
            audioSource.PlayOneShot(bubbleSound);
    }

    public void DramaSound()
    {
        if (audioSource != null && dramaSound != null)
            audioSource.PlayOneShot(dramaSound);
    }
    public void StartSwimLoop()
    {
        if (audioSource == null || swimLoop == null) return;
        if (isSwimming) return;

        isSwimming = true;
        audioSource.clip = swimLoop;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopSwimLoop()
    {
        if (!isSwimming) return;

        isSwimming = false;
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = null;
    }
}