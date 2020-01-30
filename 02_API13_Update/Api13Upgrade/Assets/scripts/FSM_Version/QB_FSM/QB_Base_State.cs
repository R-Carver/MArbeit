using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QB_Base_State
{
    public abstract void EnterState(QB_Controller_FSM qb);
    public abstract void Update(QB_Controller_FSM qb);
    public abstract void OnCollisionEnter(QB_Controller_FSM qb);
    public abstract void CleanUp(QB_Controller_FSM qb);
}
