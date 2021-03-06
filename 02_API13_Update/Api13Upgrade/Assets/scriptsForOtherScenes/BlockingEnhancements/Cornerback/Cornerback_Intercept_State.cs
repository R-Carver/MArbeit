﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockingEnhancements
{
public class Cornerback_Intercept_State : Cornerback_Base_State
{   

    public override void CleanUp(Cornerback_Controller_FSM cornerback)
    {
        cornerback.speed -= 1;
    }

    public override void EnterState(Cornerback_Controller_FSM cornerback)
    {
        cornerback.speed += 1;
    }

    public override void OnCollisionEnter(Cornerback_Controller_FSM cornerback, Collision2D other)
    {   
        
    }

    public override void OnCollisionExit(Cornerback_Controller_FSM cornerback, Collision2D other)
    {
        
    }

    public override void OnTriggerEnter(Cornerback_Controller_FSM cornerback, Collider2D other)
    {   
        if(other.gameObject.tag == "Ball")
        {
            Transform ballGo = other.gameObject.transform.parent;

            //check the height of the ball
            //Debug.Log("Ball height: " + "<color=green><b> " + ballGo.transform.position.z + "</b></color>");
            bool canCatchBall = ballGo.transform.position.z <= GameManager.Instance.catchableHeight;

            //try to catch ball
            if (canCatchBall)
            {
                other.gameObject.tag = "BallCaught";

                ballGo.parent = cornerback.transform;
                ballGo.transform.position = cornerback.transform.position;

                //baselines BallBug Test
                Rigidbody ballRb = ballGo.GetComponent<Rigidbody>();
                ballRb.isKinematic = true;

                cornerback.TransitionToState(cornerback.ballCaught_State);
            }
            else
            {
                cornerback.TransitionToState(cornerback.cover_State);
            }
        }   
    }

    public override void OnTriggerExit(Cornerback_Controller_FSM cornerback, Collider2D other)
    {
        
    }

    public override void Update(Cornerback_Controller_FSM cornerback)
    {
        Vector2 localRight = cornerback.transform.rotation * Vector2.right;
        cornerback.myRb.AddForce(localRight * cornerback.speed * Time.deltaTime);

        //it should be the best strategy to intecept the ball if the cornerback tries to head into 
        //the direction where the ball is going
        Vector3 ballDir = cornerback.ball_controller.target.position;

        float angle = Vector2.SignedAngle(ballDir - cornerback.transform.position, new Vector2(1.0f, 0.0f));
        cornerback.myRb.MoveRotation(Mathf.LerpAngle(cornerback.myRb.rotation, -angle, cornerback.rotationSpeed * Time.deltaTime));
    }
}
}
