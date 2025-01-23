using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    public GameObject targetObject; 

    private void Start()
    {
  

        StartCoroutine(CheckTargetDestroyed());
    }

    private IEnumerator CheckTargetDestroyed()
    {
        while (targetObject != null)
        {
            yield return null;  // Wait until the next frame
        }


        destroyWall();
    }

    public void destroyWall()
    {
        Debug.Log("Wall destroyed because target object was destroyed.");
        Destroy(gameObject);
    }
}
