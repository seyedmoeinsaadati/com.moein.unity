using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Moein.Core
{
    public class BuildSetting : SingletonScriptableObject<BuildSetting>
    {
        [System.Serializable]
        public class BuildScenesAsset
        {
            public string name;
            public List<SceneAsset> scenes = new();
        }

        [Header("Build Scenes")]
        [SerializeField] private List<BuildScenesAsset> sceneBundles = new();
        [SerializeField] private int currentSceneBundleIndex = 0;

        [SerializeField] private string productVersion = "1.0.0";

        ///////////////////////////////////////
        /// STATIC MEMBERS
        ///////////////////////////////////////

        internal static bool SetEditorBuildSettingsScenes()
        {
            List<EditorBuildSettingsScene> editorBuildSettingsScenes = new();

            if (Instance.currentSceneBundleIndex < 0 ||
                Instance.currentSceneBundleIndex >= Instance.sceneBundles.Count)
                return false;

            var sceneBundle = BuildSetting.Instance.sceneBundles[Instance.currentSceneBundleIndex];
            foreach (var scene in sceneBundle.scenes)
            {
                var path = AssetDatabase.GetAssetPath(scene);
                if (!string.IsNullOrEmpty(path))
                    editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(path, true));
            }
            EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
            PlayerSettings.bundleVersion = Instance.productVersion + "." + Instance.version;

            return true;
        }

        [MenuItem("Moein/Build Setting")]
        public static void SelectConfig()
        {
            Selection.activeObject = Instance;
            return;
        }

#if UNITY_EDITOR

        public class PreBuildProcessor : IPreprocessBuildWithReport
        {
            public int callbackOrder => -999999;

            public void OnPreprocessBuild(BuildReport report)
            {
                Instance.version++;
                PlayerSettings.bundleVersion = Instance.productVersion + "." + Instance.version;
                Debug.Log($"BuildSetting: PreBuildProcessor: Compeleted. (order: {callbackOrder})");

                AssetDatabase.SaveAssets();
            }
        }
#endif

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