using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Reference to the player character
    public Transform player;
    // Offset between the camera and the player character
    public Vector3 offset;
    // Smoothness of the camera movement
    public float smoothness = 0.5f;

    void LateUpdate()
    {
        // Calculate the desired position for the camera
        Vector3 desiredPosition = (Vector3)player.position + offset;
        // Smoothly move the camera towards the desired position
        transform.position = Vector2.Lerp(transform.position, desiredPosition, smoothness);
    }
}