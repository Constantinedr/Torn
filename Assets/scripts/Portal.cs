using UnityEngine;

public class Portal : Collidable
{
    public string[] sceneNames;
    public string[] sceneNames2;
    private bool canTeleport = true;  

    private const int maxTeleports = 6;  // Maximum number of teleports before switching scenes

    protected override void OnCollide(Collider2D coll)
    {
        if (canTeleport){
            if (coll.name == "PLAYER" )
            {
                canTeleport = false; 
                Teleport();
            }
        }
    }

    public void Teleport()
    {
        Debug.Log("AGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG"
        );
        GameManager.instance.teleportCount++;

        if (GameManager.instance.teleportCount >= maxTeleports)
        {
            string sceneName = sceneNames2[Random.Range(0, sceneNames2.Length)];
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
        else
        {
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

  
        GameManager.instance.SaveState();
    }
}
