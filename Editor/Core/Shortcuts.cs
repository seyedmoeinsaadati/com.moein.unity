using UnityEditor;
using UnityEngine;
using static UnityEditor.SceneView;

public class Shortcuts
{
    [MenuItem("Moein/Core/Wireframe Mode &w", false)]
    public static void ChangeDrawCameraMode()
    {
        if (lastActiveSceneView == null) return;

        if (lastActiveSceneView.cameraMode.drawMode == DrawCameraMode.Wireframe)
        {
            lastActiveSceneView.cameraMode = GetBuiltinCameraMode(DrawCameraMode.Textured);
        }
        else
        {
            lastActiveSceneView.cameraMode = GetBuiltinCameraMode(DrawCameraMode.Wireframe);
        }
    }

    [MenuItem("Moein/Core/Lock Inspector &q", false)]
    public static void InspectorLock()
    {
        ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
    }
}