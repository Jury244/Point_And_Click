using UnityEngine;

public class FloatMotion : MonoBehaviour
{
    public float moveAmplitude = 0.1f;
    public float moveSpeed = 1f;

    public float rotationAmplitude = 5f;
    public float rotationSpeed = 0.8f;

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        float yOffset =
            Mathf.Sin(Time.time * moveSpeed) * moveAmplitude;

        transform.position =
            startPosition + new Vector3(0f, yOffset, 0f);

        float zRotation =
            Mathf.Sin(Time.time * rotationSpeed) * rotationAmplitude;

        transform.rotation =
            startRotation * Quaternion.Euler(0f, 0f, zRotation);
    }
    public void SetNewStartPosition()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }
}