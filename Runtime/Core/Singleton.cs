using UnityEngine;


namespace Moein.Core
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        [SerializeField] private bool dontDestroyManagerOnLoad = false;

        public static T Instance
        {
            get { return _instance; }
        }

        private void Awake()
        {
            if (this.dontDestroyManagerOnLoad)
            {
                GameObject.DontDestroyOnLoad(this.gameObject);
            }

            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this as T;
            }
        }

        public bool DontDestroyManagerOnLoad
        {
            get { return this.dontDestroyManagerOnLoad; }
        }
    }
}