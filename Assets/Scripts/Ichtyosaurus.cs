using UnityEngine;

public class Ichthyosaur : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public Collider2D col;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip bubblesSound;

    [Header("VFX")]
    public ParticleSystem bubblesParticles1;
    public ParticleSystem bubblesParticles2;

    private bool isAttacking = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Attack();
            }
        }
    }

    public void Attack()
    {
        if (isAttacking)
            return;

        isAttacking = true;

        if (audioSource != null && attackSound != null)
            audioSource.PlayOneShot(attackSound);

        animator.SetTrigger("AttackTrigger");
    }

    public void BackToIdle()
    {
        isAttacking = false;
    }

    public void ShakeCamera()
    {
        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.Shake(0.15f, 0.15f);
        }
    }

    public void BubbleEvent()
    {
        if (bubblesParticles1 != null)
            bubblesParticles1.Play();

        if (bubblesParticles2 != null)
            bubblesParticles2.Play();

        if (audioSource != null && bubblesSound != null)
            audioSource.PlayOneShot(bubblesSound);
    }
}