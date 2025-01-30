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
        // Ensure only one GameManager instance exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void DestroyObjcect(){
        Destroy(gameObject);
    }

    public void Respawn(){
        experience = 0;
        PlayerLevel=1;
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("MAIN");
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MAIN")
        {
            teleportCount = 0;
            Debug.Log("Scene MAIN loaded, teleportCount reset to 0.");
        }
        TeleportToSpawn();
    }

    private void TeleportToSpawn()
    {
        // Find the "SPAWN" GameObject in the scene
        var spawn = GameObject.Find("SPAWN");

        if (spawn != null && player != null)
        {
            player.transform.position = spawn.transform.position;
            Debug.Log($"Player teleported to SPAWN at {spawn.transform.position}.");
        }
        else
        {
            Debug.LogWarning("SPAWN or player is missing!");
        }
    }

    private void FixedUpdate()
    {
        EnsureSinglePlayer();
        
    }
    
    private void EnsureSinglePlayer()
    {
        // Find all GameObjects with the tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // If more than one exists, destroy all but the first one
        if (players.Length > 1)
        {
            Debug.LogWarning("More than one PLAYER found. Destroying duplicates...");
            for (int i = 1; i < players.Length; i++) // Start from index 1 to preserve the first one
            {
                Destroy(players[i]);
            }
        }

        // Assign the first PLAYER as the main player reference
        if (players.Length > 0)
        {
            player = players[0];
        }
    }

    // Persistent player data
    public int pesos;
    public int experience;
    public Animator deathMenuAnim;
    public int PlayerLevel=1;

    // Game data
    public int nextLevelXP;
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
    public int score;
    public float teleportCount=0;
    public FloatingTextManager floatingTextManager;

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
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
            Debug.Log("Game Loaded: Pesos = " + pesos + ", Experience = " + experience);
        }
    }
}
