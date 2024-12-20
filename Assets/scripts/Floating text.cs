using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion; // Define the type for motion
    public float duration; // Add duration for how long the text should be visible
    private float lastShown; // Correct the type for lastShown

    // Show the floating text
    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(true);
    }

    // Hide the floating text
    public void Hide()
    {
        active = false;
        go.SetActive(false);
    }

    // Update the floating text position and visibility
    public void UpdateFloatingText()
    {
        if (!active)
            return;

        // Hide if the duration has passed
        if (Time.time - lastShown > duration)
            Hide();

        // Update position with motion
        go.transform.position += motion * Time.deltaTime;
    }
    
}
