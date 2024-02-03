using UnityEngine;
using UnityEditor;
namespace Moein.Randomize
{

    public class RandomizeEditor : EditorWindow
    {
        bool mirrorX, mirrorY, mirrorZ;
        bool lockPosX, lockPosY, lockPosZ;
        bool isLocal;
        public Vector3 minPosition;
        public Vector3 maxPosition;

        public Vector3 minAngles;
        public Vector3 maxAngles;

        public Vector3 minScale = Vector3.one;
        public Vector3 maxScale = Vector3.one;
        bool lockScaleX, lockScaleY, lockScaleZ;

        private Vector3 centerPoint, offset;
        public Vector3[] p;

        public bool positionRandom, rotationRandom, scaleRandom, options, randomSelection;
        public float percent;

        [MenuItem("Moein/Randomize &R", false, 10)]
        public static void ShowWindow()
        {
            GetWindow<RandomizeEditor>("Randomize", true);
        }

        void OnGUI()
        {
            positionRandom = EditorGUILayout.Foldout(positionRandom, "Position");
            if (positionRandom)
            {
                // deactive others
                rotationRandom = false;
                scaleRandom = false;
                // Set Postion Randmoly
                RandomPositionGUI();
                options = EditorGUILayout.Foldout(options, "Options");
                if (options)
                {
                    PositionOptionsGUI();
                }

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Randomize Position"))
                {
                    RandomizePosition();
                }
                if (GUILayout.Button("Snap Position"))
                {
                    SnapObjectPosition();
                }
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(2f);
            rotationRandom = EditorGUILayout.Foldout(rotationRandom, "Rotation");
            if (rotationRandom)
            {
                // deactive others
                positionRandom = false;
                scaleRandom = false;
                // Set Rotation Randomly
                RandomRotationGUI();

                options = EditorGUILayout.Foldout(options, "Options");
                if (options)
                {
                    GUILayout.Space(1f);
                    GUILayout.Label("Lock Axes: ", GUILayout.ExpandWidth(true));
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("X: ");
                    lockPosX = EditorGUILayout.Toggle(lockPosX);
                    GUILayout.Label("Y: ");
                    lockPosY = EditorGUILayout.Toggle(lockPosY);
                    GUILayout.Label("Z: ");
                    lockPosZ = EditorGUILayout.Toggle(lockPosZ);
                    EditorGUILayout.EndHorizontal();

                    GUILayout.Space(1f);
                    isLocal = EditorGUILayout.Toggle("Is Local Rotation:", isLocal, GUILayout.ExpandWidth(true));
                }

                if (GUILayout.Button("Randomize Rotation"))
                {
                    RandomizeRotation();
                }
            }

            GUILayout.Space(2f);
            scaleRandom = EditorGUILayout.Foldout(scaleRandom, "Scale");
            if (scaleRandom)
            {
                // deactive others
                rotationRandom = false;
                positionRandom = false;
                // Set Scale Randomly
                RandomScaleGUI();
                if (GUILayout.Button("Randomize Scale"))
                {
                    RandomizeScale();
                }
            }

            GUILayout.Space(2f);
            randomSelection = EditorGUILayout.Foldout(randomSelection, "Random Selection");
            if (randomSelection)
            {
                GUILayout.Label("Options:");
                GUILayout.Space(1f);

                percent = EditorGUILayout.FloatField("Object Persent (0 to 1): ", percent, GUILayout.ExpandWidth(true));
                if (percent > 1)
                {
                    percent = 1;
                }
                if (percent < 0)
                {
                    percent = 0;
                }

                GUILayout.Space(1f);
                if (GUILayout.Button("Select Random Objects"))
                {
                    SelectRandomObject();
                }
            }
        }

        #region Randomize Methods

        private void SelectRandomObject()
        {
            if (Selection.gameObjects.Length <= 0)
            {
                Debug.Log("Select Objects.");
                return;
            }

            Transform[] trans = Selection.GetTransforms(SelectionMode.TopLevel);

            int count = (int)(percent * trans.Length);
            GameObject[] selectedObjects = new GameObject[count];

            for (int i = 0; i < count; i++)
            {
                int rindex = (int)Random.Range(0, trans.Length);
                while (trans[rindex] == null)
                    rindex = (int)Random.Range(0, trans.Length);
                selectedObjects[i] = trans[rindex].gameObject;
                trans[rindex] = null;
            }

            Selection.objects = selectedObjects;
            Debug.Log(count + " object selected.");
        }

        void RandomizePosition(bool isSnap = false)
        {
            Transform[] objs = Selection.GetTransforms(SelectionMode.TopLevel);
            foreach (var item in objs)
            {
                Undo.RegisterCompleteObjectUndo(item, "undo object position");
                Vector3 newPosition = new Vector3(lockPosX ? item.transform.position.x : Random.Range(minPosition.x, maxPosition.x),
                                          lockPosY ? item.transform.position.y : Random.Range(minPosition.y, maxPosition.y),
                                          lockPosZ ? item.transform.position.z : Random.Range(minPosition.z, maxPosition.z));
                newPosition += offset;

                // later: add isSnap
                if (isLocal)
                {
                    item.transform.localPosition = newPosition;
                }
                else
                {
                    item.transform.position = newPosition;
                }
            }
        }

        private void RandomizeRotation()
        {
            Transform[] objs = Selection.GetTransforms(SelectionMode.TopLevel);
            foreach (var item in objs)
            {
                Quaternion oldRotation = isLocal ? item.localRotation : item.rotation;
                Undo.RegisterCompleteObjectUndo(item, isLocal ? "Local Rotation" : "Rotation");
                Vector3 newRotationAngles = new Vector3(lockPosX ? oldRotation.x : Random.Range(minAngles.x, maxAngles.x),
                                          lockPosY ? oldRotation.y : Random.Range(minAngles.y, maxAngles.y),
                                          lockPosZ ? oldRotation.z : Random.Range(minAngles.z, maxAngles.z));

                Quaternion newRotation = Quaternion.Euler(newRotationAngles);
                if (isLocal)
                {
                    item.localRotation = newRotation;
                }
                else
                {
                    item.rotation = newRotation;
                }
            }
        }

        private void RandomizeScale()
        {
            Transform[] objs = Selection.GetTransforms(SelectionMode.TopLevel);

            foreach (var item in objs)
            {
                Undo.RegisterCompleteObjectUndo(item, "undo object scale");

                Vector3 newScale = new Vector3(lockScaleX ? item.transform.localScale.x : Random.Range(minScale.x, maxScale.x),
                                         lockScaleY ? item.transform.localScale.y : Random.Range(minScale.y, maxScale.y),
                                         lockScaleZ ? item.transform.localScale.z : Random.Range(minScale.z, maxScale.z));

                item.localScale = newScale;
            }

        }

        void SnapObjectPosition()
        {
            Transform[] trans = Selection.GetTransforms(SelectionMode.TopLevel);
            foreach (var item in trans)
            {
                Undo.RegisterCompleteObjectUndo(item, "unSnap object position");
                item.position = SnapPosition(item.position);
            }
        }

        Vector3 SnapPosition(Vector3 _position)
        {
            _position.x = _position.x < 0 ? -Mathf.Round(-_position.x) : Mathf.Round(_position.x);
            _position.y = _position.y < 0 ? -Mathf.Round(-_position.y) : Mathf.Round(_position.y);
            _position.z = _position.z < 0 ? -Mathf.Round(-_position.z) : Mathf.Round(_position.z);
            return _position;
        }

        public static bool isRandomable()
        {
            return Selection.gameObjects.Length > 0;
        }

        #endregion

        #region GUI

        private void RandomPositionGUI()
        {
            minPosition = EditorGUILayout.Vector3Field("Min Position:", minPosition);
            maxPosition = EditorGUILayout.Vector3Field("Max Position:", maxPosition);
            offset = EditorGUILayout.Vector3Field("Offset:", offset);

            float centerX = (minPosition.x + maxPosition.x) / 2;
            float centerY = (minPosition.y + maxPosition.y) / 2;
            float centerZ = (minPosition.z + maxPosition.z) / 2;
            centerPoint = new Vector3(centerX, centerY, centerZ);
        }

        private void PositionOptionsGUI()
        {
            GUILayout.Space(1f);
            GUILayout.Label("Mirror Axis: ", GUILayout.ExpandWidth(true));
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("X: ");
            mirrorX = EditorGUILayout.Toggle(mirrorX);
            GUILayout.Label("Y: ");
            mirrorY = EditorGUILayout.Toggle(mirrorY);
            GUILayout.Label("Z: ");
            mirrorZ = EditorGUILayout.Toggle(mirrorZ);
            EditorGUILayout.EndHorizontal();
            if (mirrorX)
            {
                minPosition.x = -maxPosition.x;
            }
            if (mirrorY)
            {
                minPosition.y = -maxPosition.y;
            }
            if (mirrorZ)
            {
                minPosition.z = -maxPosition.z;
            }
            GUILayout.Space(1f);
            GUILayout.Label("Lock Axes: ", GUILayout.ExpandWidth(true));
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("X: ");
            lockPosX = EditorGUILayout.Toggle(lockPosX);
            GUILayout.Label("Y: ");
            lockPosY = EditorGUILayout.Toggle(lockPosY);
            GUILayout.Label("Z: ");
            lockPosZ = EditorGUILayout.Toggle(lockPosZ);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(1f);
            isLocal = EditorGUILayout.Toggle("Is Local Position:", isLocal, GUILayout.ExpandWidth(true));
        }

        private void RandomRotationGUI()
        {
            minAngles = EditorGUILayout.Vector3Field("Min Angles:", minAngles);
            maxAngles = EditorGUILayout.Vector3Field("Max Angles:", maxAngles);
        }

        private void RandomScaleGUI()
        {
            minScale = EditorGUILayout.Vector3Field("Min Scale:", minScale);
            maxScale = EditorGUILayout.Vector3Field("Max Scale:", maxScale);

            GUILayout.Space(1f);
            GUILayout.Label("Lock Axes: ", GUILayout.ExpandWidth(true));
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("X: ");
            lockScaleX = EditorGUILayout.Toggle(lockScaleX);
            GUILayout.Label("Y: ");
            lockScaleY = EditorGUILayout.Toggle(lockScaleY);
            GUILayout.Label("Z: ");
            lockScaleZ = EditorGUILayout.Toggle(lockScaleZ);
            EditorGUILayout.EndHorizontal();
        }

        #endregion

        #region SceneGUI

        void CalculateLinePoints()
        {
            p = new Vector3[8];
            p[0] = new Vector3(maxPosition.x, maxPosition.y, minPosition.z) + offset;
            p[1] = new Vector3(maxPosition.x, maxPosition.y, maxPosition.z) + offset;
            p[2] = new Vector3(minPosition.x, maxPosition.y, minPosition.z) + offset;
            p[3] = new Vector3(minPosition.x, maxPosition.y, maxPosition.z) + offset;
            p[4] = new Vector3(maxPosition.x, minPosition.y, minPosition.z) + offset;
            p[5] = new Vector3(maxPosition.x, minPosition.y, maxPosition.z) + offset;
            p[6] = new Vector3(minPosition.x, minPosition.y, minPosition.z) + offset;
            p[7] = new Vector3(minPosition.x, minPosition.y, maxPosition.z) + offset;
        }

        private void DrawLines()
        {
            Handles.DrawLine(p[0], p[1]);
            Handles.DrawLine(p[0], p[2]);
            Handles.DrawLine(p[0], p[4]);
            Handles.DrawLine(p[1], p[3]);
            Handles.DrawLine(p[1], p[5]);
            Handles.DrawLine(p[2], p[6]);
            Handles.DrawLine(p[2], p[3]);
            Handles.DrawLine(p[3], p[7]);
            Handles.DrawLine(p[4], p[5]);
            Handles.DrawLine(p[4], p[6]);
            Handles.DrawLine(p[5], p[7]);
            Handles.DrawLine(p[6], p[7]);
        }

        void OnFocus()
        {
            // Remove delegate listener if it has previously
            // been assigned.
            SceneView.duringSceneGui -= this.OnSceneGUI;
            // Add (or re-add) the delegate.
            SceneView.duringSceneGui += this.OnSceneGUI;
        }

        void OnDestroy()
        {
            // When the window is destroyed, remove the delegate
            // so that it will no longer do any drawing.
            SceneView.duringSceneGui -= this.OnSceneGUI;
        }

        void OnSceneGUI(SceneView sceneView)
        {
            // Do your drawing here using Handles.
            offset = Handles.PositionHandle(offset, Quaternion.identity);

            CalculateLinePoints();
            Handles.color = Color.yellow;
            DrawLines();

            HandleUtility.Repaint();
        }
        #endregion
    }
}