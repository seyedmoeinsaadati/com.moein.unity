using UnityEngine;

namespace Moein.ComponentInterface
{
    public interface IAnimator
    {
        Animator Animator { get; }
        float Speed { get; set; }
        bool ApplyRootMotion { get; set; }
        void SetTrigger(int id);
        void SetTrigger(string name);
        void SetInteger(int id, int value);
        void SetInteger(string name, int value);
        void SetFloat(int id, float value);
        void SetFloat(string name, float value);
        void SetFloat(int id, float value, float dampTime, float deltaTime);
        void SetFloat(string name, float value, float dampTime, float deltaTime);
        void SetBool(int id, bool value);
        void SetBool(string name, bool value);
    }
}