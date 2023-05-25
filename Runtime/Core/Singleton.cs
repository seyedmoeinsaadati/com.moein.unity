using UnityEngine;


namespace Moein.Core
{
    public class SingletonT : MonoBehaviour
    {
        // remove here if you do not need singletone SlowMotion
        private static SingletonT instance = null;

        public static SingletonT Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SingletonT>();
                    if (instance == null)
                    {
                        instance = new GameObject().AddComponent<SingletonT>();
                        instance.gameObject.name = instance.GetType().Name;
                    }
                }

                return instance;
            }
        }


        [SerializeField] private bool dontDestroyManagerOnLoad;

        public bool DontDestroyManagerOnLoad
        {
            get => dontDestroyManagerOnLoad;
            set
            {
                if (value)
                {
                    DontDestroyOnLoad(Instance.gameObject);
                }

                dontDestroyManagerOnLoad = value;
            }
        }
    }
}