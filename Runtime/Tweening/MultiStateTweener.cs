using UnityEngine;
using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Moein.Tweening
{
    public class MultiStateTweener : MonoBehaviour
    {
        [SerializeField] private bool autoStart;
        [SerializeField] private int groupId = 0;
        public Transform statesPivot;
        [SerializeField] private Ease ease;
        [SerializeField] private LoopType loop = LoopType.Once;
        [SerializeField] private Direction direction;

        private Coroutine movingRoutine;
        private Coroutine rotatingRoutine;
        private Coroutine scaleRoutine;

        [HideInInspector] public List<State> states = new List<State>();

        private int currentStateIndex = 0;
        private State CurrentState => states[currentStateIndex];

#if UNITY_EDITOR
        [HideInInspector] public int editor_index = -1;
        [Header("Editor Properties:")] public Mesh editorMesh;
        [SerializeField] private Color editorLineColor = Color.green;
        [SerializeField] private Color editorPointColor = Color.red;
        [HideInInspector] public bool showPointsList;
        [HideInInspector] public bool drawGizmos;
#endif

        void Start()
        {
            if (autoStart)
            {
                Play();
            }
        }

        private void OnEnable()
        {
            all.Add(this);
        }

        private void OnDisable()
        {
            if (all.Contains(this))
                all.Remove(this);
        }

        public void Play(bool reset = false)
        {
            if (reset)
            {
                currentStateIndex = direction == Direction.Forward ? 0 : states.Count - 1;
                SetState(CurrentState);
            }

            Tween();
        }

        public void Stop(bool reset = false)
        {
            if (movingRoutine != null) StopCoroutine(movingRoutine);
            if (rotatingRoutine != null) StopCoroutine(rotatingRoutine);
            if (scaleRoutine != null) StopCoroutine(scaleRoutine);

            if (reset)
            {
                currentStateIndex = direction == Direction.Forward ? 0 : states.Count - 1;
                SetState(CurrentState);
            }
        }

        void CalculateNextStateIndex()
        {
            switch (loop)
            {
                case LoopType.Once:
                    if (direction == Direction.Forward)
                    {
                        currentStateIndex++;
                        if (currentStateIndex > states.Count - 1)
                            currentStateIndex = 0;
                    }
                    else if (direction == Direction.Backward)
                    {
                        currentStateIndex--;
                        if (currentStateIndex < 0)
                            currentStateIndex = states.Count - 1;
                    }
                    break;
                case LoopType.Yoyo:
                    if (direction == Direction.Forward)
                    {
                        currentStateIndex++;
                        if (currentStateIndex > states.Count - 1)
                        {
                            currentStateIndex = states.Count - 2;
                            direction = (direction == Direction.Forward ? Direction.Backward : Direction.Forward);
                        }
                    }
                    else if (direction == Direction.Backward)
                    {
                        currentStateIndex--;
                        if (currentStateIndex < 0)
                        {
                            currentStateIndex = 1;
                            direction = (direction == Direction.Forward ? Direction.Backward : Direction.Forward);
                        }
                    }
                    break;
                case LoopType.Loop:
                    if (direction == Direction.Forward)
                    {
                        currentStateIndex++;
                        if (currentStateIndex > states.Count - 1)
                            currentStateIndex = 0;
                    }
                    else if (direction == Direction.Backward)
                    {
                        currentStateIndex--;
                        if (currentStateIndex < 0)
                            currentStateIndex = states.Count - 1;
                    }

                    break;
            }
        }

        private void Tween()
        {
            float duration = CurrentState.duration;
            float delay = CurrentState.delay;

            if (movingRoutine != null) StopCoroutine(movingRoutine);
            movingRoutine = this.DOPosition(transform, CurrentState.localPos, duration, delay, ease, OnComplete: ToNextState);

            if (rotatingRoutine != null) StopCoroutine(rotatingRoutine);
            rotatingRoutine = this.DORotation(transform, CurrentState.localEulerAngle, duration, delay, ease);

            if (scaleRoutine != null) StopCoroutine(scaleRoutine);
            scaleRoutine = this.DoScale(transform, CurrentState.scale, duration, delay, ease);
        }

        private void ToNextState()
        {
            if (loop == LoopType.Once)
            {
                if ((direction == Direction.Forward && currentStateIndex == states.Count - 1) ||
                    (direction == Direction.Backward && currentStateIndex == 0))
                {
                    Stop();
                    return;
                }
            }

            CalculateNextStateIndex();
            Tween();
        }

        public void SetState(State state)
        {
            transform.localPosition = state.localPos;
            transform.localEulerAngles = state.localEulerAngle;
            transform.localScale = state.scale;
        }

        #region Editor Functions
        void OnDrawGizmos()
        {
            if (drawGizmos)
            {
                Color c = Color.green;
                c.a = 0.2f;
                Gizmos.color = c;
                Gizmos.DrawSphere(transform.position, 0.5f);
                Draw();
            }
        }

        private void Draw()
        {
            for (int i = 0; i < states.Count; i++)
            {
                Vector3 nextPos;
                Gizmos.color = editorLineColor;
                if (loop == LoopType.Loop)
                {
                    nextPos = states[(i + 1) % states.Count].worldPos;
                    Gizmos.DrawLine(states[i].worldPos, nextPos);
                }
                else
                {
                    if (i != states.Count - 1)
                    {
                        nextPos = states[(i + 1)].worldPos;
                        Gizmos.DrawLine(states[i].worldPos, nextPos);
                    }
                }
                Gizmos.color = editorPointColor;
                DrawPoint(i);
            }
        }

        private void DrawPoint(int index)
        {
            Gizmos.DrawWireMesh(editorMesh, states[index].worldPos, Quaternion.Euler(states[index].localEulerAngle), states[index].scale);
#if UNITY_EDITOR
            Handles.Label(states[index].worldPos, index.ToString());
#endif
        }
        #endregion

        /////////////////////////////////
        /// Static Members
        /////////////////////////////////
        private readonly static List<MultiStateTweener> all = new List<MultiStateTweener>();

        public static void PlayByGroup(int groupId, bool reset = false)
        {
            if (groupId < 0)
            {
                for (int i = 0; i < all.Count; i++)
                {
                    all[i].Play(reset);
                }
                return;
            }

            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].groupId == groupId)
                {
                    all[i].Play(reset);
                }
            }
        }

        public static void StopByGroup(int groupId, bool reset = false)
        {
            if (groupId < 0)
            {
                for (int i = 0; i < all.Count; i++)
                {
                    all[i].Stop(reset);
                }
                return;
            }

            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].groupId == groupId)
                {
                    all[i].Stop(reset);
                }
            }
        }
    }

    [Serializable]
    public class State
    {
        public Vector3 localPos, localEulerAngle, scale;
        public float duration, delay;

#if UNITY_EDITOR
        public Vector3 worldPos;
#endif
        public State(Transform transform)
        {
            this.localPos = transform.localPosition;
            this.localEulerAngle = transform.localEulerAngles;
            this.scale = transform.localScale;
            duration = delay = 0;

#if UNITY_EDITOR
            worldPos = transform.position;
#endif
        }
    }


