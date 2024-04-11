using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public Vector3 offset; // Offset from the player's position

    void Update()
    {
        if (playerTransform != null)
        {
            // Calculate the desired position for this object
            Vector3 targetPosition = playerTransform.position + offset;

            // Smoothly move this object towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}
