using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Moein.UI
{
    public class CountNumber : MonoBehaviour
    {
        public Text target;

        public int CountFPS = 30;
        private Coroutine CountingCoroutine;
        private int oldValue = 0;

        public void UpdateText(int newValue, float duration = .75f)
        {
            if (CountingCoroutine != null)
            {
                StopCoroutine(CountingCoroutine);
            }

            CountingCoroutine = StartCoroutine(CountText(newValue, duration));
        }

        private IEnumerator CountText(int newValue, float duration)
        {
            WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS);
            int previousValue = oldValue;
            int stepAmount;

            if (newValue - previousValue < 0)
            {
                stepAmount =
                    Mathf.FloorToInt((newValue - previousValue) / (CountFPS * duration / 2));
            }
            else
            {
                stepAmount =
                    Mathf.CeilToInt((newValue - previousValue) / (CountFPS * duration / 2));
            }

            if (previousValue < newValue)
            {
                while (previousValue < newValue)
                {
                    previousValue += stepAmount;
                    if (previousValue > newValue)
                    {
                        previousValue = newValue;
                    }

                    target.text = previousValue.ToString();

                    yield return Wait;
                }
            }
            else
            {
                while (previousValue > newValue)
                {
                    previousValue += stepAmount;
                    if (previousValue < newValue)
                    {
                        previousValue = newValue;
                    }

                    target.text = previousValue.ToString();
                    yield return Wait;
                }
            }
        }
    }
}