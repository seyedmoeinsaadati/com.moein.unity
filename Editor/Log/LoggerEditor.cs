using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Moein.Log
{
    public class LoggerEditor : EditorWindow
    {
        [MenuItem("Moein/Create UILogger", false)]
        public static void CreateUILogger()
        {
            GameObject uilogger = (GameObject)Instantiate(Resources.Load("Logger"), Vector3.zero, Quaternion.identity);
            uilogger.name = "MoeinLogger";
        }
    }

}