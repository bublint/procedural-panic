// The Look script is a simple script that allows an object to rotate towards the mouse position or a thumbstick input
// It uses the Unity Input System to handle input for rotation and uses Quaternion.Lerp to smoothly rotate the object towards the target angle

using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    // The speed at which the object rotates
    [SerializeField] private float rotationSpeed = 10f;

    // Reference to the PlayerInputActions asset
    private PlayerInputActions inputActions;

    // Needed for input action system
    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Look.performed += OnMousePosition;
        inputActions.Player.LookGamepad.performed += OnRightThumbstick;
    }

    private void OnMousePosition(InputAction.CallbackContext context)
    {
        // Get the mouse position in world space
        Vector2 mousePosition = context.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.z));

        // Calculate the angle between the weapon and the mouse position
        float angle = Mathf.Atan2(mouseWorldPosition.y - transform.position.y, mouseWorldPosition.x - transform.position.x) * Mathf.Rad2Deg;

        // Rotate the weapon towards the mouse position
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
    }

    private void OnRightThumbstick(InputAction.CallbackContext context)
    {
        // Get the thumbstick input
        Vector2 thumbstickInput = context.ReadValue<Vector2>();

        // Calculate the angle based on the thumbstick input
        float angle = Mathf.Atan2(thumbstickInput.y, thumbstickInput.x) * Mathf.Rad2Deg;

        // Rotate the weapon towards the calculated angle
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
    }

    // Needed for input action system
    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    // Needed for input action system
    private void OnDisable()
    {
        inputActions.Player.Disable();
    }
}
