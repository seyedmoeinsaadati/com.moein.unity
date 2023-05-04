using System;
using Moein.Core;
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

        public ControlPoint Init(Path path)
        {
            this.path = path;
            localStartTangent = transform.up;
            localEndTangent = -transform.up;
            return this;
        }

        public ControlPoint Init(Path path, Vector3 localStartTangent, Vector3 localEndTangent)
        {
            this.path = path;
            this.localStartTangent = localStartTangent;
            this.localEndTangent = localEndTangent;
            return this;
        }

        public ControlPoint Init(Path path, Vector3 tangentDirection)
        {
            this.path = path;
            this.localStartTangent = tangentDirection;
            this.localEndTangent = -tangentDirection;
            return this;
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

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (Event.current.shift)
            {
                GUILayout.Label("Lock (XY)");
            }
            else if (Event.current.control)
            {
                GUILayout.Label("Lock (XZ)");
            }
            else if (Event.current.alt)
            {
                GUILayout.Label("Lock (YZ)");
            }
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

            newPos = DetectLimitation(newPos, point.StartTangent);

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

            newPos = DetectLimitation(newPos, point.EndTangent);

            if (point.EndTangent != newPos)
            {
                Undo.RecordObject(point, "Move End Tangent");
                if (point.path.autoSetControlPoints) point.AutoSetStartTangent();

                point.EndTangent = newPos;
            }
        }


        /// <summary>
        /// return position(XY) if shift is pressed
        /// return position(XZ) if ctrl is pressed
        /// return position(YZ) if alt is pressed
        /// otherwise, return -1 
        /// </summary>
        /// <returns></returns>
        public Vector3 DetectLimitation(Vector3 position, Vector3 defValue)
        {
            Event guiEvent = Event.current;
            if (guiEvent.shift)
            {
                return position.ToXY(defValue.z);
            }

            if (guiEvent.control)
            {
                return position.ToXZ(defValue.y);
            }

            if (guiEvent.alt)
            {
                return position.ToYZ(defValue.x);
            }

            return position;
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