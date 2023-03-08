using System.Collections.Generic;
using log4net.Util;
using UnityEngine;

namespace Moein.Mirror
{
    public class SyncAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private List<Animator> syncAnimators = new List<Animator>();

        public Animator Animator => _animator;

        public float Speed
        {
            get { return _animator.speed; }
            set
            {
                _animator.speed = value;

                if (syncAnimators.Count > 0)
                {
                    for (int i = 0; i < syncAnimators.Count; i++)
                    {
                        syncAnimators[i].speed = value;
                    }
                }
            }
        }

        public bool ApplyRootMotion
        {
            get { return _animator.applyRootMotion; }
            set
            {
                _animator.applyRootMotion = value;

                if (syncAnimators.Count > 0)
                {
                    for (int i = 0; i < syncAnimators.Count; i++)
                    {
                        syncAnimators[i].applyRootMotion = value;
                    }
                }
            }
        }

        public void SetTrigger(int id)
        {
            _animator.SetTrigger(id);

            if (syncAnimators.Count > 0)
            {
                for (int i = 0; i < syncAnimators.Count; i++)
                {
                    syncAnimators[i].SetTrigger(id);
                }
            }
        }

        public void SetTrigger(string name)
        {
            _animator.SetTrigger(name);

            if (syncAnimators.Count > 0)
            {
                for (int i = 0; i < syncAnimators.Count; i++)
                {
                    syncAnimators[i].SetTrigger(name);
                }
            }
        }

        public void SetInteger(int id, int value)
        {
            _animator.SetInteger(id, value);

            if (syncAnimators.Count > 0)
            {
                for (int i = 0; i < syncAnimators.Count; i++)
                {
                    syncAnimators[i].SetInteger(id, value);
                }
            }
        }

        public void SetInteger(string name, int value)
        {
            _animator.SetInteger(name, value);

            if (syncAnimators.Count > 0)
            {
                for (int i = 0; i < syncAnimators.Count; i++)
                {
                    syncAnimators[i].SetInteger(name, value);
                }
            }
        }

        public void SetFloat(int id, float value)
        {
            _animator.SetFloat(id, value);

            if (syncAnimators.Count > 0)
            {
                for (int i = 0; i < syncAnimators.Count; i++)
                {
                    syncAnimators[i].SetFloat(id, value);
                }
            }
        }

        public void SetFloat(string name, float value)
        {
            _animator.SetFloat(name, value);

            if (syncAnimators.Count > 0)
            {
                for (int i = 0; i < syncAnimators.Count; i++)
                {
                    syncAnimators[i].SetFloat(name, value);
                }
            }
        }

        public void SetFloat(int id, float value, float dampTime, float deltaTime)
        {
            _animator.SetFloat(name, value);

            if (syncAnimators.Count > 0)
            {
                for (int i = 0; i < syncAnimators.Count; i++)
                {
                    syncAnimators[i].SetFloat(id, value, dampTime, deltaTime);
                }
            }
        }

        public void SetFloat(string name, float value, float dampTime, float deltaTime)
        {
            _animator.SetFloat(name, value);

            if (syncAnimators.Count > 0)
            {
                for (int i = 0; i < syncAnimators.Count; i++)
                {
                    syncAnimators[i].SetFloat(name, value, dampTime, deltaTime);
                }
            }
        }

        public void SetBool(int id, bool value)
        {
            _animator.SetBool(id, value);

            if (syncAnimators.Count > 0)
            {
                for (int i = 0; i < syncAnimators.Count; i++)
                {
                    syncAnimators[i].SetBool(id, value);
                }
            }
        }

        public void SetBool(string name, bool value)
        {
            _animator.SetBool(name, value);

            if (syncAnimators.Count > 0)
            {
                for (int i = 0; i < syncAnimators.Count; i++)
                {
                    syncAnimators[i].SetBool(name, value);
                }
            }
        }


        private void Reset()
        {
            _animator = GetComponent<Animator>();
        }

        public void Add(Animator animator)
        {
            if (animator == null) return;
            if (syncAnimators.Contains(animator) == false)
            {
                syncAnimators.Add(animator);
            }
        }

        public void Remove(Animator animator)
        {
            if (animator == null) return;
            if (syncAnimators.Contains(animator))
            {
                syncAnimators.Remove(animator);
            }
        }
    }
}