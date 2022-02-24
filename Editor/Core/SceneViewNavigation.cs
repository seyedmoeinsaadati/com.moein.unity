using UnityEditor;
using UnityEngine;
using static UnityEditor.SceneView;

namespace Moein.Core
{
    public static class SceneViewNavigation
    {
        public static bool RotationLock
        {
            get => lastActiveSceneView.isRotationLocked;
            set => lastActiveSceneView.isRotationLocked = value;
        }

        public static bool Orthographic
        {
            get => lastActiveSceneView.orthographic;
            set => lastActiveSceneView.orthographic = value;
        }

        [MenuItem("Moein/View/Toggle Projection")]
        public static void ToggleProjection()
        {
            if (lastActiveSceneView == null) return;
            Orthographic = !Orthographic;
            RepaintAll();
        }

        [MenuItem("Moein/View/Lock Rotation")]
        public static void LockRotation()
        {
            if (lastActiveSceneView == null) return;
            RotationLock = !RotationLock;
            RepaintAll();
        }
    }
}