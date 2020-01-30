using UnityEngine;

namespace BlockingEnhancements
{
    public abstract class OLinerBaseState
    {
        public abstract void EnterState(OLinerBaseState player);

        public abstract void Update(OLinerBaseState player);

        public abstract void OnCollisionEnter(OLinerBaseState player);

        public abstract void CleanUp(OLinerBaseState player);
    }
}

