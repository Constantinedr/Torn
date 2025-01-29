using UnityEngine;

public class CanvasController : MonoBehaviour
{
    // Reference to the Canvas component
    public Canvas canvas;

    // Function to disable the canvas
    public void DisableCanvas()
    {
        if (canvas != null)
        {
            canvas.enabled = false;
        }
        else
        {
            Debug.LogWarning("Canvas reference is not set.");
        }
    }

    // Optionally, you can add a function to enable the canvas as well
    public void EnableCanvas()
    {
        if (canvas != null)
        {
            canvas.enabled = true;
        }
        else
        {
            Debug.LogWarning("Canvas reference is not set.");
        }
    }
}
