using UnityEngine;

public class Portal : Collidable
{
    public string[] sceneNames;
    public string[] sceneNames2;
    private bool canTeleport = true;  // Flag to prevent rapid teleportation after a certain number of uses

    private const int maxTeleports = 6;  // Maximum number of teleports before switching scenes

    protected override void OnCollide(Collider2D coll)
    {
        if (canTeleport){
            if (coll.name == "PLAYER" )
            {
                Teleport();
            }
        }
    }

    public void Teleport()
    {
      
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
