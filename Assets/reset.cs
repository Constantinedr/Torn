using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonTap : MonoBehaviour
{
public List<GameObject> someListOfGameObjects = new List<GameObject>();

public void DoomsdayButtonPressed()
{
    foreach (GameObject daysAreNumbered in someListOfGameObjects)
    {
        Destroy(daysAreNumbered);
    }

    someListOfGameObjects.Clear();
}
}