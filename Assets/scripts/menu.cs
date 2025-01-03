using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required for TextMeshProUGUI

public class menu : MonoBehaviour
{
    public GameManager gameManager;
    public int gold;
    public int experience;
    public float score;
    public List<int> MenuXpTable;
    public GameObject GoldText; 
    public GameObject ExperienceText; 
    public GameObject ScoreText; 
    public GameObject XpTableText; 

    void Start()
    {
        // Find the GameManager instance
        gameManager = GameManager.instance;

        // Ensure this menu persists across scenes
        DontDestroyOnLoad(gameObject);

        // Initialize gold, experience, and score
        if (gameManager != null)
        {
            MenuXpTable = gameManager.xpTable;
            score = gameManager.score;
            gold = gameManager.pesos;
            experience = gameManager.experience;
            UpdateGoldText();
            UpdateExperienceText();
            UpdateScoreText();
            UpdateXpTable();
        }
        else
        {
            Debug.LogWarning("GameManager not found!");
        }
    }

    public void UpdateGoldAndExperience()
    {
        // Update gold, experience, and score from GameManager
        if (gameManager != null)
        {   
            MenuXpTable = gameManager.xpTable;
            gold = gameManager.pesos;
            experience = gameManager.experience;
            score = gameManager.score;
            UpdateGoldText();
            UpdateExperienceText();
            UpdateScoreText();
            UpdateXpTable();
        }
    }

    private void UpdateGoldText()
    {
        if (GoldText != null)
        {
            var textComponent = GoldText.GetComponent<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = $"{gold}";
            }
            else
            {
                Debug.LogWarning("GoldText does not have a TextMeshProUGUI component!");
            }
        }
    }

    private void UpdateExperienceText()
    {
        if (ExperienceText != null)
        {
            var textComponent = ExperienceText.GetComponent<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = $"{experience}";
            }
            else
            {
                Debug.LogWarning("ExperienceText does not have a TextMeshProUGUI component!");
            }
        }
    }

    private void UpdateScoreText()
    {
        if (ScoreText != null)
        {
            var textComponent = ScoreText.GetComponent<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = $"{score:F1}"; // Display score with 1 decimal place
            }
            else
            {
                Debug.LogWarning("ScoreText does not have a TextMeshProUGUI component!");
            }
        }
    }

    private void UpdateXpTable()
    {
        if (XpTableText != null)
        {
            var textComponent = XpTableText.GetComponent<TextMeshProUGUI>();
            if (textComponent != null && MenuXpTable != null && MenuXpTable.Count > 0)
            {
                int nextLevel = 0;
                foreach (int xp in MenuXpTable)
                {
                    if (experience < xp)
                    {
                        experience-=xp;
                        nextLevel = xp;
                        break;
                    }
                }

                textComponent.text = $"{nextLevel} ";
            }
            else
            {
                Debug.LogWarning("XpTableText does not have a TextMeshProUGUI component or MenuXpTable is empty!");
            }
        }
    }
}
