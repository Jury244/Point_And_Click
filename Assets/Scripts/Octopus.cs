using UnityEngine;

public class Octopus : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public ParticleSystem bubbleParticles;
    public Cannon cannon;
    public Trilobyte trilobyte;
    public BreakableWall wall;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip emergeSound;
    public AudioClip squishSound;
    public AudioClip cannonSound;  

    private bool isHidden = true;
    private bool isComingOut = false;
    private bool isIdle = false;
    private bool isSqueezing = false;

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
        if (isHidden)
        {
            isHidden = false;
            isComingOut = true;

            animator.SetTrigger("OutTrigger");
            return;
        }

        if (isIdle && !isSqueezing)
        {
            isIdle = false;
            isSqueezing = true;

            animator.SetTrigger("SqueezeTrigger");
        }
    }

    public void PlayEmergeSound()
    {
        if (audioSource != null && emergeSound != null)
            audioSource.PlayOneShot(emergeSound);
    }

    public void PlaySquishSound()
    {
        if (audioSource != null && squishSound != null)
            audioSource.PlayOneShot(squishSound);
    }

    public void PlayCannonSound()
    {
        if (audioSource != null && cannonSound != null)
            audioSource.PlayOneShot(cannonSound);
    }

    public void EnterIdle()
    {
        isComingOut = false;
        isIdle = true;
    }

    public void FinishSqueeze()
    {
        isSqueezing = false;
        isIdle = true;
    }

    public void PlayBubbles()
    {
        if (bubbleParticles != null)
            bubbleParticles.Play();

        if (cannon != null)
            cannon.Fire();

        if (trilobyte != null &&
            wall != null &&
            trilobyte.IsCharged())
        {
            trilobyte.Consume();
            wall.DestroyWall();
        }
    }

    public void FireCannonEvent()
    {
        if (cannon != null)
            cannon.Fire();
    }
}