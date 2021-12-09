using UnityEditor;
using static UnityEditor.SceneView;

namespace Moein.Core
{
    public class DrawMode
    {
        [MenuItem("Moein/Core/Wireframe Mode &w", false)]
        public static void ChangeDrawCameraMode()
        {
            if (SceneView.lastActiveSceneView == null) return;

            if (SceneView.lastActiveSceneView.cameraMode.drawMode == DrawCameraMode.Wireframe)
            {
                SceneView.lastActiveSceneView.cameraMode = GetBuiltinCameraMode(DrawCameraMode.Textured);
            }
            else
            {
                SceneView.lastActiveSceneView.cameraMode = GetBuiltinCameraMode(DrawCameraMode.Wireframe);
            }
        }
    }
}