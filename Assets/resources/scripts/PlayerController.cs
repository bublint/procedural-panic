using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement speed of the player character
    public float speed = 10f;
    // Maximum speed the player character can reach
    public float maxSpeed = 5f;
    // Drag applied to the player character when no keys are being pressed
    public float stopDrag = 5f;
    // Reference to the Rigidbody2D component of the player character
    private Rigidbody2D rb;

    void Start()
    {
        // Get a reference to the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the WASD keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        // If any of the WASD keys are being pressed
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            // Set the drag to zero
            rb.drag = 0f;
            // Create a Vector2 for the movement direction based on the input
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            // Normalize the movement vector
            if(movement.sqrMagnitude > 1)
            {
                movement.Normalize();
            }
            // Add a force to the player character in the movement direction
            rb.AddForce(movement * speed);
            // Limit the magnitude of the player character's velocity to the max speed
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
        // If none of the WASD keys are being pressed
        else
        {
            // Set the velocity of the player character to zero
            rb.drag = stopDrag;
        }
    }
}
