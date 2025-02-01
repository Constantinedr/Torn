using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ShopUi : MonoBehaviour
{
 public GameManager gameManager;
 private float gold; 
private Animator anim;
public List<Sprite> weaponSprites;
public GameObject boonOptions; 
private int currentSpriteIndex = 0;
 public GameObject GoldText;
 public GameObject requiredGoldText;
 public GameObject weapon;
 public GameObject weaponImage;
 public int requiredGold=10;
  private Weapon weaponScript;
    private void Start()
    {
        // Find the GameManager instance
        gameManager = GameManager.instance;
        anim = GetComponent<Animator>();
        
        // Ensure this menu persists across scenes
        DontDestroyOnLoad(gameObject);
        weaponScript = weapon.GetComponent<Weapon>();

    }

    public void NextSprite()
    {
     

        currentSpriteIndex = (currentSpriteIndex + 1) % weaponSprites.Count;
        weaponImage.GetComponent<Image>().sprite = weaponSprites[currentSpriteIndex];
    }


    public void updateGold(){
        gold = gameManager.pesos;
        UpdateGoldText();
    }
        private void UpdateGoldText()
    {
        UpdateText(GoldText, $"{gold}");
    }

    public void Upgrade(){
        if (gold >= requiredGold){
            NextSprite();
            gold -= requiredGold;
            requiredGold *= 2;
            UpdateText(requiredGoldText,$"{requiredGold}");
            
            gameManager.pesos = gold;
            UpdateGoldText();
            weaponScript.NextSprite();
        }
    }
    public void Boon(){
            if (gold >= 50){
                gold -= 50;
                gameManager.pesos = gold;
                anim.SetTrigger("Hide");
                Animator animator = boonOptions.GetComponent<Animator>();
                BoonOptionsManager boonOptionsManager = boonOptions.GetComponent<BoonOptionsManager>();
                if (boonOptionsManager != null)
                {
                    boonOptionsManager.reAssign();  // Shuffle the boons
                }
                animator.SetTrigger("ShowBOON");
            }
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
