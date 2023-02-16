using UnityEngine;

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

        [SerializeField] KeyCode StartActionKey = KeyCode.G;
        [SerializeField] KeyCode StopActionKey = KeyCode.H;
        [SerializeField] KeyCode ReverseActionKey = KeyCode.F;

        private void Update()
        {
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