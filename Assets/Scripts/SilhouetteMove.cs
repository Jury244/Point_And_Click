using UnityEngine;
using System.Collections;

public class SilhouetteMove : MonoBehaviour
{
    [Header("Movement")]
    public Transform target;
    public float speed = 2f;

    [Header("Animation")]
    public Animator animator;

    [Header("UI")]
    public GameObject startButton;

    [Header("Fish Trigger")]
    public MemeFish fish;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip swimSound;

    private bool isMoving = false;
    private bool hasStarted = false;

    public void OnStartButton()
    {
        if (hasStarted) return;

        hasStarted = true;

        if (audioSource != null && swimSound != null)
            audioSource.PlayOneShot(swimSound);

        if (startButton != null)
            startButton.SetActive(false);

        StartMove();
    }

    public void StartMove()
    {
        if (isMoving) return;

        isMoving = true;

        if (animator != null)
            animator.SetTrigger("StartMove");

        StartCoroutine(MoveToTarget());
    }

    IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(transform.position, target.position) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                speed * Time.deltaTime
            );

            yield return null;
        }

        Arrived();
    }

    void Arrived()
    {
        isMoving = false;

        if (animator != null)
            animator.SetTrigger("Idle");

        if (fish != null)
            fish.Wake();
    }
}