/*
In this State the Cornerback tries to block the receiver for a while
and then release him. After that he changes into the cover state
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cornerback_Push_State : Cornerback_Base_State
{   
    bool trigger = false;   //triggerExit Hack
    public override void CleanUp(Cornerback_Controller_FSM cornerback)
    {
        trigger = false;     //triggerExit Hack
    }

    public override void EnterState(Cornerback_Controller_FSM cornerback)
    {
        //Debug.Log("Enter push state");
        //cornerback.transform.GetComponent<BoxCollider2D>().enabled = true;

        cornerback.StartCoroutine(TriggerExitTest(cornerback));  //triggerExit Hack
    }

    public override void OnCollisionEnter(Cornerback_Controller_FSM cornerback, Collision2D other)
    {
        //Debug.Log("Collision Receiver Cornerback");
        if (other.gameObject.tag == "Receiver")
        {
            cornerback.StartCoroutine(Release(cornerback));
        }
    }

    public override void Update(Cornerback_Controller_FSM cornerback)
    {
        Vector2 localRight = cornerback.transform.rotation * Vector2.right;
        cornerback.myRb.AddForce(localRight * cornerback.speed * Time.deltaTime);

        float angle = Vector2.SignedAngle(cornerback.PlayerToCover.position - cornerback.transform.position, new Vector2(1.0f, 0.0f));
        cornerback.myRb.MoveRotation(Mathf.LerpAngle(cornerback.myRb.rotation, -angle, cornerback.rotationSpeed * Time.deltaTime));
    }

    IEnumerator Release(Cornerback_Controller_FSM cornerback)
    {   
        yield return new WaitForSeconds(1);
        cornerback.transform.GetComponent<BoxCollider2D>().enabled = false;
        
    }

    public override void OnCollisionExit(Cornerback_Controller_FSM cornerback, Collision2D other)
    {
    }

    public override void OnTriggerEnter(Cornerback_Controller_FSM cornerback, Collider2D other)
    {
        
    }

     //triggerExit Hack
    IEnumerator TriggerExitTest(Cornerback_Controller_FSM cornerback)
    {
        yield return new WaitForSeconds(1);
        trigger = true;
    }

    public override void OnTriggerExit(Cornerback_Controller_FSM cornerback, Collider2D other)
    {      
        /*
        something is wrong with the OnTriggerExit. The Reset somehow forces OnTriggerExit to fire
        for now the hack is to wait a second until after the reset before we react to the event
        however this is not a stable sollution
        */
        if(other.gameObject.tag == "Receiver" && trigger == true)
        {
            Debug.Log("Trigger in Push State");
            cornerback.TransitionToState(cornerback.cover_State);
        }
    }
}
