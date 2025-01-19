using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShopUi : MonoBehaviour
{
 public GameManager gameManager;
 private float gold; 
 public GameObject GoldText;
    private void Start()
    {
        // Find the GameManager instance
        gameManager = GameManager.instance;
        
        // Ensure this menu persists across scenes
        DontDestroyOnLoad(gameObject);


    }
    public void updateGold(){
        gold = gameManager.pesos;
        UpdateGoldText();
    }
        private void UpdateGoldText()
    {
        UpdateText(GoldText, $"{gold}");
    }
    private void UpdateText(GameObject textObject, string content)
    {
        if (textObject != null)
        {
            var textComponent = textObject.GetComponent<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = content;
            }
            else
            {
                Debug.LogWarning($"{textObject.name} does not have a TextMeshProUGUI component!");
            }
        }
    }
}
