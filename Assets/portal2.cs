using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal2 : Collidable
{
    public string[] sceneNames;
    public void tp(){
        string sceneName = sceneNames[Random.Range(0, sceneNames.Length)]; // Fixed Random.Range
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName); // Fixed UnityEngine capitalization
    }
}