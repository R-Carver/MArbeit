using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos_Instance : MonoBehaviour
{
    void Awake()
    {
        Receiver_StartPositions.availableStartPositions.Add(this.transform.position, true);
    }
}
