using UnityEngine;


namespace Moein.Recorder
{
    [System.Serializable]
    public class SerializableVector3
    {
        float x, y, z;

        public SerializableVector3(Vector3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vector3 getVector3()
        {
            return new Vector3(x, y, z);
        }
    }

    [System.Serializable]
    public class TransformInfo
    {
        protected SerializableVector3 position;
        protected SerializableVector3 rotationEulerAngles;

        public TransformInfo(Vector3 position, Quaternion rotation)
        {
            this.position = new SerializableVector3(position);
            this.rotationEulerAngles = new SerializableVector3(rotation.eulerAngles);
        }

        public Vector3 GetPosition()
        {
            return position.getVector3();
        }

        public Vector3 GetEularAngles()
        {
            return rotationEulerAngles.getVector3();
        }
    }
}