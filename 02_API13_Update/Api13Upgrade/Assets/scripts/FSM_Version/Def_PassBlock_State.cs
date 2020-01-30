using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Def_PassBlock_State : DefenderBaseState
{   
    int frameCounter;

    public override void CleanUp(DefenderController_FSM player)
    {
        
    }

    public override void EnterState(DefenderController_FSM player)
    {   
        //on reset the joint should be disabled
        player.joint.enabled = false;

        frameCounter = 20;
    }

    public override void OnCollisionEnter(DefenderController_FSM player)
    {
        
    }

    public override void Update(DefenderController_FSM player)
    {
        //move first object
        float angle = Vector2.SignedAngle(player.otherObject.position - player.transform.position, new Vector2(1.0f, 0.0f));
        player.myRb.MoveRotation(-angle);

        if(frameCounter == 0)
        {   
            //for the cross product we need to compare the vectors from the QB to the Receiver
            //to the Vector from the QB to the Defender
            Vector2 defenderQbVector = player.myRb.transform.position - player.QBtransform.position;
            Vector2 receiverQbVector = player.otherObject.transform.position - player.QBtransform.position;

            if(IsLeft(defenderQbVector, receiverQbVector))
            {
                player.myRb.AddRelativeForce(Vector2.up * player.speed);
                //Debug.Log("Is left");
            }else
            {
                player.myRb.AddRelativeForce(Vector2.up * - player.speed);
                //Debug.Log("Is right");
            }
            frameCounter = 20;    
        }else
        {
            frameCounter--;
        }

        if(player.colliders.Length > 0)
        {
            player.TransitionToState(player.snap_State);
        }
    }

    bool IsLeft(Vector2 A, Vector2 B)
    {
        return (-A.x * B.y + A.y * B.x) < 0;
    }

}
