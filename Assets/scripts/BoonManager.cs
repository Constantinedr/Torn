using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonManager : MonoBehaviour
{

    public GameObject player;
    public GameObject gameManager;

    private void Start(){
        player = GameObject.Find("PLAYER");
        gameManager = GameObject.Find("GameManager");
    }


    public void HeartSteel(){
        //coll.SendMessage("Heartsteel",2);
    }
}
