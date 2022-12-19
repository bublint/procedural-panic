using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouse : MonoBehaviour
{
    // The speed at which the object should rotate towards the mouse
    public float rotationSpeed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePos = Input.mousePosition;

        // Convert the mouse position to world coordinates
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // Calculate the direction from the object to the mouse
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        // Rotate the object towards the mouse
        transform.up = direction;
    }
}