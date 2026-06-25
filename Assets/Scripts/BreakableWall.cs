using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [Header("References")]
    public Animator animator;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip breakSound;

    private bool isDestroyed = false;

    public bool IsOpened
    {
        get { return isDestroyed; }
    }

    public void DestroyWall()
    {
        if (isDestroyed) return;

        isDestroyed = true;

        animator.SetTrigger("DestroyTrigger");
    }

    public void PlayBreakSound()
    {
        if (audioSource != null && breakSound != null)
        {
            audioSource.PlayOneShot(breakSound);
        }
    }
}