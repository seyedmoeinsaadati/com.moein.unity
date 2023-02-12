using UnityEngine;

namespace Moein.Trans.Mirror
{
    public class MirrorTransform : MirrorObject
    {
        [Tooltip("IMirror Component is prerequisites")]
        public Transform mirror;
        IMirrorTransform _iMirrorTransform;

        Quaternion newRotation;
        Vector3 newPosition, newScale;

        new void Start()
        {
            base.Start();
            _iMirrorTransform = mirror.GetComponent<IMirrorTransform>();
            if (_iMirrorTransform == null)
            {
                Debug.LogError("IMirror is null. Add IMirror component to mirror object");
            }
        }

        public override void Refresh()
        {
            if (_iMirrorTransform == null)
            {
                Debug.LogError("IMirror is null. Add IMirror component to mirror object");
                return;
            }
            newPosition = _iMirrorTransform.GetMirrorPosition(target.position);
            newRotation = _iMirrorTransform.GetMirrorRotation(target.rotation);
            newScale = _iMirrorTransform.GetMirrorScale(target.localScale);
            transform.position = newPosition;
            transform.rotation = newRotation;
            transform.localScale = newScale;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        public void Show()
        {
            gameObject.SetActive(true);
        }

        void OnDisable()
        {
            Debug.Log(name + " Disabled.");
        }


    }
}