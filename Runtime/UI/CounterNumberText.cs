using UnityEngine;
using UnityEngine.UI;

namespace Moein.UI
{
    [RequireComponent(typeof(Text))]
    public class CounterNumberText : MonoBehaviour
    {
        [SerializeField] private float duration = 1;
        [SerializeField] private AnimationCurve speedCurve = AnimationCurve.Linear(0, 1, 1, 1);
        [SerializeField] private string numberFormat = "0";

        [SerializeField] private bool useAdvanceFormat = false;
        private string firstStringValue = string.Empty;
        private Text textUi = null;

        [SerializeField] private float desiredNumber = 0;
        private float initialNumber = 0;
        private float diffNumber = 0;
        private float currentNumber = 0;
        private float speedFactor = 1f;

        [SerializeField] private bool isUpdating = false;

        public float Duration
        {
            get => duration;
            set => duration = value;
        }

        private void Reset()
        {
            textUi = GetComponent<Text>();
        }

        private void Awake()
        {
            textUi = GetComponent<Text>();
            firstStringValue = textUi.text;
        }

        private void Update()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            if (isUpdating == false) return;


            if (diffNumber != 0)
            {
                speedFactor = speedCurve.Evaluate(Mathf.Abs(currentNumber - initialNumber) / diffNumber);
            }

            if (initialNumber < desiredNumber)
            {
                currentNumber += (speedFactor * Time.deltaTime / duration) * diffNumber;
                if (currentNumber >= desiredNumber)
                {
                    currentNumber = desiredNumber;
                    isUpdating = false;
                }
            }
            else
            {
                currentNumber -= (speedFactor * Time.deltaTime / duration) * diffNumber;
                if (currentNumber <= desiredNumber)
                {
                    currentNumber = desiredNumber;
                    isUpdating = false;
                }
            }

            textUi.text = currentNumber.ToString(numberFormat);

            if (useAdvanceFormat)
                textUi.text = string.Format(firstStringValue, currentNumber.ToString(numberFormat));
            else
                textUi.text = currentNumber.ToString(numberFormat);
        }

        public void SetNumberFormat(string numberFormat)
        {
            this.numberFormat = numberFormat;
        }

        public void SetNumber(float value, float duration)
        {
            Duration = duration;
            SetNumber(value);
        }

        public void SetNumber(float value)
        {
            initialNumber = currentNumber;
            desiredNumber = value;
            diffNumber = Mathf.Abs(desiredNumber - initialNumber);
            isUpdating = true;
        }

        public void SetNumberImmediate(float value)
        {
            currentNumber = value;
            desiredNumber = value;
            textUi.text = currentNumber.ToString(numberFormat);
            diffNumber = Mathf.Abs(desiredNumber - initialNumber);
            isUpdating = true;
        }
    }

    public static partial class ExtensionMethods
    {
        public static void SetSlidingNumber(this Text self, float value, float duration = -1)
        {
            var component = self.GetComponent<CounterNumberText>();
            if (component == null)
            {
                component = self.gameObject.AddComponent<CounterNumberText>();
            }

            if (duration == 0)
                component.SetNumberImmediate(value);
            else if (duration > 0)
                component.SetNumber(value, duration);
            else
                component.SetNumber(value);
        }
    }
}