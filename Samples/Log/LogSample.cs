using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LogSample : MonoBehaviour, Moein.Log.ILogger
{
    private void Start()
    {

    }
    public void CollectLogData()
    {
        Moein.Log.Logger.Add("Positoin " + gameObject.name + ": " + transform.position);
        Moein.Log.Logger.Add("Local Positoin " + gameObject.name + ": " + transform.localPosition);
    }
}