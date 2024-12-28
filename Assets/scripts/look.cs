using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thelooker : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.15f;
    private void Start(){
        lookAt = GameObject.Find("PLAYER").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        // Calculate deltaX
        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        // Calculate deltaY
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        // Update camera position
        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
