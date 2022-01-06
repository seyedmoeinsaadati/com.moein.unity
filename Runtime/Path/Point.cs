using UnityEngine;

namespace Moein.Path
{
    [System.Serializable]
    public class Point
    {
        public Vector3 position;
        private Vector3 forward;
        public Vector3 upward;

        public Vector3 Forward
        {
            get => forward;
            set => forward = value * .05f;
        }

        public Point(Vector3 position, Vector3 forward, Vector3 upward)
        {
            this.position = position;
            this.forward = forward * .05f;
            this.upward = upward * .05f;
        }

        public Point(Vector3 position, Vector3 upward)
        {
            this.position = position;
            this.upward = upward * .05f;
        }

        public Point(Vector3 position)
        {
            this.position = position;
        }
    }
}