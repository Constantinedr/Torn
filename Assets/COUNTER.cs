using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class COUNTER : MonoBehaviour
{   
    public GameObject targetObject; 
    public TextMeshProUGUI scoreText;

    private void Update(){


    }
        private IEnumerator CheckTargetDestroyed()
    {
        while (targetObject != null)
        {
            yield return null;  // Wait until the next frame
        }


        AddScore();
    }
    void Awake()
    {
        targetObject = GameObject.Find("Demon_boss");
        if (targetObject != null){
            StartCoroutine(CheckTargetDestroyed());
        }
        DontDestroyOnLoad(gameObject);

        GameObject textObject = GameObject.Find("difficulty");
        if (textObject != null)
        {
            scoreText = textObject.GetComponent<TextMeshProUGUI>();
            if (scoreText == null)
            {
                Debug.LogError("The object named 'difficulty' does not have a TextMeshProUGUI component!");
            }
        }
        else
        {
            Debug.LogError("No GameObject named 'difficulty' found in the scene!");
        }
    }

    public void AddScore()
    {
        if (scoreText != null)
        {
            int currentScore;
            if (int.TryParse(scoreText.text, out currentScore))
            {
                currentScore++;  // Increment the score
                scoreText.text = currentScore.ToString();  // Update the UI
            }
            else
            {
                Debug.LogError("Invalid number format in difficulty text.");
            }
        }
    }
}
