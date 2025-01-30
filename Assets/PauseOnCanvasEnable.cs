using UnityEngine;

public class PauseOnCanvasEnable : MonoBehaviour
{
    public Canvas targetCanvas; 

    void Start()
    {
        if (targetCanvas != null && targetCanvas.enabled)
        {
            Time.timeScale = 0f;
        }
    }

    public void OnDisable()
    {
        Time.timeScale = 1f; 
    }
}