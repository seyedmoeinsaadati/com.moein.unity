using UnityEngine;

namespace Moein.Mirror
{
    public class MirrorAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SyncAnimator target;

        private void Reset()
        {
            animator = GetComponent<Animator>();
        }

        public void SetTarget(SyncAnimator target)
        {
            if (this.target != null)
                this.target.Remove(animator);
            this.target = target;
            target.Add(animator);
        }

        private void OnEnable()
        {
            target.Add(animator);
        }

        private void OnDisable()
        {
            target.Remove(animator);
        }
    }
}