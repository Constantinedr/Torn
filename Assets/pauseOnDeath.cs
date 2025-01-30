using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseOnDeath : MonoBehaviour
{
    public bool paused;
    public void Unpasused(){

        paused= false;
    }
    private void Update()
    {

        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
