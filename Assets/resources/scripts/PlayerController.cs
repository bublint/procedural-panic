// The PlayerController script is a simple script that allows a player character to move around in a 2D game
// It uses the Unity Input System to handle input for movement and applies forces to the Rigidbody2D component of the player character
// The script also has a drag applied when no keys are being pressed to slow down the player character

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    // The bounding box of the area that the player character can move in
    public Bounds bounds;

    private PlayerInputActions inputActions;
    private Vector2 movementInput;

    // Needed for input action system
    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Movement.canceled += ctx => movementInput = Vector2.zero;
    }

    // Needed for input action system
    void OnEnable()
    {
        inputActions.Enable();
    }

    // Needed for input action system
    void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        // Get a reference to the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // If any movement input is being provided
        if (movementInput != Vector2.zero)
        {
            // Set the drag to zero
            rb.drag = 0f;
            // Normalize the movement vector
            if(movementInput.sqrMagnitude > 1)
            {
                movementInput.Normalize();
            }
            // Add a force to the player character in the movement direction
            rb.AddForce(movementInput * speed);
            // Limit the magnitude of the player character's velocity to the max speed
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
        // If no movement input is being provided
        else
        {
            // Set the velocity of the player character to zero
            rb.drag = stopDrag;
        }
    }
}