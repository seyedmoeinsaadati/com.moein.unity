using UnityEngine;

namespace Moein.Physics
{
    public class TriggerArea : MonoBehaviour
    {
        public bool oneTimeUse;
        public bool debugMode;
        public bool active = true, enterEnable = true, exitEnable = true;

        [Tooltip("if checked, it just works for \"Player\" tag.")]
        public bool onlyPlayer;

        private void OnTriggerEnter(Collider other)
        {
            if (!active) return;
            if (debugMode) Debug.Log(other.name + "Entered.");
            if (enterEnable)
            {
                if (onlyPlayer && other.CompareTag("Player"))
                {
                    if (oneTimeUse) active = false;
                    Enter(other);
                }

                if (!onlyPlayer)
                {
                    if (oneTimeUse) active = false;
                    Enter(other);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!active) return;
            if (debugMode) Debug.Log(other.name + "Exited.");
            if (exitEnable)
            {
                if (onlyPlayer && other.CompareTag("Player"))
                {
                    if (oneTimeUse) active = false;
                    Exit(other);
                }

                if (!onlyPlayer)
                {
                    if (oneTimeUse) active = false;
                    Exit(other);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!active) return;
            if (debugMode) Debug.Log(other.name + "Staying.");
            if (exitEnable)
            {
                if (onlyPlayer && other.CompareTag("Player"))
                {
                    Stay(other);
                }

                if (!onlyPlayer)
                {
                    Stay(other);
                }
            }
        }

        public virtual void Enter(Collider other) { }
        public virtual void Exit(Collider other) { }
        public virtual void Stay(Collider other) { }
    }
}