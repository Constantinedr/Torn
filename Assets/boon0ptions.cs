using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class BoonOptionsManager : MonoBehaviour
{
    [System.Serializable]
    public class BoonData
    {
        public Sprite icon;          // Icon for the boon
        public string title;         // Title of the boon
        public string description;   // Description of the boon
        public UnityEvent buttonAction; // Action to assign to the button
    }
    



    // List of prefab data for boons
    public List<BoonData> boonPrefabs;
    public GameObject ability1;
    public GameObject ability2;
    public GameObject ability3;

    void Start()
    {
        AssignRandomBoons();
        
    }

    private void AssignRandomBoons()
    {
        if (boonPrefabs.Count == 0)
        {
            Debug.LogWarning("No boons available to assign!");
            return;
        }

        // Shuffle the boonPrefabs list
        List<BoonData> shuffledBoons = new List<BoonData>(boonPrefabs);
        ShuffleList(shuffledBoons);

        // Assign boons to each ability
        AssignBoonToAbility(ability1, shuffledBoons[0]);
        AssignBoonToAbility(ability2, shuffledBoons[1]);
        AssignBoonToAbility(ability3, shuffledBoons[2]);
    }

    private void AssignBoonToAbility(GameObject ability, BoonData boon)
    {
        // Find UI elements inside the ability
        Transform boonIcon = ability.transform.Find("boonIcon");
        Transform titleBoon = ability.transform.Find("TitleBoon");
        Transform textDescription = ability.transform.Find("TextDescription");
        Transform abilityButton = ability.transform.Find("abilityButton");

        // Assign the icon
        if (boonIcon != null && boonIcon.GetComponent<Image>() != null)
        {
            boonIcon.GetComponent<Image>().sprite = boon.icon;
            Debug.Log($"Icon updated for {ability.name}");
        }
        else
        {
            Debug.LogWarning($"boonIcon not found or missing Image component in {ability.name}");
        }

        // Assign the title
        if (titleBoon != null && titleBoon.GetComponent<TextMeshProUGUI>() != null)
        {
            titleBoon.GetComponent<TextMeshProUGUI>().text = boon.title;
            Debug.Log($"Title updated for {ability.name}: {boon.title}");
        }
        else
        {
            Debug.LogWarning($"TitleBoon not found or missing Text component in {ability.name}");
        }

        // Assign the description
        if (textDescription != null && textDescription.GetComponent<TextMeshProUGUI>() != null)
        {
            textDescription.GetComponent<TextMeshProUGUI>().text = boon.description;
            Debug.Log($"Description updated for {ability.name}: {boon.description}");
        }
        else
        {
            Debug.LogWarning($"TextDescription not found or missing Text component in {ability.name}");
        }

        // Assign the button action
        if (abilityButton != null && abilityButton.GetComponent<Button>() != null)
        {
            Button button = abilityButton.GetComponent<Button>();
            button.onClick.RemoveAllListeners(); // Clear any previous actions
            if (boon.buttonAction != null)
            {
                button.onClick.AddListener(() => boon.buttonAction.Invoke());
                Debug.Log($"Button action assigned for {ability.name}");
            }
        }
        else
        {
            Debug.LogWarning($"abilityButton not found or missing Button component in {ability.name}");
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}