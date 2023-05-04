using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Moein.Path
{
    public static class PathConfig
    {
        public enum ControlType
        {
            FreeMove,
            MoveHandle
        }

        public static ControlType controlType = ControlType.MoveHandle;
        public static Color pointColor = Color.black;
        public static Color anchorColor = Color.red;
        public static Color controlColor = Color.yellow;
        public static Color controlLineColor = Color.grey;
        public static Color pathColor = Color.blue;
        public static float anchorDiameter = .075f;
        public static float controlDiameter = .075f;
        public static float pointDiameter = 0.01f;
        public static bool displayControlPoints = true;

        public static Color upAxisColor = Color.green;
        public static Color forwardAxisColor = Color.blue;
    }

#if UNITY_EDITOR

    public class PathConfigEditor : EditorWindow
    {
        [MenuItem("Moein/Path Config", false, 10)]
        public static void ShowWindow()
        {
            GetWindow<PathConfigEditor>("Path Config", true);
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            PathConfig.controlType =
                (PathConfig.ControlType) EditorGUILayout.EnumPopup("Control Type:", PathConfig.controlType);
            PathConfig.displayControlPoints =
                EditorGUILayout.Toggle("Display Control Point ", PathConfig.displayControlPoints);
            GUILayout.Space(10);
            GUILayout.Label("Colors:");
            PathConfig.anchorColor = EditorGUILayout.ColorField("Anchor Color:", PathConfig.anchorColor);
            PathConfig.controlColor = EditorGUILayout.ColorField("Control Color:", PathConfig.controlColor);
            PathConfig.pathColor = EditorGUILayout.ColorField("Path Color:", PathConfig.pathColor);
            PathConfig.pointColor = EditorGUILayout.ColorField("Point Color:", PathConfig.pointColor);
            PathConfig.controlLineColor =
                EditorGUILayout.ColorField("Control Line Color:", PathConfig.controlLineColor);
            GUILayout.Space(10);
            PathConfig.forwardAxisColor =
                EditorGUILayout.ColorField("Forward Axis Color:", PathConfig.forwardAxisColor);
            PathConfig.upAxisColor =
                EditorGUILayout.ColorField("Up Axis Color:", PathConfig.upAxisColor);
            GUILayout.Space(10);
            GUILayout.Label("Size:");
            PathConfig.controlDiameter = EditorGUILayout.FloatField("Control Diameter:", PathConfig.controlDiameter);
            PathConfig.anchorDiameter = EditorGUILayout.FloatField("Anchor Diameter:", PathConfig.anchorDiameter);
            PathConfig.pointDiameter = EditorGUILayout.FloatField("Point Diameter:", PathConfig.pointDiameter);
        }
    }

#endif
}