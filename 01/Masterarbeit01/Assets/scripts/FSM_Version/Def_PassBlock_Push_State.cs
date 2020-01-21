using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Def_PassBlock_Push_State : DefenderBaseState
{
    public override void EnterState(DefenderController_FSM player)
    {
        //player.StartCoroutine(DuellRotateTest_Cor(player));

        //this is for preventing the angular velocity problem
        player.myRb.isKinematic = true;
        player.myRb.angularVelocity = 0.0f;
        player.otherRb.angularVelocity = 0.0f;
    }

    public override void OnCollisionEnter(DefenderController_FSM player)
    {
        
    }

    //for now get the probs for the next action every couple of frames
    int frameCounter = 50;

    public override void Update(DefenderController_FSM player)
    {   
        player.myRb.AddRelativeForce(Vector2.right * player.pushPower * Time.fixedDeltaTime);

        //call the actions based on the probs
        if(frameCounter == 0)
        {   
            //this is for preventing the angular velocity problem
            player.myRb.isKinematic = false;

            float prob = GetNaiveActionProb();
            if(prob <= 0.5f)
            {   
                //Default Action
                DuellRotate(player);
                
            }
            else if(prob <= 0.8f)
            {
                //Push Action

                // 1.) disable the joint
                player.joint.enabled = false;

                //2.) Push the other rb away
                player.otherRb.AddRelativeForce(Vector2.left * 2);

                //3.) change to the PassBlock state
                player.TransitionToState(player.idle_state);
            }
            else
            {
                //Knock Over Action
                player.TransitionToState(player.down_state);
            }
            frameCounter = 50;
            
        }else
        {
            frameCounter --;
        }
        

    }

    /*private IEnumerator DuellRotateTest_Cor(DefenderController_FSM player)
    {
        yield return new WaitForSeconds(0.3f);

        //try to rotate just a little bit to simulate a arm battle
        //player.myRb.MoveRotation(Mathf.LerpAngle(player.myRb.rotation, player.myRb.rotation + 50, 2));
        float rotatePower = Random.Range(-1.0f, 1.0f);

        player.myRb.AddTorque(rotatePower * 500, ForceMode2D.Force);

        player.StartCoroutine(DuellRotateTest_Cor(player));
        
    }*/

    private void DuellRotate(DefenderController_FSM player)
    {
        float rotatePower = Random.Range(-1.0f, 1.0f);

        player.myRb.AddTorque(rotatePower * 500, ForceMode2D.Force);
    }

    private float GetNaiveActionProb()
    {
        //for now the probs are: Default: 0.5 | Push: 0.3 | KnockOver: 0.2
        return Random.Range(0.0f, 1.0f);
    }

    public override void CleanUp(DefenderController_FSM player)
    {   
        player.myRb.angularVelocity = 0.0f;
    }
}
