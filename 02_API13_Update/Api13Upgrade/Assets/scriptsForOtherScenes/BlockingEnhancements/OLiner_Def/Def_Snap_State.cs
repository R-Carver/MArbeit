using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockingEnhancements
{
public class Def_Snap_State : DefenderBaseState
{

    public override void EnterState(DefenderController_FSM player)
    {
        
    }

    public override void OnCollisionEnter(DefenderController_FSM player)
    {
        
    }

    public override void Update(DefenderController_FSM player)
    {

        if (player.joint.enabled == false)
        {
            player.myRb.angularVelocity = 0.0f;
            player.otherRb.angularVelocity = 0.0f;
        }

        player.joint.enabled = true;
        
        player.TransitionToState(player.push_State);
    }

    public override void CleanUp(DefenderController_FSM player)
    {
        
    }
}

}
