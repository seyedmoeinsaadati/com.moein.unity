using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Moein.Core;

namespace Moein.Log
{
    public class Logger : MonoBehaviour
    {
        static Text content;
        static string str_content;

        [SerializeField] KeyCode activeKey = KeyCode.Tab;

        public bool active = false;
        public int fontSize = 20;

        public event Action onCollect;
        List<ILogger> loggers;

        void Start()
        {
            loggers = new List<ILogger>();
            content = GetComponentInChildren<Text>();
            content.text = "";
            Invoke("RegisterLoggers", 1);
        }

        void OnDestrory()
        {
            RemoveLoggers();
        }

        private void RegisterLoggers()
        {
            GameObject[] gobjects = FindObjectsOfType<GameObject>();

            foreach (GameObject item in gobjects)
            {
                try
                {
                    ILogger[] _loggers = item.GetComponents<ILogger>();
                    foreach (var logItem in _loggers)
                    {
                        onCollect += logItem.CollectLogData;
                        loggers.Add(logItem);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
        }

        private void RemoveLoggers()
        {
            foreach (ILogger logger in loggers)
            {
                onCollect -= logger.CollectLogData;
            }
        }

        void InitText()
        {
            str_content = "";
            Add(Constants.TiitleText());
            Add("Logger Items: " + loggers.Count);
            Add("--------------------------------------------------");
        }

        void Update()
        {
            if (Input.GetKeyDown(activeKey))
            {
                active = !active;
                Show(content);
            }

            if (active)
            {
                InitText();
                Collect();
                ShowContent();
            }
        }

        public static void Add(string content)
        {
            str_content += content + "\n";
        }

        void ShowContent()
        {
            content.text = str_content;
            content.fontSize = fontSize;
        }

        void Show(Text content)
        {
            content.gameObject.SetActive(active);
        }

        void Collect()
        {
            if (onCollect != null)
            {
                onCollect();
            }
        }

        private static Logger instance = null;

        public static Logger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<Logger>();
                    if (instance == null)
                    {
                        instance = new GameObject().AddComponent<Logger>();
                        instance.gameObject.name = instance.GetType().Name;
                    }
                }

                return instance;
            }
        }
    }
}

public class UILogStyle
{
    Color textColor;

    public UILogStyle(Color textColor)
    {
        this.textColor = textColor;
    }
}