using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public ParticleSystem blastParticles;

    private bool isFiring = false;

    public void Fire()
    {
        if (isFiring) return;

        isFiring = true;

        Debug.Log("CANNON FIRE!");

        animator.SetTrigger("BlastTrigger");

        if (blastParticles != null)
        {
            blastParticles.Play();
        }
    }

    public void BackToIdle()
    {
        isFiring = false;
    }
}