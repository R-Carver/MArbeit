using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Opponent_Rush : MonoBehaviour
{
    //public List<GameObject> opponents = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //if(other.gameObject.tag == "Defender" && !opponents.Contains(other.gameObject))
        if(other.gameObject.tag == "Defender")
        {
            Rusher_Controller_FSM rush_Controller = transform.parent.GetComponent<Rusher_Controller_FSM>();
            rush_Controller.Target = other.transform;

            rush_Controller.TransitionToState(rush_Controller.oppVisible_State);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
           

    }
}
