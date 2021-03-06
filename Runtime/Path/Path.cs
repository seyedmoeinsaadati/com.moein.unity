using System;
using System.Collections.Generic;
using Moein.Core;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Moein.Path
{
    [Serializable]
    public class Path : MonoBehaviour
    {
        [HideInInspector] public ControlPoint[] anchorPoints;
        [HideInInspector] public List<Point> points;
        [HideInInspector] public float totalLength;

        [Header("Path Properties:")] [Min(1)] public int resolution = 64;
        [SerializeField] private float power = 1;
        [SerializeField] private bool isClose;
        public bool autoSetControlPoints = true;
        [Header("Visual")] public bool controlPreview = true;

        public int NumAnchors => anchorPoints.Length;

        public int NumSegments
        {
            get { return isClose ? anchorPoints.Length : anchorPoints.Length - 1; }
        }

        public int NumPoints { get { return points.Count; } }

        public float TotalLength => totalLength;

        public Vector3 Forward
        {
            get { return points[0].Forward; }
        }

        public Point StartPoint
        {
            get { return points[0]; }
        }

        public void Create()
        {
            LoadAnchorPoints();
            foreach (var controlPoint in anchorPoints)
            {
                controlPoint.Init(this);
            }

            CalculatePoints();
            Debug.Log("Path created.");
        }

        public void LoadAnchorPoints()
        {
            anchorPoints = GetComponentsInChildren<ControlPoint>();
            for (int i = 0; i < anchorPoints.Length; i++)
            {
                anchorPoints[i].name = "Point " + i;
            }

            if (anchorPoints.Length > 0) anchorPoints[0].transform.localPosition = Vector3.zero;
        }

        private void Reset()
        {
            Create();
        }

        #region Gizmos

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            LoadAnchorPoints();
            CalculatePoints();
            if (anchorPoints.Length > 1) DrawPath();
            if (points.Count > 0) DrawPoints();
        }
#endif
        private void DrawPath()
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                Gizmos.color = PathConfig.pathColor;
                Gizmos.DrawLine(points[i].position, points[i + 1].position);

                Gizmos.color = PathConfig.zAxis;
                Gizmos.DrawLine(points[i].position, points[i].Forward + points[i].position);
                Gizmos.color = PathConfig.yAxis;
                Gizmos.DrawLine(points[i].position, points[i].upward + points[i].position);
            }
        }

        private void DrawPoints()
        {
            for (int i = 0; i < points.Count; i++)
            {
                Gizmos.color = PathConfig.pointColor;
                Gizmos.DrawSphere(points[i].position, PathConfig.pointDiameter);
            }
        }

        #endregion

        #region Points

        public void CalculatePoints()
        {
            if (NumAnchors < 1)
            {
                points = new List<Point>();
                return;
            }

            points = new List<Point>
                {new Point(anchorPoints[0].position, anchorPoints[0].transform.forward, anchorPoints[0].transform.up)};
            totalLength = 0;

            Vector3 previousPoint = points[0].position;
            for (int i = 0; i < anchorPoints.Length - 1; i++)
            {
                float t = 0;
                while (t < 1)
                {
                    t += 1f / resolution;
                    var position = Bezier.CubicCurve(anchorPoints[i].position, anchorPoints[i].EndTangent * power,
                        anchorPoints[i + 1].StartTangent * power, anchorPoints[i + 1].position, t);
                    totalLength += Vector3.Distance(previousPoint, position);

                    var upward = Bezier.Lerp(anchorPoints[i].transform.up, anchorPoints[i + 1].transform.up, t);
                    var forward = (position - previousPoint).normalized;
                    points[points.Count - 1].Forward = forward;

                    Point point = new Point(position, upward);
                    points.Add(point);

                    previousPoint = position;
                }
            }

            if (isClose)
            {
                float t = 0;
                while (t < 1)
                {
                    t += 1f / resolution;

                    var pointOnCurve = Bezier.CubicCurve(anchorPoints[anchorPoints.Length - 1].position,
                        anchorPoints[anchorPoints.Length - 1].EndTangent * power,
                        anchorPoints[0].StartTangent * power, anchorPoints[0].position, t);

                    totalLength += Vector3.Distance(previousPoint, pointOnCurve);

                    var upward = Bezier.Lerp(anchorPoints[anchorPoints.Length - 1].transform.up,
                        anchorPoints[0].transform.up, t);
                    var forward = (pointOnCurve - previousPoint).normalized;
                    points[points.Count - 1].Forward = forward;

                    Point point = new Point(pointOnCurve, upward);
                    points.Add(point);

                    previousPoint = pointOnCurve;
                }
            }
        }

        #endregion

        private void AutoSetAllControlPoints()
        {
            for (int i = 0; i < anchorPoints.Length; i++)
            {
                anchorPoints[i].AutoSetAffectedControlPoints();
            }
        }

        public Point GetPoint(float distance)
        {
            float dst = 0;
            distance %= totalLength;
            for (int i = 0; i < points.Count; i++)
            {
                dst += Vector3.Distance(points[i].position, points[i + 1].position);
                if (dst > distance)
                {
                    return points[i];
                }
            }

            return null;
        }

        public Point GetNearestPoint(Vector3 position)
        {
            float minDst = float.MaxValue;
            int index = 0;
            for (int i = 0; i < points.Count; i++)
            {
                float dst = Vector3.Distance(points[i].position, position);
                if (dst < minDst)
                {
                    index = i;
                    minDst = dst;
                }
            }

            return points[index];
        }

        public float GetDistanceFromNearestPoint(Vector3 position)
        {
            float minDst = float.MaxValue;
            for (int i = 0; i < points.Count; i++)
            {
                float dst = Vector3.Distance(points[i].position, position);
                if (dst < minDst) minDst = dst;
            }

            return minDst;
        }

        public Point this[int i]
        {
            get
            {
                if (points.Count > 0)
                {
                    return points[i];
                }
                return null;
            }
        }

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Path))]
    public class PathEditor : Editor
    {
        private Path path;

        private void OnEnable()
        {
            path = (Path)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (path.NumAnchors < 2)
                EditorGUILayout.HelpBox("Add two ControlPoint object to create path", MessageType.Info);

            if (GUILayout.Button("Add Control Point"))
            {
                AddControlPoint();
            }

            GUILayout.Label("Length : " + path.totalLength);


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

        private void AddControlPoint()
        {
            GameObject cPoint = new GameObject();
            cPoint.AddComponent<ControlPoint>().Init(path).transform.SetParent(path.transform);
            cPoint.transform.localPosition = path.NumAnchors > 0 ? path.transform.localPosition : Vector3.zero;
            Undo.RegisterCreatedObjectUndo(cPoint, "Add Control Point");
        }

        private void OnSceneGUI()
        {
            path.LoadAnchorPoints();
            if (path.controlPreview)
            {
                ControlTangents();
                DrawTangentLines();
            }
        }

        private void ControlTangents()
        {
            Handles.color = PathConfig.controlColor;
            for (int i = 0; i < path.anchorPoints.Length; i++)
            {
                var point = path.anchorPoints[i];
                Vector3 newPos;
                switch (PathConfig.controlType)
                {
                    case PathConfig.ControlType.FreeMove:
                        newPos = Handles.FreeMoveHandle(point.StartTangent, Quaternion.identity,
                            PathConfig.controlDiameter,
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
                    if (path.autoSetControlPoints)
                        path.anchorPoints[i].AutoSetEndTangent();
                    point.StartTangent = newPos;
                }

                switch (PathConfig.controlType)
                {
                    case PathConfig.ControlType.FreeMove:
                        newPos = Handles.FreeMoveHandle(point.EndTangent, Quaternion.identity,
                            PathConfig.controlDiameter,
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
                    if (path.autoSetControlPoints)
                        path.anchorPoints[i].AutoSetStartTangent();
                    point.EndTangent = newPos;
                }
            }
        }

        private void DrawTangentLines()
        {
            for (int i = 0; i < path.anchorPoints.Length; i++)
            {
                Handles.color = PathConfig.controlLineColor;
                Handles.DrawLine(path.anchorPoints[i].position, path.anchorPoints[i].StartTangent);
                Handles.DrawLine(path.anchorPoints[i].position, path.anchorPoints[i].EndTangent);
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
                return position.To3XY(defValue.z);
            }

            if (guiEvent.control)
            {
                return position.To3XZ(defValue.y);
            }

            if (guiEvent.alt)
            {
                return position.To3YZ(defValue.x);
            }

            return position;
        }
    }
#endif
}