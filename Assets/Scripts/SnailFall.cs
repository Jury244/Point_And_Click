using UnityEngine;

public class SnailFall : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Collider2D col;

    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip snail1;
    public AudioClip snail2;
    public AudioClip snail3;
    public AudioClip snail4;
    public AudioClip snail5;

    public AudioClip stretch;
    public AudioClip bubblesSound;

    [Header("Particles")]
    public ParticleSystem bubblesParticles;

    [Header("Octopus Event")]
    public Octopus octopus;
    public Transform octopusTarget;

    private bool hasFallen = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Fall();
            }
        }
    }

    void Fall()
    {
        if (hasFallen) return;

        hasFallen = true;

        col.enabled = false;

        animator.SetTrigger("FallTrigger");
    }

    public void Snail1()
    {
        if (audioSource != null && snail1 != null)
            audioSource.PlayOneShot(snail1);
    }

    public void Snail2()
    {
        if (audioSource != null && snail2 != null)
            audioSource.PlayOneShot(snail2);
    }

    public void Snail3()
    {
        if (audioSource != null && snail3 != null)
            audioSource.PlayOneShot(snail3);
    }

    public void Snail4()
    {
        if (audioSource != null && snail4 != null)
            audioSource.PlayOneShot(snail4);
    }

    public void Snail5()
    {
        if (audioSource != null && snail5 != null)
            audioSource.PlayOneShot(snail5);
    }

    public void Stretch()
    {
        if (audioSource != null && stretch != null)
            audioSource.PlayOneShot(stretch);
    }
    public void BubbleEvent()
    {
        if (bubblesParticles != null)
            bubblesParticles.Play();

        if (audioSource != null && bubblesSound != null)
            audioSource.PlayOneShot(bubblesSound);
    }


    public void MoveOctopus()
    {
        if (octopus != null && octopusTarget != null)
        {
            octopus.transform.position = octopusTarget.position;
        }
    }

    public void Die()
    {
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
    }
}