/*
Global Script for Player Info needed for example for the agent script to get the
Player Information
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{   
    public static PlayerInfo Instance;

    public GameObject[] Receivers;
    public GameObject[] Cornerbacks;

    void Awake() 
    {   
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }else
        {
            Instance = this;
        }

        GameObject receiverObj = GameObject.Find("Receivers");
        Receivers = new GameObject[receiverObj.transform.childCount];

        int childIndex = 0;
        foreach(Transform receiver in receiverObj.transform)
        {
            Receivers[childIndex++] = receiver.gameObject;
        }

        GameObject cornerbackObj = GameObject.Find("Secondary");
        Cornerbacks = new GameObject[cornerbackObj.transform.childCount];

        childIndex = 0;
        foreach(Transform cornerback in cornerbackObj.transform)
        {
            Cornerbacks[childIndex++] = cornerback.gameObject;
        }    
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
