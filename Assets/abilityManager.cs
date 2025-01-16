using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    [System.Serializable]
    public class AbilitySlot
    {
        public GameObject slotObject; 
        public bool isActive;        
    }
    public Sprite defaultSprite;
    public Color defaultColor = new Color(0.298f, 0.290f, 0.290f);
    public List<AbilitySlot> abilitySlots = new List<AbilitySlot>(); 

    public void OnButtonPressed(Sprite selectedSprite)
    {
        AssignSpriteToFirstAvailableSlot(selectedSprite);
    }

    private void AssignSpriteToFirstAvailableSlot(Sprite sprite)
    {
        foreach (var slot in abilitySlots)
        {
            if (!slot.isActive) 
            {
                AssignSpriteToSlot(slot, sprite);
                slot.isActive = true;
                return;
            }
        }

        Debug.LogWarning("No available slots to assign the sprite!");
    }
    public void ResetAllIcons()
{
    foreach (var slot in abilitySlots)
    {
        if (slot.isActive)
        {
            Image slotImage = slot.slotObject.GetComponent<Image>();
            if (slotImage != null)
            {
                slotImage.sprite = defaultSprite; 
                slotImage.color = defaultColor;  
                slot.slotObject.SetActive(false); 
                slot.isActive = false; 
         
            }
        }
    }
}
    private void AssignSpriteToSlot(AbilitySlot slot, Sprite sprite)
    {
        if (slot.slotObject == null)
        {
            Debug.LogWarning("Slot object is not assigned!");
            return;
        }

        Image slotImage = slot.slotObject.GetComponent<Image>();
        if (slotImage != null)
        {
            slotImage.sprite = sprite;
            slot.slotObject.SetActive(true);
            Debug.Log("Assigned sprite to slot.");
        }
        else
        {
            Debug.LogWarning("Slot object does not have an Image component!");
        }
    }
}
