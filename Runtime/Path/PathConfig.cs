using UnityEngine;

namespace Moein.Path
{
    public static class PathConfig
    {
        public enum ControlType
        {
            FreeMove,
            MoveHandle
        }

        public static ControlType controlType = ControlType.FreeMove;
        public static Color pointColor = Color.black;
        public static Color anchorColor = Color.red;
        public static Color controlColor = Color.yellow;
        public static Color controlLineColor = Color.grey;
        public static Color pathColor = Color.blue;
        public static readonly float anchorDiameter = .075f;
        public static readonly float controlDiameter = .075f;
        public static float pointDiameter = 0.01f;
        public static bool displayControlPoints = true;

        public static Color xAxis = Color.red;
        public static Color yAxis = Color.green;
        public static Color zAxis = Color.blue;
    }
}