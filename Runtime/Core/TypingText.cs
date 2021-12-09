using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Moein.Core
{
    public class TypingText : MonoBehaviour
    {
        [Tooltip("if true Invoke else Call StartText method by scripts")]
        public bool auto;

        public float delay, delayLetter;
        Text txt;
        string story;

        void Awake()
        {
            txt = GetComponent<Text>();
            story = txt.text;
            if (auto)
            {
                Texing();
            }
        }

        public void Texing()
        {
            Invoke("StartText", delay);
        }

        void StartText()
        {
            txt.text = "";
            StopAllCoroutines();
            StartCoroutine(PlayText());
        }

        IEnumerator PlayText()
        {
            foreach (char c in story)
            {
                txt.text += c;
                yield return new WaitForSeconds(delayLetter);
            }
        }
    }
}