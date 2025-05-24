using UnityEngine;

namespace Moein.Mirror
{
    public class MirrorAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SyncAnimator target;

        public void SetTarget(SyncAnimator target)
        {
            if (this.target != null)
                this.target.Remove(animator);
            this.target = target;
            target.Add(animator);
        }

        private void OnEnable()
        {
            if (target == null) return;
            target.Add(animator);
        }

        private void OnDisable()
        {
            if (target == null) return;
            target.Remove(animator);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            animator = GetComponent<Animator>();
        }
#endif
    }
}