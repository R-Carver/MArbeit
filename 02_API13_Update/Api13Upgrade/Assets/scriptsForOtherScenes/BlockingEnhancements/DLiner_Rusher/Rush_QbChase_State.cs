﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockingEnhancements
{
    public class Rush_QbChase_State : Rush_Base_State
    {
        Rigidbody2D myRb;
        public override void CleanUp(Rusher_Controller_FSM rusher)
        {

        }

        public override void EnterState(Rusher_Controller_FSM rusher)
        {
            myRb = rusher.myRb;
            rusher.Target = rusher.Qb;
        }

        public override void OnCollisionEnter(Rusher_Controller_FSM rusher, Collider2D col)
        {

        }

        public override void Update(Rusher_Controller_FSM rusher)
        {
            RaycastHit2D hit = rusher.RayToQb();

            //check if the defender is in the way
            if(hit.collider !=  null)
            {   
                rusher.Target = hit.transform;

                rusher.TransitionToState(rusher.oppVisible_State);
                //Debug.Log("Oliner in sight");
            }

            Vector2 localRight = rusher.transform.rotation * Vector2.right;
            myRb.AddForce(localRight * rusher.speed * Time.deltaTime);

            float angle = Vector2.SignedAngle(rusher.Target.position - rusher.transform.position, new Vector2(1.0f, 0.0f));
            myRb.MoveRotation(Mathf.LerpAngle(myRb.rotation, -angle, rusher.rotationSpeed * Time.deltaTime));
        }
    }
}
