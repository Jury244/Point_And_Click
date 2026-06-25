using UnityEngine;
using System.Collections;

public class IchthyosaurMove : MonoBehaviour
{
    [Header("Movement")]
    public Transform target;
    public float speed = 2f;

    [Header("References")]
    public Ichthyosaur ichthyosaur;
    public FloatMotion floatMotion;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip swimSound;

    private bool isMoving = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartMove();
        }
    }

    public void StartMove()
    {
        if (isMoving)
            return;

        if (target == null)
        {
            Debug.LogError("TARGET není pøiøazen!");
            return;
        }

        if (audioSource != null && swimSound != null)
        {
            audioSource.PlayOneShot(swimSound);
        }

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        isMoving = true;

        if (floatMotion != null)
            floatMotion.enabled = false;

        while (Vector3.Distance(transform.position, target.position) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                speed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = target.position;

        Arrived();
    }

    void Arrived()
    {
        isMoving = false;

        Debug.Log("ICHTHYOSAUR ARRIVED");

        if (floatMotion != null)
        {
            floatMotion.enabled = true;
            floatMotion.SetNewStartPosition();
        }

        if (ichthyosaur != null)
        {
            ichthyosaur.Attack();
        }
    }
}