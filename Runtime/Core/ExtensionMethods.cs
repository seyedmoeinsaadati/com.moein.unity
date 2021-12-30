using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Moein.Core
{
    public static class ExtensionMethods
    {
        #region GameObject

        public static void SetActive(this Component self, bool active)
        {
            self.gameObject.SetActive(active);
        }

        public static void SetActive(this Transform self, bool active)
        {
            self.gameObject.SetActive(active);
        }

        #endregion

        #region Transform

        public static Transform FindRecursive(this Transform self, string childName)
        {
            foreach (Transform child in self)
            {
                return child.name == childName ? child : child.FindRecursive(childName);
            }

            return null;
        }

        #endregion

        #region Vector

        public static Vector2 ToXZ(this Vector3 self)
        {
            return new Vector2(self.x, self.z);
        }

        public static Vector2 ToXY(this Vector3 self)
        {
            return new Vector2(self.x, self.y);
        }

        public static Vector2 ToYZ(this Vector3 self)
        {
            return new Vector2(self.y, self.z);
        }

        public static Vector3 ToXZ(this Vector2 self, float defValue = 0)
        {
            return new Vector3(self.x, defValue, self.y);
        }

        public static Vector3 ToXY(this Vector2 self, float defValue = 0)
        {
            return new Vector3(self.x, self.y, defValue);
        }

        public static Vector3 ToYZ(this Vector2 self, float defValue = 0)
        {
            return new Vector3(defValue, self.x, self.y);
        }

        #endregion

        #region Math

        public static float ChangeRange(this float value, float firstRangeMin, float firstRangeMax,
            float secondRangeMin, float secondRangeMax)
        {
            return (value - firstRangeMin) *
                Mathf.Abs((secondRangeMax - secondRangeMin) / (firstRangeMax - firstRangeMin)) + secondRangeMin;
        }

        #endregion

        #region Randomize

        public static float Randomize(this float self, float min, float max, bool isAdditive = true)
        {
            self = isAdditive ? self + Random.Range(min, max) : Random.Range(min, max);
            return self;
        }

        public static Vector2 Randomize(this Vector2 self, Vector2 min, Vector2 max, bool isAdditive = true)
        {
            self.x = isAdditive ? self.x + Random.Range(min.x, max.x) : Random.Range(min.x, max.x);
            self.y = isAdditive ? self.y + Random.Range(min.y, max.y) : Random.Range(min.y, max.y);
            return self;
        }

        public static Vector3 Randomize(this Vector3 self, Vector3 min, Vector3 max, bool isAdditive = true)
        {
            self.x = isAdditive ? self.x + Random.Range(min.x, max.x) : Random.Range(min.x, max.x);
            self.y = isAdditive ? self.y + Random.Range(min.y, max.y) : Random.Range(min.y, max.y);
            self.z = isAdditive ? self.z + Random.Range(min.z, max.z) : Random.Range(min.z, max.z);
            return self;
        }

        #endregion
    }
}