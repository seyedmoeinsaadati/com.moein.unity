using System;
using System.Collections;
using UnityEngine;

namespace Moein.Trans.Follower
{
    public abstract class TransformFollower : MonoBehaviour
    {
        public Transform target;
        public float followSmoothSpeed; // refrence speed
        public Vector3 offset;

        [SerializeField]
        protected float smoothSpeed;

        public event Action onTargetChange;
        protected void Start()
        {
            smoothSpeed = followSmoothSpeed;
            if (target != null)
            {
                SetOffset();
            }

        }
        void Update()
        {
            Follow();
        }

        void FixedUpdate()
        {
            FixedFollow();
        }

        protected abstract void Follow();

        protected abstract void FixedFollow();

        public abstract void SetOffset();

        public void ChangeTarget(Transform newTarget)
        {
            target = newTarget;
            OnTargetChange();
        }

        private void OnTargetChange()
        {
            onTargetChange?.Invoke();
        }

        IEnumerator LerpSpeed(float endValue, float duration = 1)
        {
            smoothSpeed = 0;
            float elapsed = 0;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                smoothSpeed = Mathf.Lerp(smoothSpeed, followSmoothSpeed, Time.deltaTime / duration);

                yield return null;
            }
            smoothSpeed = followSmoothSpeed;
        }
    }
}