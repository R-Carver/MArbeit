using UnityEngine;

namespace BlockingEnhancements
{
public abstract class DefenderBaseState
{
    public abstract void EnterState(DefenderController_FSM player);

    public abstract void Update(DefenderController_FSM player);

    public abstract void OnCollisionEnter(DefenderController_FSM player);

    public abstract void CleanUp(DefenderController_FSM player);
}

}
