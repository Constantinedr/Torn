using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonManager : MonoBehaviour
{
    public GameObject player;
    public GameObject gameManager;
    public GameObject weapon;

    private void Start()
    {
        player = GameObject.Find("PLAYER");
        gameManager = GameObject.Find("GameManager");
        weapon = GameObject.Find("weapon_0");
    }
    private void Update(){
        player = GameObject.Find("PLAYER");
        gameManager = GameObject.Find("GameManager");
        weapon = GameObject.Find("weapon_0");

    }
    public void FreezePlayer()
    {
        Player playerScript = player.GetComponent<Player>();
        Weapon weaponScript = weapon.GetComponent<Weapon>();
        playerScript.Freeze();
        weaponScript.Freeze();
    }

    public void UnfreezePlayer()
    {
        Player playerScript = player.GetComponent<Player>();
        Weapon weaponScript = weapon.GetComponent<Weapon>();
        playerScript.Unfreeze();
        weaponScript.Unfreeze();

    }
    public void DefyDeath(){
        Player playerScript = player.GetComponent<Player>();
        playerScript.DefyDeath();
    }

    public void HeartSteel()
    {
        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.HeartSteel(2); // Increase max HP by 2
            Debug.Log("HeartSteel applied: Max HP increased by 2.");
        }
        else
        {
            Debug.LogError("Player script not found! Cannot apply HeartSteel.");
        }
    }
    public void HellHoundFury(){
        Player playerScript = player.GetComponent<Player>();
        playerScript.HellHoundFury();
    }
    public void SpeedDemon()
    {
        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.ActivateSpeedBuffPASSIVE();
            Debug.Log("SpeedDemon applied: Player speed increased.");
        }
        else
        {
            Debug.LogError("Player script not found! Cannot apply SpeedDemon.");
        }
    }
}
