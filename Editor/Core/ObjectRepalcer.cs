using UnityEngine;
using UnityEditor;

namespace Moein.Core
{
    public class ObjectRepalcer : ScriptableWizard
    {
        public GameObject replaceObject;

        [MenuItem("Moein/Core/Object Replacer", false, 1)]
        static void ReplaceObject()
        {
            ScriptableWizard.DisplayWizard("Object Replacer", typeof(ObjectRepalcer), "Replace");
        }

        void OnWizardCreate()
        {
            DoRepaceAll();
        }

        private void DoRepaceAll()
        {
            Transform[] transforms = Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.ExcludePrefab);
            foreach (var item in transforms)
            {
                Replace(item);
            }
        }

        private void Replace(Transform item)
        {
            GameObject newGo;
            newGo = PrefabUtility.InstantiatePrefab(replaceObject) as GameObject;

            newGo.transform.position = item.position;
            newGo.transform.rotation = item.rotation;
            newGo.transform.localScale = item.localScale;
            newGo.transform.parent = item.parent;

            Undo.RegisterCreatedObjectUndo(newGo, "Repalce object");
            Undo.DestroyObjectImmediate(item.gameObject);
        }
    }
}