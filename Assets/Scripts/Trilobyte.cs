using UnityEngine;
using System.Collections;

public class Trilobyte : MonoBehaviour
{
    [Header("References")]
    public Animator animator;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip rollSound;
    public AudioClip chargeSound;
    public AudioClip timeoutUnrollSound;

    [Header("Bubble Event")]
    public ParticleSystem bubbles1;
    public ParticleSystem bubbles2;
    public AudioClip bubblesSound;

    [Header("Settings")]
    public float ballDuration = 2f;

    private bool isRolling = false;
    private bool isBall = false;
    private bool isCharged = false;

    private Coroutine timerRoutine;

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
        if (!isRolling && !isBall && !isCharged)
        {
            isRolling = true;

            if (audioSource != null && rollSound != null)
                audioSource.PlayOneShot(rollSound);

            animator.SetTrigger("RollTrigger");
            return;
        }

        if (isBall && !isCharged)
        {
            ChargeToCannon();
        }
    }

    public void EnterBall()
    {
        isRolling = false;
        isBall = true;

        if (timerRoutine != null)
            StopCoroutine(timerRoutine);

        timerRoutine = StartCoroutine(BallTimer());
    }

    IEnumerator BallTimer()
    {
        float t = 0f;

        while (t < ballDuration)
        {
            if (isCharged)
                yield break;

            t += Time.deltaTime;
            yield return null;
        }

        TriggerUnroll();
    }

    void TriggerUnroll()
    {
        if (isCharged)
            return;

        isBall = false;

        if (audioSource != null && timeoutUnrollSound != null)
            audioSource.PlayOneShot(timeoutUnrollSound);

        animator.SetTrigger("UnrollTrigger");
    }

    public void BackToIdle()
    {
        isRolling = false;
        isBall = false;
        isCharged = false;
    }

    void ChargeToCannon()
    {
        Debug.Log("TRILOBYTE CHARGED");

        isCharged = true;

        if (audioSource != null && chargeSound != null)
            audioSource.PlayOneShot(chargeSound);

        if (timerRoutine != null)
            StopCoroutine(timerRoutine);

        transform.position = new Vector3(4.46f, -2.41f, -1.71f);
    }

    public bool IsCharged()
    {
        return isCharged;
    }

    public void Consume()
    {
        gameObject.SetActive(false);
    }

    public void BubbleEvent()
    {
        if (bubbles1 != null)
            bubbles1.Play();

        if (bubbles2 != null)
            bubbles2.Play();

        if (audioSource != null && bubblesSound != null)
            audioSource.PlayOneShot(bubblesSound);
    }
}