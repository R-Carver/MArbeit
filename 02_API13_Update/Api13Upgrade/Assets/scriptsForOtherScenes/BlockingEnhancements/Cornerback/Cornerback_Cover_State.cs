using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockingEnhancements
{
public class Cornerback_Cover_State : Cornerback_Base_State
{   
    bool doReact;
    public override void CleanUp(Cornerback_Controller_FSM cornerback)
    {
        
    }

    public override void EnterState(Cornerback_Controller_FSM cornerback)
    {
        GetRotation(cornerback);
        doReact = true;
    }

    public override void Update(Cornerback_Controller_FSM cornerback)
    {
        Vector2 localRight = cornerback.transform.rotation * Vector2.right;
        cornerback.myRb.AddForce(localRight * cornerback.speed * Time.deltaTime);

        //mechanism to simulate reaction time for cornerback
        if(doReact == true)
        {
            cornerback.StartCoroutine(ApplyRotation(cornerback));
            doReact = false;
        }

        //try to intercept the ball if its in the air
        if(cornerback.qB_Controller.Receiver_current == cornerback.PlayerToCover.gameObject)
        {
            //here we make sure that onlt the right cornerback tries to intercept the ball
            //if (cornerback.ball_controller.launched == true)
            if(GameManager.Instance.ballLaunched == true)
            {
                cornerback.TransitionToState(cornerback.intercept_State);
            }
        }
    }

    IEnumerator ApplyRotation(Cornerback_Controller_FSM cornerback)
    {   
        yield return new WaitForSeconds(cornerback.reactionDelay);
        GetRotation(cornerback);

        doReact = true;
    }

    private void GetRotation(Cornerback_Controller_FSM cornerback)
    {
        float angle = Vector2.SignedAngle(cornerback.PlayerToCover.position - cornerback.transform.position, new Vector2(1.0f, 0.0f));
        cornerback.myRb.MoveRotation(-angle);
    }

    public override void OnCollisionEnter(Cornerback_Controller_FSM cornerback, Collision2D other)
    {
        
    }

    public override void OnCollisionExit(Cornerback_Controller_FSM cornerback, Collision2D other)
    {

    }

    public override void OnTriggerEnter(Cornerback_Controller_FSM cornerback, Collider2D other)
    {
        
    }

    public override void OnTriggerExit(Cornerback_Controller_FSM cornerback, Collider2D other)
    {
        
    }
}
}
