using UnityEngine;

namespace Moein.Mirror
{
    public abstract class MirrorObject : MonoBehaviour
    {
        public Transform target;
        public enum UpdateType // The available methods of updating are:
        {
            FixedUpdate, // Update in FixedUpdate.
            LateUpdate, // Update in LateUpdate.
            Update,
            ManualUpdate // user must call refresh method
        }

        public UpdateType updateType;

        protected void Start()
        {
            if (target == null)
            {
                Debug.LogError("IMirror is null. Add IMirror component to mirror object");
            }
        }

        private void Update()
        {
            if (updateType == UpdateType.Update)
            {
                Refresh();
            }
        }

        private void FixedUpdate()
        {
            if (updateType == UpdateType.FixedUpdate)
            {
                Refresh();
            }
        }

        private void LateUpdate()
        {
            if (updateType == UpdateType.LateUpdate)
            {
                Refresh();
            }
        }

        public virtual void Refresh()
        {
            Debug.Log("Mirror Object Refreshed.");
        }
    }
}
