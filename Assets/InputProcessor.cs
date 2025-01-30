using UnityEngine;
using TMPro;

public class InputProcessor : MonoBehaviour
{
    public TMP_InputField inputField; // Assign in Inspector
    public TMP_Text outputText; // Assign in Inspector

    // Public function to process the input
    public void ProcessInput()
    {
        if (inputField != null && outputText != null)
        {
            string userInput = inputField.text;
            outputText.text = userInput;
        }
        else
        {
            Debug.LogError("InputField or OutputText is not assigned in the Inspector!");
        }
    }
}
