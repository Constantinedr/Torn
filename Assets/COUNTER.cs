using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class COUNTER : MonoBehaviour
{   
    public int currentScore;
    public GameObject targetObject; 
    public TextMeshProUGUI scoreText;
    public bool check = false;
    private void Update(){
    if (targetObject == null){
          
           targetObject = GameObject.Find("BOSS");

        }
    if (targetObject != null && !check){
        StartCoroutine(CheckTargetDestroyed());
        check = true;
    }
        if (targetObject == null && check){

        check = false;
    }
    }
        private IEnumerator CheckTargetDestroyed()
    {
        while (targetObject != null)
        {
            yield return null; 
        }


        AddScore();
    }
    void Awake()
    {
        check = false;
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
