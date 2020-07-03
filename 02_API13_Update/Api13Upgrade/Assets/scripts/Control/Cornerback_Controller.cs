using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Script for Managing Behavior for all Cornerbacks.
*/

public class Cornerback_Controller : MonoBehaviour
{
    public static Cornerback_Controller Instance;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            Debug.LogError("There were two Cornerback-Controllers. This should not happen");
        }else
        {
            Instance = this;
        }
    }

    public List<GameObject> allCornerbacks = new List<GameObject>();

    public void ActivateAll()
    {
        foreach(GameObject go in allCornerbacks)
        {
            go.SetActive(true);
        }
    }

    public void DeactivateOne()
    {
        GameObject goToDisable = allCornerbacks[Random.Range(0,allCornerbacks.Count)];
        goToDisable.SetActive(false);
    }
}
