using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
   public int damage;
   public float pushForce;
    private int difficulty;

    protected override void Start()
    {
        GameObject difficultyCounter = GameObject.Find("COUNTER");
        if (difficultyCounter != null)
        {
            COUNTER counterScript = difficultyCounter.GetComponent<COUNTER>();
            if (counterScript != null && counterScript.scoreText != null)
            {
                if (int.TryParse(counterScript.scoreText.text, out difficulty))
                {
                    difficulty = Mathf.Max(difficulty, 1); // Ensure difficulty is at least 1
                }
                else
                {
                    difficulty = 1;
                }
            }
        }
        else
        {
            difficulty = 1;
        }
        base.Start();
        damage *= difficulty; 
    }

    protected override void OnCollide(Collider2D coll){
        if (coll.tag == "FIGHTER" && coll.name == "PLAYER"){
               
            Damage dmg = new Damage{ 
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };
            coll.SendMessage("ReceiveDamage",dmg); 
        }

    }


}
