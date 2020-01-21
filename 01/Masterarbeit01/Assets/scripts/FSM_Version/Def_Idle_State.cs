using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Def_Idle_State : DefenderBaseState
{

    public override void EnterState(DefenderController_FSM player)
    {
        player.StartCoroutine(BeIdle_Cor(player));

    }

    public override void OnCollisionEnter(DefenderController_FSM player)
    {
        
    }

    public override void Update(DefenderController_FSM player)
    {

    }

    public override void CleanUp(DefenderController_FSM player)
    {

    }

    private IEnumerator BeIdle_Cor(DefenderController_FSM player)
    {
        yield return new WaitForSeconds(1f);

        player.TransitionToState(player.passBlock_State);
        
    }
}
