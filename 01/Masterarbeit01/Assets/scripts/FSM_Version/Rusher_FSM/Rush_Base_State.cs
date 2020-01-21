using UnityEngine;

public abstract class Rush_Base_State
{
    public abstract void EnterState(Rusher_Controller_FSM rusher);

    public abstract void Update(Rusher_Controller_FSM rusher);

    public abstract void OnCollisionEnter(Rusher_Controller_FSM rusher, Collider2D col);

    public abstract void CleanUp(Rusher_Controller_FSM rusher);
}
