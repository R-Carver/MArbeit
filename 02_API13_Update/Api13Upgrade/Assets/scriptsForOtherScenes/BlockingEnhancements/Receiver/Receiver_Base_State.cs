using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockingEnhancements
{
public abstract class Receiver_Base_State 
{
    public abstract void EnterState(Receiver_Controller_FSM receiver);
    public abstract void Update(Receiver_Controller_FSM receiver);
    public abstract void OnCollisionEnter(Receiver_Controller_FSM receiver, Collider2D col);
    public abstract void CleanUp(Receiver_Controller_FSM receiver);
}
}