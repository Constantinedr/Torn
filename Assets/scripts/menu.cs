using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required for TextMeshProUGUI
using UnityEngine.UI;
public class menu : MonoBehaviour
{
    public GameManager gameManager;
    public float gold;
    public int experience;
    public float score;
    public List<int> MenuXpTable;
    public List<int> MenuWeaponPrices;
    public GameObject GoldText;
    public GameObject ExperienceText;
    public GameObject ScoreText;
    public GameObject XpTableText;
    public GameObject WeaponPricesText;
    public GameObject levelText;
    private int Level = 1;

    void Start()
    {
        // Find the GameManager instance
        gameManager = GameManager.instance;
        
        // Ensure this menu persists across scenes
        DontDestroyOnLoad(gameObject);

        // Initialize gold, experience, and score
        if (gameManager != null)
        {
            UpdateGoldAndExperience();

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
            MenuWeaponPrices = gameManager.weaponPrices;
            MenuXpTable = gameManager.xpTable;
            gold = gameManager.pesos;
            experience = gameManager.experience;
            score = gameManager.score;
            UpdateWeapon();
            UpdateGoldText();
            UpdateExperienceText();
            UpdateScoreText();
            UpdateXpTable();
            
            LevelUP();
        }
    }

    private void UpdateGoldText()
    {
        UpdateText(GoldText, $"{gold}");
    }

    private void UpdateExperienceText()
    {
        UpdateText(ExperienceText, $"{experience}");
    }

    private void UpdateScoreText()
    {
        UpdateText(ScoreText, $"{score:F1}"); // Display score with 1 decimal place
    }
     public void DestroyObjcect(){
        Destroy(gameObject);
    }
    private void UpdateXpTable()
    {
        if (XpTableText != null)
        {
            var textComponent = XpTableText.GetComponent<TextMeshProUGUI>();
            if (textComponent != null && MenuXpTable != null && MenuXpTable.Count > 0)
            {
                int nextLevelXp = 0;
                foreach (int xp in MenuXpTable)
                {
                    if (experience < xp)
                    {
                        nextLevelXp = xp;
                       
                        break;
                    }
                    else{
                        Level++;
                    
                    }
                }

                // Check if we should level up
                if (experience >= nextLevelXp && nextLevelXp > 0)
                {
                    LevelUP();
                    
                }

                textComponent.text = $"{nextLevelXp}";
                
            }
            else
            {
                Debug.LogWarning("XpTableText does not have a TextMeshProUGUI component or MenuXpTable is empty!");
            }
        }
    }



    public void UpdateWeaponPrices()
    {
        if (WeaponPricesText != null)
        {
            var textComponent = WeaponPricesText.GetComponent<TextMeshProUGUI>();
            if (textComponent != null && MenuWeaponPrices != null && MenuWeaponPrices.Count > 0)
            {
                int nextLevelGold = 0;
                foreach (int prices in MenuWeaponPrices)
                {
                    if (gold < prices)
                    {
                        
                        nextLevelGold = prices;
                        break;
                    }
                }
                textComponent.text = $"{nextLevelGold}";
            }
        
        }
    }

    private void LevelUP()
    {
        
        UpdateText(levelText, $"{Level}");
        Debug.Log("{Level}");
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
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private GameObject sourceObject;
    [SerializeField] private Image targetImage; // UI Image component
    private Weapon weaponScript; 
    public int dmg;
    public void UpdateWeapon(){
        
        weaponScript = sourceObject.GetComponent<Weapon>();
        dmg = weaponScript.damagePoint;
        UpdateUI(dmg);
    }
    private void UpdateUI(float dmg)
    {
        // Update TMP text
        if (textDisplay != null)
        {
            textDisplay.text = dmg.ToString("F2"); // Format to 2 decimal places
        }


        SpriteRenderer sourceSpriteRenderer = sourceObject.GetComponent<SpriteRenderer>();

        if (sourceSpriteRenderer != null && targetImage != null)
        {
            targetImage.sprite = sourceSpriteRenderer.sprite;
        }
    }
}

