using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moein.Core
{
    public class GizmosPoint : MonoBehaviour
    {
        public float radius = 0.5f;
        public Color pointColor = Color.black, lineColor = Color.white;
        public Transform TransformToLine;
        public bool drawLine;

        void OnDrawGizmos()
        {
            Gizmos.color = pointColor;
            Gizmos.DrawSphere(transform.position, radius);

            if (TransformToLine != null && drawLine)
            {
                Gizmos.color = lineColor;
                Gizmos.DrawLine(transform.position, TransformToLine.position);
            }
        }
    }
}