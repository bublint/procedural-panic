// Weapon is a script that allows a game object to shoot a raycast
// The raycast can deal damage to objects with an EnemyHealth script
// The script uses the Unity Input System to handle input for firing the raycast

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon: MonoBehaviour
{
    // The transform of the weapon
    public Transform weaponTransform;

    // The maximum distance that the raycast should reach
    public float maxDistance = Mathf.Infinity;

    // The layer mask for the raycast to check for objects to hit
    public LayerMask layerMask;

    // The damage that the raycast will deal to enemies
    public int damage = 10;

    // The input action that will be used to perform the raycast
    public InputActionAsset playerActions;
    InputActionMap playerActionMap;
    InputAction fireInputAction;

    // Needed for input action system
    void Awake()
    {
        playerActionMap = playerActions.FindActionMap("Player");
        fireInputAction = playerActionMap.FindAction("Fire");
        fireInputAction.performed += context => Fire();
    }

    // Needed for input action system
    void OnEnable()
    {
        fireInputAction.Enable();
    }

    // Needed for input action system
    void OnDisable()
    {
        fireInputAction.Disable();
    }

    void Fire()
    {
        Debug.Log("Fire!");
        // Perform the raycast
        Vector2 origin = weaponTransform.position;
        Vector2 direction = weaponTransform.right;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, layerMask);
        if (hit.collider != null)
        {
            Debug.Log("Hit!");
            // Visualize the raycast
            Debug.DrawRay(origin, direction, Color.red, 10.0f);;

            // Check if the object hit has a Health component
            EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
            if (health != null)
            {
                // Deal damage to the object
                health.HandleCollision(damage);
            }
        }
        else
        {
            Debug.Log("Miss!");
            // Visualize the raycast
            Debug.DrawRay(origin, direction, Color.red, 10.0f);
        }
    }
}