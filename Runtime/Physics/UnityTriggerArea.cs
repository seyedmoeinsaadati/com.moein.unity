using UnityEngine;
using UnityEngine.Events;

namespace Moein.Physics
{
    public class UnityTriggerArea : TriggerArea
    {
        public UnityEvent OnEnter, OnExit, OnStay;

        public override void Enter(Collider other)
        {
            base.Enter(other);
            if (OnEnter != null)
            {
                OnEnter.Invoke();
            }
        }

        public override void Exit(Collider other)
        {
            base.Exit(other);
            if (OnExit != null)
            {
                OnExit.Invoke();
            }
        }

        public override void Stay(Collider other)
        {
            base.Stay(other);
            if (OnStay != null)
            {
                OnStay.Invoke();
            }
        }

        public void Destroy()
        {
            Destroy(this.gameObject);
        }
    }
}