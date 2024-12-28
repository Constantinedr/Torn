using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public static GameManager instance;
 

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Other GameManager methods...



    // Persistent player data
    public int pesos;
    public int experience;

    // Game data
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;   


    public FloatingTextManager floatingTextManager;

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration){
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);



    }

 

    private void OnEnable()
    {
        SceneManager.sceneLoaded += LoadState;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadState;
    }

    public void SaveState()
    {
        string s = "0|"; // Placeholder for additional data
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += "0"; // Placeholder for additional data
        PlayerPrefs.SetString("SaveState", s);
        PlayerPrefs.Save();
        Debug.Log("Game Saved");
    }
    
    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        if (PlayerPrefs.HasKey("SaveState"))
        {
            string[] data = PlayerPrefs.GetString("SaveState").Split('|');
            pesos = int.Parse(data[1]);
            experience = int.Parse(data[2]);
           
            player.transform.position =GameObject.Find("SPAWN").transform.position;
            Debug.Log("Game Loaded: Pesos = " + pesos + ", Experience = " + experience);
        }
    }
}
