using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall2 : MonoBehaviour
{
    public void destroyWall()
    {
        Debug.Log("Wall destroyed because target object was destroyed.");
        Destroy(gameObject);
    }
}
