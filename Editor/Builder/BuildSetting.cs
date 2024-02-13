using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Moein.Core
{
    public class BuildSetting : SingletonScriptableObject<BuildSetting>
    {
        [System.Serializable]
        public class BuildScenesAsset
        {
            public string name;
            public List<SceneAsset> scenes = new List<SceneAsset>();
        }

        [Header("Build Scenes")]
        [SerializeField] private List<BuildScenesAsset> sceneBundles = new List<BuildScenesAsset>();
        [SerializeField] private int currentSceneBundleIndex = 0;

        internal static void SetEditorBuildSettingsScenes()
        {
            List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();

            if (BuildSetting.Instance.currentSceneBundleIndex < 0 ||
                BuildSetting.Instance.currentSceneBundleIndex >= BuildSetting.Instance.sceneBundles.Count)
                return;

            var sceneBundle = BuildSetting.Instance.sceneBundles[BuildSetting.Instance.currentSceneBundleIndex];
            foreach (var scene in sceneBundle.scenes)
            {
                var path = AssetDatabase.GetAssetPath(scene);
                if (!string.IsNullOrEmpty(path))
                    editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(path, true));
            }
            EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
        }

        [MenuItem("Moein/Build Setting")]
        public static void SelectConfig()
        {
            Selection.activeObject = BuildSetting.Instance;
            return;
        }
    }

    [CustomEditor(typeof(BuildSetting))]
    public class SceneConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);
            if (GUILayout.Button("Apply To Build Settings"))
            {
                BuildSetting.SetEditorBuildSettingsScenes();
            }
        }
    }

}