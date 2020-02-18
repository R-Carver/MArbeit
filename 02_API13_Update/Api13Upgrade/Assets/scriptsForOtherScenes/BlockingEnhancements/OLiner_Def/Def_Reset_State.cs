//this state is there for giving the other objects some time after the reset before the colliders start to 
//be active. So just don't do anything here but change into the Passblock state after a short delay
 using System.Collections;
using UnityEngine;

namespace BlockingEnhancements
{
public class Def_Reset_State : DefenderBaseState
{
    public override void CleanUp(DefenderController_FSM player)
    {
        
    }

    public override void EnterState(DefenderController_FSM player)
    {
        player.StartCoroutine(FinishResetDelay(player));
    }

    public override void OnCollisionEnter(DefenderController_FSM player)
    {
        
    }

    public override void Update(DefenderController_FSM player)
    {
        
    }

    private IEnumerator FinishResetDelay(DefenderController_FSM player)
    {   
        yield return new WaitForSeconds(0.5f);

        player.TransitionToState(player.passBlock_State);
    }
}
}