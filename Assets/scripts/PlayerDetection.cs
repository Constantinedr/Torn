using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : Collidable
{
    public GameObject enemyGameObject;
    private bool Detected=false;
    protected override void OnCollide(Collider2D coll)
    {    
        if (coll.name == "PLAYER"){
            if(Detected!=true){
            Detected=true;
            Alert();
            Debug.Log("ALERT");
            }
        }
        else{
            Detected=false;
            ShootAt();
        }
    }
    public void Alert(){
        if (Detected){
        demonShooter demonShooter = enemyGameObject.GetComponent<demonShooter>();
        demonShooter.Danger();
        }
        else{
            return;
        }
    }
     public void ShootAt(){
        if (!Detected){
        demonShooter demonShooter = enemyGameObject.GetComponent<demonShooter>();
        demonShooter.shoot();
        }
        else{
            return;
        }
    }
}
