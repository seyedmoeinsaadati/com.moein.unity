using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moein.Log
{
    public class ScreenSize : MonoBehaviour, ILogger
    {
        private void Start()
        {
        }
        public void CollectLogData()
        {
            Logger.Add(string.Format("Screen Size: " + Screen.width + " x " + Screen.height));
        }
    }
}
