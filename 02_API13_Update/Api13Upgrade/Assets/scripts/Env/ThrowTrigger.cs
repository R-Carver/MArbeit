using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
            if(other.gameObject.tag == "Receiver")
            {
                //Debug.Log("Recevier reached Throw zone");
                GameManager.Instance.canThrow = true;
                this.gameObject.SetActive(false);
            }
    }
}
