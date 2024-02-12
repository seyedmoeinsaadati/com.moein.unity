using UnityEngine;

namespace Moein.Physics
{
    public class TriggerArea : MonoBehaviour
    {
        public bool oneTimeUse;
        public bool active = true, enterEnable = true, exitEnable = true;

        [Tooltip("if checked, it just works for \"Player\" tag.")]
        public bool onlyPlayer;

#if UNITY_EDITOR
        [Space] public bool debugMode;
#endif

        private void OnTriggerEnter(Collider other)
        {
            if (!active) return;
#if UNITY_EDITOR
            if (debugMode) Debug.Log(other.name + "Entered.");
#endif
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
#if UNITY_EDITOR
            if (debugMode) Debug.Log(other.name + "Exited.");
#endif
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
#if UNITY_EDITOR
            if (debugMode) Debug.Log(other.name + "Staying.");
#endif
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