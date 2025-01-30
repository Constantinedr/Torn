using UnityEngine;

public class pause : MonoBehaviour
{
    public GameObject pauseMenuUI; // Drag your menu UI Panel here in the Inspector
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void close(){

     Application.Quit();
    }
    void PauseGame()
    {
        pauseMenuUI.SetActive(true); // Show menu
        Time.timeScale = 0f; // Freeze time
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Hide menu
        Time.timeScale = 1f; // Resume time
        isPaused = false;
    }
}
