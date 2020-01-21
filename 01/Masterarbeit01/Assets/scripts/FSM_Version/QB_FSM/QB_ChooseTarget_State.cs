using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QB_ChooseTarget_State : QB_Base_State
{
    public override void CleanUp(QB_Controller_FSM qb)
    {
        
    }

    public override void EnterState(QB_Controller_FSM qb)
    {
        //for now just a hacky, random version
        /*int receiverIndex = Random.Range(0, qb.Receivers.Length);
        qb.Receiver_current = qb.Receivers[receiverIndex];

        qb.StartCoroutine(ExecuteThrow(qb));*/
    }

    public override void OnCollisionEnter(QB_Controller_FSM qb)
    {
        
    }

    public override void Update(QB_Controller_FSM qb)
    {
        
    }

    IEnumerator ExecuteThrow(QB_Controller_FSM qb)
    {
        yield return new WaitForSeconds(2);
        qb.TransitionToState(qb.throw_State);
    }
}
