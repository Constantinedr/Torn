using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    [System.Serializable]
    public class AbilitySlot
    {
        public GameObject slotObject; // UI slot object
        public bool isActive;         // Whether the slot is filled
    }

    public List<AbilitySlot> abilitySlots = new List<AbilitySlot>(); // List of ability slots

    public void OnButtonPressed(Sprite selectedSprite)
    {
        AssignSpriteToFirstAvailableSlot(selectedSprite);
    }

    private void AssignSpriteToFirstAvailableSlot(Sprite sprite)
    {
        foreach (var slot in abilitySlots)
        {
            if (!slot.isActive) // Find the first empty slot
            {
                AssignSpriteToSlot(slot, sprite);
                slot.isActive = true;
                return;
            }
        }

        Debug.LogWarning("No available slots to assign the sprite!");
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
