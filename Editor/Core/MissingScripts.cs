using UnityEngine;
using UnityEditor;

namespace Moein.Core
{
    public static class MissingScripts
    {
        static int go_count = 0, components_count = 0, missing_count = 0;

        [MenuItem("Moein/Core/FindMissingScripts")]
        public static void FindMissingScripts()
        {
            FindInSelected();
        }

        [MenuItem("Moein/Core/FindMissingScriptsRecursively")]
        public static void FindMissingScriptsRecursively()
        {
            FindInSelected(true);
        }

        private static void FindInSelected(bool recuresivly = false)
        {
            GameObject[] go = Selection.gameObjects;
            go_count = 0;
            components_count = 0;
            missing_count = 0;
            foreach (GameObject g in go)
            {
                FindMissingRefs(g, recuresivly);
            }
            Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", go_count, components_count, missing_count));
        }

        private static void FindMissingRefs(GameObject g, bool recuresivly)
        {
            go_count++;
            Component[] components = g.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                components_count++;
                if (components[i] == null)
                {
                    missing_count++;
                    string s = g.name;
                    Transform t = g.transform;
                    while (t.parent != null)
                    {
                        s = t.parent.name + "/" + s;
                        t = t.parent;
                    }
                    Debug.LogError(s + " has an empty script attached in position: " + i, g);
                }
            }

            if (recuresivly)
                foreach (Transform childT in g.transform)
                    FindMissingRefs(childT.gameObject, recuresivly);
        }
    }
}