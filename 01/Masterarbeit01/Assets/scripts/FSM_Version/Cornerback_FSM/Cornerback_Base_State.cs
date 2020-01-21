using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cornerback_Base_State
{
    public abstract void EnterState(Cornerback_Controller_FSM cornerback);
    public abstract void Update(Cornerback_Controller_FSM cornerback);
    public abstract void OnCollisionEnter(Cornerback_Controller_FSM cornerback, Collision2D other);
    public abstract void OnCollisionExit(Cornerback_Controller_FSM cornerback, Collision2D other);

    public abstract void OnTriggerEnter(Cornerback_Controller_FSM cornerback, Collider2D other);
    public abstract void OnTriggerExit(Cornerback_Controller_FSM cornerback, Collider2D other);
    public abstract void CleanUp(Cornerback_Controller_FSM cornerback);
}
