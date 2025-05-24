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
        internal List<Point> points;
        [HideInInspector] public float totalLength;

        [Header("Path Properties:")] [Min(1)] public int resolution = 64;
        [SerializeField] private bool isClose;
        public bool autoSetControlPoints = true;
        [Header("Visual")] public bool controlPreview = true;

        public int NumAnchors => anchorPoints.Length;

        public int NumSegments
        {
            get { return isClose ? anchorPoints.Length : anchorPoints.Length - 1; }
        }

        public int NumPoints
        {
            get { return points.Count; }
        }

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

                Gizmos.color = PathConfig.forwardAxisColor;
                Gizmos.DrawLine(points[i].position, points[i].Forward + points[i].position);
                Gizmos.color = PathConfig.upAxisColor;
                Gizmos.DrawLine(points[i].position, points[i].upward * .1f + points[i].position);
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
            {
                new Point(anchorPoints[0].position, anchorPoints[0].transform.up)
                {
                    distance = 0,
                    Forward = anchorPoints[0].transform.forward
                }
            };

            totalLength = 0;

            Vector3 previousPoint = points[0].position;
            for (int i = 0; i < anchorPoints.Length - 1; i++)
            {
                float t = 0;
                while (t < 1)
                {
                    t += 1f / resolution;
                    var position = Bezier.CubicCurve(anchorPoints[i].position, anchorPoints[i].EndTangent,
                        anchorPoints[i + 1].StartTangent, anchorPoints[i + 1].position, t);
                    totalLength += Vector3.Distance(previousPoint, position);

                    var upward = Bezier.Lerp(anchorPoints[i].transform.up, anchorPoints[i + 1].transform.up, t);
                    var forward = (position - previousPoint).normalized;
                    points[points.Count - 1].Forward = forward;

                    Point point = new Point(position, upward)
                    {
                        distance = totalLength
                    };
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
                        anchorPoints[anchorPoints.Length - 1].EndTangent,
                        anchorPoints[0].StartTangent, anchorPoints[0].position, t);

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
            if (points.Count < 2) return points[0];
            var nearestPointIndex = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].distance > distance)
                {
                    nearestPointIndex = i;
                    break;
                }
            }

            Point currPoint, nextPoint;
            if (isClose && nearestPointIndex == 0)
            {
                currPoint = points[points.Count - 1];
                nextPoint = points[0];
            }
            else if (isClose && nearestPointIndex == points.Count - 1)
            {
                currPoint = points[points.Count - 2];
                nextPoint = points[points.Count - 1];
            }
            else
            {
                currPoint = points[nearestPointIndex - 1];
                nextPoint = points[nearestPointIndex];
            }
            
            var localDistance = distance - currPoint.distance;
            var deltaDistance = nextPoint.distance - currPoint.distance;

            var t = localDistance / deltaDistance;
            return new Point(Vector3.Lerp(currPoint.position, nextPoint.position, t))
            {
                distance = distance,
                Forward = Vector3.Slerp(currPoint.Forward, nextPoint.Forward, t),
                upward = Vector3.Slerp(currPoint.upward, nextPoint.upward, t),
            };
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
            get { return points.Count > 0 ? points[i] : null; }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Path))]
    public class PathEditor : Editor
    {
        private Path path;

        private void OnEnable()
        {
            path = (Path) target;
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
                        var fmh_338_77_638498331781866475 = Quaternion.identity; newPos = Handles.FreeMoveHandle(point.StartTangent,
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
                        var fmh_362_75_638498331781872871 = Quaternion.identity; newPos = Handles.FreeMoveHandle(point.EndTangent,
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
    }
#endif
}