#if UNITY_EDITOR

    [CustomEditor(typeof(MultiStateTweener)), CanEditMultipleObjects]
    public class PathMotionEditor : Editor
    {
        private static int groupId;
        public MultiStateTweener tweener;
        public SelectionInfo selectionInfo;
        public bool shapeChangedSinceLastRepaint;

        private void OnEnable()
        {
            shapeChangedSinceLastRepaint = true;
            tweener = target as MultiStateTweener;
            selectionInfo = new SelectionInfo();
            Undo.undoRedoPerformed += OnUndoOrRedo;
            tweener.drawGizmos = true;
        }

        private void OnDisable()
        {
            Undo.undoRedoPerformed -= OnUndoOrRedo;
            tweener.drawGizmos = false;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            int pointDeleteIndex = -1;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Prev State"))
            {
                PrevPosition();
            }
            if (GUILayout.Button("Next State"))
            {
                NextPosition();
            }
            GUILayout.EndHorizontal();

            tweener.showPointsList = EditorGUILayout.Foldout(tweener.showPointsList, "Show States List");
            if (tweener.showPointsList)
            {
                for (int i = 0; i < tweener.states.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"State {i}:");

                    GUILayout.Label("Dur: ");
                    tweener.states[i].duration = EditorGUILayout.FloatField(tweener.states[i].duration);

                    GUILayout.Label("Del: ");
                    tweener.states[i].delay = EditorGUILayout.FloatField(tweener.states[i].delay);

                    GUI.enabled = (i != selectionInfo.pointIndex);
                    if (GUILayout.Button("Select"))
                    {
                        selectionInfo.pointIndex = i;
                        tweener.SetState(tweener.states[selectionInfo.pointIndex]);
                    }
                    GUI.enabled = true;

                    if (GUILayout.Button("Update"))
                    {
                        UpdateState();
                    }

                    if (GUILayout.Button("Delete"))
                    {
                        pointDeleteIndex = i;
                    }

                    GUILayout.EndHorizontal();
                }
            }

            if (GUILayout.Button("Add State"))
            {
                AddState();
            }

            if (Application.isPlaying)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Play"))
                    tweener.Play();
                if (GUILayout.Button("Stop"))
                    tweener.Stop();
                GUILayout.EndHorizontal();

                EditorGUILayout.LabelField("Groups");
                groupId = EditorGUILayout.IntField("Group Id", groupId);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button($"Play ({groupId})"))
                    MultiStateTweener.PlayByGroup(groupId);
                if (GUILayout.Button($"Stop ({groupId})"))
                    MultiStateTweener.StopByGroup(groupId);
                GUILayout.EndHorizontal();
            }

            if (pointDeleteIndex != -1)
            {
                Undo.RecordObject(tweener, "Delete Point");
                tweener.states.RemoveAt(pointDeleteIndex);
                selectionInfo.pointIndex =
                    Mathf.Clamp(selectionInfo.pointIndex, 0, tweener.states.Count - 1);
            }

            if (GUI.changed)
            {
                shapeChangedSinceLastRepaint = true;
                SceneView.RepaintAll();
            }
        }

        private void NextPosition()
        {
            tweener.editor_index++;
            if (tweener.editor_index > tweener.states.Count - 1)
            {
                tweener.editor_index = 0;
            }
            tweener.SetState(tweener.states[tweener.editor_index]);
        }

        private void PrevPosition()
        {
            tweener.editor_index--;
            if (tweener.editor_index < 0)
            {
                tweener.editor_index = tweener.states.Count - 1;
            }
            tweener.SetState(tweener.states[tweener.editor_index]);
        }

        private void OnSceneGUI()
        {
            Event guiEvent = Event.current;

            HandleInput(guiEvent);
        }

        private void HandleInput(Event guiEvent)
        {
            if (guiEvent.type == EventType.MouseDown && guiEvent.button == 1 && guiEvent.modifiers == EventModifiers.None)
            {
                HandleRightMouseDown();
            }

            HandleSelectionPoint();
        }

        private void HandleRightMouseDown()
        {
            AddState();
        }

        private void OnUndoOrRedo()
        {
            if (selectionInfo.pointIndex >= tweener.states.Count || selectionInfo.pointIndex == -1)
            {
                selectionInfo.pointIndex = tweener.states.Count - 1;
            }
        }

        private void SelectPointUnderMouse()
        {
            selectionInfo.pointIsSelected = true;
            shapeChangedSinceLastRepaint = true;
        }

        private void AddState()
        {
            int newPointIndex = tweener.states.Count;

            Undo.RecordObject(tweener, "Add Point");
            tweener.states.Insert(newPointIndex, new State(tweener.transform));
            selectionInfo.pointIndex = newPointIndex;
            shapeChangedSinceLastRepaint = true;

            SelectPointUnderMouse();
        }

        private void UpdateState()
        {
            Undo.RecordObject(tweener, "Update State");
            tweener.states[selectionInfo.pointIndex] = new State(tweener.transform);
            shapeChangedSinceLastRepaint = true;
            SelectPointUnderMouse();
        }

        private void HandleSelectionPoint()
        {
            if (selectionInfo.pointIndex == -1) return;

            Undo.RecordObject(tweener, "Move Selection Point");
            var handlePos = Handles.PositionHandle(tweener.states[selectionInfo.pointIndex].worldPos, Quaternion.identity);
            tweener.states[selectionInfo.pointIndex].worldPos = handlePos;
            handlePos = tweener.statesPivot.InverseTransformPoint(handlePos);
            tweener.states[selectionInfo.pointIndex].localPos = handlePos;
        }
    }

    public class SelectionInfo
    {
        public int pointIndex = -1;
        public bool pointIsSelected;
    }

#endif
}