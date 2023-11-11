using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
namespace Moein.Core
{
    public class AutoMotion : MonoBehaviour
    {
        public enum MotionType
        {
            Rotation,
            Location
        }

        public bool isMotion, isLocal;
        public Vector3 direction;
        public float speed;

        [SerializeField] MotionType motionType;

#if UNITY_EDITOR && !ENABLE_INPUT_SYSTEM
        [SerializeField] KeyCode StartActionKey = KeyCode.G;
        [SerializeField] KeyCode StopActionKey = KeyCode.H;
        [SerializeField] KeyCode ReverseActionKey = KeyCode.F;
#endif

        private void Update()
        {

#if UNITY_EDITOR && !ENABLE_INPUT_SYSTEM
            if (Input.GetKeyUp(StartActionKey))
            {
                StartAction();
            }

            if (Input.GetKeyUp(StopActionKey))
            {
                StopAction();
            }

            if (Input.GetKeyUp(ReverseActionKey))
            {
                Reverse();
            }
#endif
            if (isMotion) Action();
        }

        public void StartAction()
        {
            isMotion = true;
        }

        public void StopAction()
        {
            isMotion = false;
        }

        public void Reverse()
        {
            direction = -direction;
        }

        private void Action()
        {
            switch (motionType)
            {
                case MotionType.Location:
                    transform.Translate(direction * speed * Time.deltaTime, isLocal ? Space.Self : Space.World);
                    break;
                case MotionType.Rotation:
                    transform.Rotate(direction * speed * Time.deltaTime, isLocal ? Space.Self : Space.World);
                    break;
            }
        }
    }
}