using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockingEnhancements
{
    public class Rush_OppVisible_State : Rush_Base_State
    {
        Rigidbody2D myRb;
        RushMode rushMode;
        public override void CleanUp(Rusher_Controller_FSM rusher)
        {

        }

        public override void EnterState(Rusher_Controller_FSM rusher)
        {
            myRb = rusher.myRb;
            //Decide if you want to go inside or outside
            //for now go outside
            rushMode = getRandomRushmode();
        }

        public override void OnCollisionEnter(Rusher_Controller_FSM rusher, Collider2D col)
        {

        }

        public override void Update(Rusher_Controller_FSM rusher)
        {   
            //check if the defender is in the way
            if(rusher.RayToQb().collider ==  null)
            {   
                //Debug.Log("Oliner out of sight");
                rusher.TransitionToState(rusher.qbChase_State);
            }

            Vector2 localRight = rusher.transform.rotation * Vector2.right;
            myRb.AddForce(localRight * rusher.speed * Time.deltaTime);

            /*if (rushMode == RushMode.Outside)
            {
                RushOutside(rusher);
            }*/
            switch(rushMode)
            {
                case RushMode.Central:
                    RushCentral(rusher);
                    //Debug.Log(rushMode);
                    break;
                case RushMode.Inside:
                    RushInside(rusher);
                    //Debug.Log(rushMode);
                    break;
                case RushMode.Outside:
                    RushOutside(rusher);
                    //Debug.Log(rushMode);
                    break;
            }
        }

        private void RushOutside(Rusher_Controller_FSM rusher)
        {
            //get the world coord of the point which is 1 left localy from the defender
            Vector2 leftToTarget = rusher.Target.TransformPoint(Vector3.down * .5f);

            //modify the angle to rush outside
            //Vector2 outVec = new Vector2(rusher.Target.position.x, rusher.Target.position.y + );
            float angle = Vector2.SignedAngle(leftToTarget - (Vector2)rusher.transform.position, new Vector2(1.0f, 0.0f));
            //float angle = Vector2.SignedAngle(rusher.Target.position - rusher.transform.position, new Vector2(1.0f, 0.0f));
            //angle = angle - 15;

            myRb.MoveRotation(Mathf.LerpAngle(myRb.rotation, -angle, rusher.rotationSpeed * Time.deltaTime));
        }

        private void RushInside(Rusher_Controller_FSM rusher)
        {
            //get the world coord of the point which is 1 left localy from the defender
            Vector2 leftToTarget = rusher.Target.TransformPoint(Vector3.up * .5f);

            //modify the angle to rush outside
            float angle = Vector2.SignedAngle(leftToTarget - (Vector2)rusher.transform.position, new Vector2(1.0f, 0.0f));

            myRb.MoveRotation(Mathf.LerpAngle(myRb.rotation, -angle, rusher.rotationSpeed * Time.deltaTime));
        }

        private void RushCentral(Rusher_Controller_FSM rusher)
        {
            //modify the angle to rush outside
            float angle = Vector2.SignedAngle(rusher.Target.position - rusher.transform.position, new Vector2(1.0f, 0.0f));

            myRb.MoveRotation(Mathf.LerpAngle(myRb.rotation, -angle, rusher.rotationSpeed * Time.deltaTime));
        }

        private RushMode getRandomRushmode()
        {
            int value = Random.Range(0, 3);
            return (RushMode)value;
        }

        enum RushMode { Inside, Outside, Central }
    }
}
