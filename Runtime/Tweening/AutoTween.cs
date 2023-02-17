using UnityEngine;

namespace Moein.Tweening
{
    public class AutoTween : MonoBehaviour
    {
        public enum TweenType
        {
            Rotation,
            Location,
            Scale
        }

        public bool isMotion, isLocal;
        public Vector3 targetValue;
        public float duration;
        public Ease ease;
        [SerializeField] TweenType tweenType;
        [SerializeField] KeyCode StartActionKey = KeyCode.G;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyUp(StartActionKey))
            {
                if (isMotion == false) Action();
            }
        }

        private void Action()
        {
            isMotion = true;
            switch (tweenType)
            {
                case TweenType.Location:
                    this.DOPosition(transform, targetValue, duration, 0, ease, isLocal, () =>
                    {
                        isMotion = false;
                        print("Position Tweening Compeleted.");
                    });
                    break;
                case TweenType.Rotation:
                    this.DORotation(transform, targetValue, duration, 0, ease, isLocal, () =>
                    {
                        isMotion = false;
                        print("Rotation Tweening Compeleted.");
                    });
                    break;
                case TweenType.Scale:
                    this.DoScale(transform, targetValue, duration, 0, ease, () =>
                    {
                        isMotion = false;
                        print("Scale Tweening Compeleted.");
                    });
                    break;
            }
        }
    }
}