using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    public string[] sceneNames;
    private COUNTER counter;
    public bool score = false;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "PLAYER" && score)
        {
            counter.AddScore(); 
            Debug.Log("ji");
        }
        if (coll.name == "PLAYER")
        {
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)]; // Fixed Random.Range
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName); // Fixed UnityEngine capitalization
        }


    }
    public void tp(){
        string sceneName = sceneNames[Random.Range(0, sceneNames.Length)]; // Fixed Random.Range
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName); // Fixed UnityEngine capitalization
    }
}
