using UnityEngine;
using System.Collections.Generic;

namespace Moein.Modifier
{
    public class ArrayModifier : MonoBehaviour
    {
        [SerializeField] private Transform[] originals;
        [SerializeField] private Transform objectOffset;
        [SerializeField] private int count;

        [SerializeField] public float hierarchySmoothFactor = 1;
        [SerializeField] public float smoothSpeed = 1;

        private List<Transform> objects;

        private Vector3 scaleRatio, positionOffset;

        private void Start()
        {
            objects = new List<Transform>();
            CreateObjects();
        }

        public void CreateObjects()
        {
            objects.Clear();

            foreach (Transform original in originals)
                objects.Add(original);

            for (int i = originals.Length; i < count; i++)
            {
                var newObj = Instantiate(originals[i % originals.Length], originals[0].parent);
                objects.Add(newObj.transform);
            }
        }

        public void Update()
        {
            scaleRatio.x = objectOffset.localScale.x / objects[0].localScale.x;
            scaleRatio.y = objectOffset.localScale.y / objects[0].localScale.y;
            scaleRatio.z = objectOffset.localScale.z / objects[0].localScale.z;
            positionOffset = transform.TransformDirection(objectOffset.position - objects[0].position);

            UpdateTransforms();
        }

        private void UpdateTransforms()
        {
            float dt = Time.deltaTime * smoothSpeed;
            for (int i = 1; i < count; i++)
            {
                dt /= hierarchySmoothFactor;
                objects[i].position = Vector3.Lerp(objects[i].position, objects[i - 1].TransformPoint(positionOffset), dt);

                objects[i].localRotation = Quaternion.Slerp(objects[i].localRotation, objects[i - 1].localRotation * objectOffset.localRotation, dt);

                Vector3 localScale = transform.TransformVector(objectOffset.localScale);
                localScale.x = objects[i - 1].localScale.x * scaleRatio.x;
                localScale.y = objects[i - 1].localScale.y * scaleRatio.y;
                localScale.z = objects[i - 1].localScale.z * scaleRatio.z;
                objects[i].localScale = Vector3.Lerp(objects[i].localScale, localScale, dt);
            }
        }
    }
}