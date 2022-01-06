using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Moein.Path
{
    public class ControlPoint : MonoBehaviour
    {
        [HideInInspector] public Path path;

        public Vector3 localStartTangent, localEndTangent;

        public Vector3 StartTangent
        {
            get { return localStartTangent + position; }
            set { localStartTangent = value - position; }
        }

        public Vector3 EndTangent
        {
            get { return localEndTangent + position; }
            set { localEndTangent = value - position; }
        }

        public Vector3 position => transform.position;

        public void Init(Path path)
        {
            this.path = path;
            localStartTangent = transform.up;
            localEndTangent = -transform.up;
        }

        public void AutoSetAffectedControlPoints()
        {
            AutoSetStartTangent();
            AutoSetEndTangent();
        }

        public void AutoSetStartTangent()
        {
            var a = EndTangent - position;
            var b = StartTangent - position;
            StartTangent = -a.normalized * b.magnitude + position;
        }

        public void AutoSetEndTangent()
        {
            var a = StartTangent - position;
            var b = EndTangent - position;
            EndTangent = -a.normalized * b.magnitude + position;
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = PathConfig.anchorColor;
            Gizmos.DrawSphere(position, PathConfig.anchorDiameter);
        }
#endif
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(ControlPoint)), CanEditMultipleObjects]
    public class ControlPointEditor : Editor
    {
        private ControlPoint point;

        private void OnEnable()
        {
            point = (ControlPoint) target;
        }

        private void OnSceneGUI()
        {
            ControlTangents();
            DrawTangentLines();
        }


        private void ControlTangents()
        {
            Handles.color = PathConfig.controlColor;
            Vector3 newPos;

            switch (PathConfig.controlType)
            {
                case PathConfig.ControlType.FreeMove:
                    newPos = Handles.FreeMoveHandle(point.StartTangent, Quaternion.identity, PathConfig.controlDiameter,
                        Vector3.zero, Handles.SphereHandleCap);
                    break;
                case PathConfig.ControlType.MoveHandle:
                    newPos = Handles.PositionHandle(point.StartTangent, Quaternion.identity);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (point.StartTangent != newPos)
            {
                Undo.RecordObject(point, "Move Start Tangent");
                if (point.path.autoSetControlPoints) point.AutoSetEndTangent();
                point.StartTangent = newPos;
            }

            switch (PathConfig.controlType)
            {
                case PathConfig.ControlType.FreeMove:
                    newPos = Handles.FreeMoveHandle(point.EndTangent, Quaternion.identity, PathConfig.controlDiameter,
                        Vector3.zero, Handles.SphereHandleCap);
                    break;
                case PathConfig.ControlType.MoveHandle:
                    newPos = Handles.PositionHandle(point.EndTangent, Quaternion.identity);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (point.EndTangent != newPos)
            {
                Undo.RecordObject(point, "Move End Tangent");
                if (point.path.autoSetControlPoints) point.AutoSetStartTangent();
                point.EndTangent = newPos;
            }
        }

        private void DrawTangentLines()
        {
            Handles.color = PathConfig.controlLineColor;
            Handles.DrawLine(point.position, point.StartTangent);
            Handles.DrawLine(point.position, point.EndTangent);
        }
    }

#endif
}