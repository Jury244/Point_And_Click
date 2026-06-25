using UnityEngine;

public class TeleportOnEnter : MonoBehaviour
{
    [Header("Target Position")]
    public Vector3 targetPosition = Vector3.zero;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            transform.position = targetPosition;

            Debug.Log("Teleported to: " + targetPosition);
        }
    }
}