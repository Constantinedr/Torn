using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public GameObject heartPrefab; // Prefab for a single heart
    public Transform heartsContainer; // Parent object to hold the hearts
    public Sprite fullHeartSprite; // Sprite for a full heart
    public Sprite halfHeartSprite; // Sprite for a half heart
    public Sprite emptyHeartSprite; // Sprite for an empty heart

    private List<Image> heartImages = new List<Image>();

    // Initialize hearts based on player's max HP
    public void InitializeHearts(int maxHP)
    {
        // Clear existing hearts
        foreach (Transform child in heartsContainer)
        {
            Destroy(child.gameObject);
        }
        heartImages.Clear();

        // Create hearts (1 heart per 2 HP)
        for (int i = 0; i < Mathf.CeilToInt(maxHP / 2f); i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsContainer);
            Image heartImage = heart.GetComponent<Image>();
            heartImages.Add(heartImage);
        }
    }

    // Update heart sprites based on current HP
    public void UpdateHearts(int currentHP)
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            int heartValue = currentHP - (i * 2); // Calculate the value for each heart (2 HP per heart)
            if (heartValue >= 2)
            {
                heartImages[i].sprite = fullHeartSprite;
            }
            else if (heartValue == 1)
            {
                heartImages[i].sprite = halfHeartSprite;
            }
            else
            {
                heartImages[i].sprite = emptyHeartSprite;
            }
        }
    }
}
