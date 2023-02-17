using UnityEngine;
using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Moein.Tweening
{
    public class Tweener : MonoBehaviour
    {
        [SerializeField] private bool autoStart;
        [SerializeField] private float duration = 2, delay;
        [SerializeField] private Ease ease;
        [SerializeField] private int groupId = 0;
        [SerializeField] private bool isStart;

        private Coroutine movingRoutine;
        private Coroutine rotatingRoutine;
        private Coroutine scaleRoutine;

        // editor fields
        [HideInInspector] public bool showStates = true;
        [HideInInspector] public Vector3 startLocalPosition, startLocalRotation, startScale = Vector3.one;
        [HideInInspector] public Vector3 endLocalPosition, endLocalRotation, endScale = Vector3.one;

        private void Start()
        {
            if (autoStart) ToEnd();
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

        public void ToEnd(bool resetState = false, Action OnComplete = null)
        {
            if (resetState)
            {
                transform.localPosition = startLocalPosition;
                transform.localEulerAngles = startLocalRotation;
                transform.localScale = startScale;
            }

            if (movingRoutine != null) StopCoroutine(movingRoutine);
            movingRoutine = this.DOPosition(transform, endLocalPosition, duration, delay,ease, OnComplete: OnComplete);

            if (rotatingRoutine != null) StopCoroutine(rotatingRoutine);
            rotatingRoutine = this.DORotation(transform, endLocalRotation, duration, delay, ease);

            if (scaleRoutine != null) StopCoroutine(scaleRoutine);
            scaleRoutine = this.DoScale(transform, endScale, duration, delay, ease);

        }

        public void ToStart(bool resetState = false, Action OnComplete = null)
        {
            if (resetState)
            {
                transform.localPosition = endLocalPosition;
                transform.localEulerAngles = endLocalRotation;
                transform.localScale = endScale;
            }

            if (movingRoutine != null) StopCoroutine(movingRoutine);
            movingRoutine = this.DOPosition(transform, startLocalPosition, duration, delay, ease);

            if (rotatingRoutine != null) StopCoroutine(rotatingRoutine);
            rotatingRoutine = this.DORotation(transform, startLocalRotation, duration, delay, ease);

            if (scaleRoutine != null) StopCoroutine(scaleRoutine);
            scaleRoutine = this.DoScale(transform, startScale, duration, delay, ease);
        }

        public void Play(bool toStart)
        {
            if (toStart)
            {
                ToStart();
            }
            else
            {
                ToEnd();
            }
        }

#if UNITY_EDITOR
        private GUIStyle style = new GUIStyle();
        void OnDrawGizmos()
        {
            style.normal.textColor = Color.black;
            style.alignment = TextAnchor.MiddleCenter;
            UnityEditor.Handles.Label(transform.position, groupId.ToString(), style);
        }
#endif

        #region Editor Functions

        internal void SetStartState()
        {
            startLocalPosition = transform.localPosition;
            startLocalRotation = transform.localEulerAngles;
            startScale = transform.localScale;
        }

        internal void SetEndState()
        {
            endLocalPosition = transform.localPosition;
            endLocalRotation = transform.localEulerAngles;
            endScale = transform.localScale;
        }

        internal void Toggle()
        {
            if (isStart)
            {
                transform.localPosition = endLocalPosition;
                transform.localEulerAngles = endLocalRotation;
                transform.localScale = endScale;
                isStart = false;
            }
            else
            {
                transform.localPosition = startLocalPosition;
                transform.localEulerAngles = startLocalRotation;
                transform.localScale = startScale;
                isStart = true;
            }
        }

        #endregion

        /////////////////////////////////
        /// Static Members
        /////////////////////////////////

        private static List<Tweener> all = new List<Tweener>();

        public void ToEndByGroup(int groupId, bool reset = false)
        {
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].groupId == groupId)
                {
                    all[i].ToEnd(reset);
                }
            }
        }

        public void ToStartByGroup(int groupId, bool reset = false)
        {
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].groupId == groupId)
                {
                    all[i].ToStart(reset);
                }
            }
        }

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Tweener)), CanEditMultipleObjects]

    public class TweenerEditor : Editor
    {
        public Tweener tweenObject;

        public override void OnInspectorGUI()
        {
            tweenObject = target as Tweener;

            base.OnInspectorGUI();

            if (Application.isPlaying == false)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Set Start State"))
                    SetStartPosition();
                if (GUILayout.Button("Set End State"))
                    SetEndPosition();
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("To End"))
                    tweenObject.ToEnd(true);
                if (GUILayout.Button("To Start"))
                    tweenObject.ToStart(true);
                GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Toggle Position"))
                Toogle();

            tweenObject.showStates = EditorGUILayout.Foldout(tweenObject.showStates, "States");
            if (tweenObject.showStates)
            {
                tweenObject.startLocalPosition = EditorGUILayout.Vector3Field("Start Local Position", tweenObject.startLocalPosition);
                tweenObject.startLocalRotation = EditorGUILayout.Vector3Field("Start Local Rotation", tweenObject.startLocalRotation);
                tweenObject.startScale = EditorGUILayout.Vector3Field("Start Scale", tweenObject.startScale);

                tweenObject.endLocalPosition = EditorGUILayout.Vector3Field("End Local Position", tweenObject.endLocalPosition);
                tweenObject.endLocalRotation = EditorGUILayout.Vector3Field("End Local Rotation", tweenObject.endLocalRotation);
                tweenObject.endScale = EditorGUILayout.Vector3Field("End Scale", tweenObject.endScale);
            }
        }

        private void SetStartPosition()
        {
            foreach (var o in targets)
            {
                var item = (Tweener)o;
                item.SetStartState();
            }
        }

        private void SetEndPosition()
        {
            foreach (var o in targets)
            {
                var item = (Tweener)o;
                item.SetEndState();
            }
        }

        private void Toogle()
        {
            foreach (Tweener item in targets)
            {
                item.Toggle();
            }
        }
    }
#endif
}