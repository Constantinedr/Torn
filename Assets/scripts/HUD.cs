using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;  // Reference to the GameManager
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro UI text component

    void Start()
    {
        // Get the instance of GameManager
        gameManager = GameManager.instance;

        // Ensure the HUD persists across scenes
        DontDestroyOnLoad(gameObject);

        GameObject textObject = GameObject.Find("ScoreText");
        if (textObject != null)
        {
            scoreText = textObject.GetComponent<TextMeshProUGUI>();
            if (scoreText == null)
            {
                Debug.LogError("The object named 'ScoreText' does not have a TextMeshProUGUI component!");
            }
        }

    }

    void Update()
    {
        if (gameManager != null && scoreText != null)
        {
            // Update the TextMeshPro text with the current score from GameManager
            scoreText.text = " " + gameManager.score.ToString();
        }
    }
}
