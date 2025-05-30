﻿using UnityEditor;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Moein.Physics
{
    public class Grenade : MonoBehaviour
    {
        public GrenadeType activeType;
        public DetectArea detectArea;
        [Tooltip("Destroy object after exploding")]
        public bool destroySelf;
        public bool extraPoint;

        public float delay = 3f, destroyDelay = 0f;
        public Vector3 point;

        public float radius, force;

#if !ENABLE_INPUT_SYSTEM
        public KeyCode explodeKey = KeyCode.J;
#endif

        float countdown;
        // Use this for initialization

        public LayerMask layers;
        void Start()
        {
            countdown = delay;
        }

        // Update is called once per frame
        void Update()
        {
            switch (activeType)
            {
                case GrenadeType.Manual:
                    if (CheckExplodeKeyInputPressed())
                    {
                        Explode();
                    }
                    break;
                case GrenadeType.ManualPlusDelay:
                    if (CheckExplodeKeyInputPressed())
                    {
                        activeType = GrenadeType.Delay;
                    }
                    break;
                case GrenadeType.Delay:
                    countdown -= Time.deltaTime;
                    if (countdown < 0)
                    {
                        Explode();
                    }
                    break;
                default:
                    break;
            }
        }

        public void Explode()
        {
            Collider[] overC;
            switch (detectArea)
            {
                case DetectArea.Sphere:
                    overC = UnityEngine.Physics.OverlapSphere(transform.position, radius, layers);
                    break;
                case DetectArea.Cube:
                    overC = UnityEngine.Physics.OverlapBox(transform.position, Vector3.one * radius / 2, Quaternion.identity, layers);
                    Debug.Log(overC.Length);
                    break;
                default:
                    overC = null;
                    break;
            }

            foreach (var item in overC)
            {
                Rigidbody rb = item.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.AddExplosionForce(force, point, radius);
                }
            }

            if (destroySelf)
            {
                Destroy(gameObject);
            }
        }

        private bool CheckExplodeKeyInputPressed()
        {
#if !ENABLE_INPUT_SYSTEM
            return Input.GetKeyDown(explodeKey);
#else
            return Keyboard.current.jKey.wasPressedThisFrame;
#endif
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            switch (detectArea)
            {
                case DetectArea.Sphere:
                    Gizmos.DrawWireSphere(transform.position, radius);
                    break;
                case DetectArea.Cube:
                    Gizmos.DrawWireCube(transform.position, Vector3.one * radius);
                    break;
                default:
                    break;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(point, .3f);
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(Grenade), true), CanEditMultipleObjects]
    public class GrenadeEditor : Editor
    {
        public Grenade grenade;

        void OnEnable()
        {
            grenade = target as Grenade;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (!grenade.extraPoint)
            {
                grenade.point = grenade.transform.position;
            }
        }

        void OnSceneGUI()
        {
            if (grenade.extraPoint)
            {
                grenade.point = Handles.PositionHandle(grenade.point, Quaternion.identity);
                Undo.RecordObject(grenade, "Point Position");
            }
        }
    }
#endif

}