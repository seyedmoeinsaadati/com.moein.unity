﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Moein.Core
{
    public static class ExtensionMethods
    {
        private static System.Random rng = new System.Random();

        #region GameObject

        public static void SetActive(this Component self, bool active)
        {
            self.gameObject.SetActive(active);
        }

        public static void SetActive(this Transform self, bool active)
        {
            self.gameObject.SetActive(active);
        }

        public static bool HasComponent<T>(this GameObject self) where T : MonoBehaviour
        {
            return self.GetComponent<T>() != null;
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

        public static Transform GetClosest(this Transform self, Transform[] objs, bool isLocal = false)
        {
            if (objs.Length == 0) throw new Exception("The list of transforms is empty");

            var minDistance = Vector3.Distance(self.position, objs[0].position);

            if (isLocal)
            {
                minDistance = Vector3.Distance(self.localPosition, objs[0].localPosition);
            }

            var closestTransform = objs[0];

            for (var i = objs.Length - 1; i > 0; i--)
            {
                var newDistance = Vector3.Distance(self.position, objs[i].position);

                if (isLocal)
                {
                    newDistance = Vector3.Distance(self.localPosition, objs[i].localPosition);
                }

                if (newDistance < minDistance)
                {
                    minDistance = newDistance;
                    closestTransform = objs[i];
                }
            }

            return closestTransform;
        }

        public static void ResetTransform(this Transform self)
        {
            self.localPosition = Vector3.zero;
            self.localRotation = Quaternion.identity;
            self.localScale = Vector3.one;
        }

        public static void DestroyChildren(this Transform self)
        {
            while (self.childCount > 0)
                Object.DestroyImmediate(self.GetChild(0).gameObject);
        }

        #endregion

        #region RectTransform
        public static Vector2 ConvertWorldPosToRectPos(this Vector3 worldPos, RectTransform rectTransform)
        {
            if (Camera.main == null) return Vector2.zero;
            var screenPos = Camera.main.WorldToScreenPoint(worldPos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPos, null, out Vector2 localPoint);
            return localPoint;
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

        public static Vector3 ToXZ(this Vector2 self, float yValue = 0)
        {
            return new Vector3(self.x, yValue, self.y);
        }

        public static Vector3 ToXY(this Vector2 self, float zValue = 0)
        {
            return new Vector3(self.x, self.y, zValue);
        }

        public static Vector3 ToYZ(this Vector2 self, float xValue = 0)
        {
            return new Vector3(xValue, self.x, self.y);
        }

        public static Vector3 ToXZ(this Vector3 self, float yValue = 0)
        {
            self.y = yValue;
            return self;
        }

        public static Vector3 ToXY(this Vector3 self, float zValue = 0)
        {
            self.z = zValue;
            return self;
        }

        public static Vector3 ToYZ(this Vector3 self, float xValue = 0)
        {
            self.x = xValue;
            return self;
        }

        public static Vector2 Lerp(this Vector2 self, Vector2 target, Vector2 t)
        {
            Vector2 result = Vector3.zero;
            result.x = Mathf.Lerp(self.x, target.x, t.x);
            result.y = Mathf.Lerp(self.y, target.y, t.y);
            return result;
        }

        public static Vector2 Lerp(this Vector2 self, Vector2 start, Vector3 end, Vector2 t)
        {
            Vector2 result = Vector3.zero;
            result.x = Mathf.Lerp(start.x, end.x, t.x);
            result.y = Mathf.Lerp(start.y, end.y, t.y);
            return result;
        }

        public static Vector3 Lerp(this Vector3 self, Vector3 target, Vector3 t)
        {
            Vector3 result = Vector3.zero;
            result.x = Mathf.Lerp(self.x, target.x, t.x);
            result.y = Mathf.Lerp(self.y, target.y, t.y);
            result.z = Mathf.Lerp(self.z, target.z, t.z);
            return result;
        }

        public static Vector3 Lerp(this Vector3 self, Vector3 start, Vector3 end, Vector3 t)
        {
            Vector3 result = Vector3.zero;
            result.x = Mathf.Lerp(start.x, end.x, t.x);
            result.y = Mathf.Lerp(start.y, end.y, t.y);
            result.z = Mathf.Lerp(start.z, end.z, t.z);
            return result;
        }

        /// <summary>
        /// Rotate 2D point
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="angle"></param>
        /// <param name="clockwise"></param>
        /// <returns></returns>
        public static Vector2 Rotate(this Vector2 vector, float angle, bool clockwise) //angle in radians
        {
            if (clockwise)
            {
                angle = 2 * Mathf.PI - angle;
            }

            float xVal = vector.x * Mathf.Cos(angle) - vector.y * Mathf.Sin(angle);
            float yVal = vector.x * Mathf.Sin(angle) + vector.y * Mathf.Cos(angle);
            return new Vector2(xVal, yVal);
        }

        /// <summary>
        /// Rotate 2D point (XY)
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="angle"></param>
        /// <param name="clockwise"></param>
        /// <returns></returns>
        public static Vector3 RotateXY(this Vector3 vector, float angle, bool clockwise) //angle in radians
        {
            if (clockwise)
            {
                angle = 2 * Mathf.PI - angle;
            }

            float xVal = vector.x * Mathf.Cos(angle) - vector.y * Mathf.Sin(angle);
            float yVal = vector.x * Mathf.Sin(angle) + vector.y * Mathf.Cos(angle);
            return new Vector3(xVal, yVal, 0);
        }

        /// <summary>
        /// Rotate 2D point (XZ)
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="angle"></param>
        /// <param name="clockwise"></param>
        /// <returns></returns>
        public static Vector3 RotateXZ(this Vector3 vector, float angle, bool clockwise) //angle in radians
        {
            if (clockwise)
            {
                angle = 2 * Mathf.PI - angle;
            }

            float xVal = vector.x * Mathf.Cos(angle) - vector.z * Mathf.Sin(angle);
            float yVal = vector.x * Mathf.Sin(angle) + vector.z * Mathf.Cos(angle);
            return new Vector3(xVal, 0, yVal);
        }

        /// <summary>
        /// Rotate 2D point (YZ)
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="angle"></param>
        /// <param name="clockwise"></param>
        /// <returns></returns>
        public static Vector3 RotateYZ(this Vector3 vector, float angle, bool clockwise) // angle in radians
        {
            if (clockwise)
            {
                angle = 2 * Mathf.PI - angle;
            }

            float xVal = vector.y * Mathf.Cos(angle) - vector.z * Mathf.Sin(angle);
            float yVal = vector.y * Mathf.Sin(angle) + vector.z * Mathf.Cos(angle);
            return new Vector3(0, xVal, yVal);
        }

        /// <summary>
        /// Spiral translation around a position (offset) in XZ plane
        /// </summary>
        /// <param name="self"></param>
        /// <param name="offset">point</param>
        /// <param name="rotationSpeed">rotation speed</param>
        /// <param name="forwardSpeed">distance between actor and point</param>
        /// <returns></returns>
        public static Vector3 SpiralLerpXZ(this Vector3 self, Vector3 offset, float rotationSpeed, float forwardSpeed)
        {
            self.x = Mathf.Sin(rotationSpeed) * forwardSpeed;
            self.z = Mathf.Cos(rotationSpeed) * forwardSpeed;
            self += offset;
            return self;
        }

        /// <summary>
        /// Spiral translation around a position (offset) in YZ plane
        /// </summary>
        /// <param name="self"></param>
        /// <param name="offset">point</param>
        /// <param name="rotationSpeed">rotation speed</param>
        /// <param name="forwardSpeed">distance between actor and point</param>
        /// <returns></returns>
        public static Vector3 SpiralLerpYZ(this Vector3 self, Vector3 offset, float rotationSpeed, float forwardSpeed)
        {
            self.y = Mathf.Sin(rotationSpeed) * forwardSpeed;
            self.z = Mathf.Cos(rotationSpeed) * forwardSpeed;
            self += offset;
            return self;
        }

        /// <summary>
        /// Spiral translation around a position (offset) in XY plane
        /// </summary>
        /// <param name="self"></param>
        /// <param name="offset">point</param>
        /// <param name="rotationSpeed">rotation speed</param>
        /// <param name="forwardSpeed">distance between actor and point</param>
        /// <returns></returns>
        public static Vector3 SpiralLerpXY(this Vector3 self, Vector3 offset, float rotationSpeed, float forwardSpeed)
        {
            self.x = Mathf.Sin(rotationSpeed) * forwardSpeed;
            self.y = Mathf.Cos(rotationSpeed) * forwardSpeed;
            self += offset;
            return self;
        }

        #endregion

        #region Quaternion

        /// <summary>
        /// Clamp rotation angles
        /// </summary>
        public static Quaternion ClampRotation(this Quaternion self, float minimumX, float maximumX, float minimumY,
            float maximumY, float minimumZ, float maximumZ)
        {
            self.x /= self.w;
            self.y /= self.w;
            self.z /= self.w;
            self.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(self.x);
            angleX = Mathf.Clamp(angleX, minimumX, maximumX);
            self.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(self.y);
            angleY = Mathf.Clamp(angleY, minimumY, maximumY);
            self.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);

            float angleZ = 2.0f * Mathf.Rad2Deg * Mathf.Atan(self.z);
            angleZ = Mathf.Clamp(angleZ, minimumZ, maximumZ);
            self.z = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleZ);

            return self;
        }

        /// <summary>
        /// Clamp rotation angles
        /// </summary>
        /// <param name="self"></param>
        /// <param name="min">minimum angles</param>
        /// <param name="max">maximum angles</param>
        public static Quaternion ClampRotation(this Quaternion self, Vector3 min, Vector3 max)
        {
            self.x /= self.w;
            self.y /= self.w;
            self.z /= self.w;
            self.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(self.x);
            angleX = Mathf.Clamp(angleX, min.x, max.x);
            self.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(self.y);
            angleY = Mathf.Clamp(angleY, min.y, max.y);
            self.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);

            float angleZ = 2.0f * Mathf.Rad2Deg * Mathf.Atan(self.z);
            angleZ = Mathf.Clamp(angleZ, min.z, max.z);
            self.z = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleZ);

            return self;
        }

        public static Quaternion ClampRotationAroundX(this Quaternion self, float minimumX, float maximumX)
        {
            self.x /= self.w;
            self.y /= self.w;
            self.z /= self.w;
            self.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(self.x);
            angleX = Mathf.Clamp(angleX, minimumX, maximumX);
            self.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return self;
        }

        public static Quaternion ClampRotationAroundY(this Quaternion self, float minimumY, float maximumY)
        {
            self.x /= self.w;
            self.y /= self.w;
            self.z /= self.w;
            self.w = 1.0f;

            float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(self.y);
            angleY = Mathf.Clamp(angleY, minimumY, maximumY);
            self.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);

            return self;
        }

        public static Quaternion ClampRotationAroundZ(this Quaternion self, float minimumZ, float maximumZ)
        {
            self.x /= self.w;
            self.y /= self.w;
            self.z /= self.w;
            self.w = 1.0f;

            float angleZ = 2.0f * Mathf.Rad2Deg * Mathf.Atan(self.z);
            angleZ = Mathf.Clamp(angleZ, minimumZ, maximumZ);
            self.z = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleZ);

            return self;
        }

        #endregion

        #region Math

        public static float ChangeRange(this float value, float firstMin, float firstMax,
            float secondMin, float secondMax)
        {
            return (value - firstMin) *
                Mathf.Abs((secondMax - secondMin) / (firstMax - firstMin)) + secondMin;
        }

        public static float Remap(this float value, float firstMin, float firstMax,
            float secondMin, float secondMax)
        {
            float t = Mathf.InverseLerp(firstMin, firstMax, value);
            return Mathf.Lerp(secondMin, secondMax, t);
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

        #region List

        public static T GetRandomItem<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static int[] GetRandomlyIndexes(int count, int[] weights)
        {
            int[] visited = new int[weights.Length];

            for (int i = 0; i < visited.Length; i++)
                visited[i] = 1;

            int[] result = new int[count];
            int[] indexes = new int[weights.Length];
            int randomValue, candidateIndex;

            for (int i = 0; i < count; i++)
            {
                if (i > 0 && i % weights.Length == 0)
                {
                    for (int j = 0; j < visited.Length; j++)
                        visited[j] = 1;
                }

                indexes[0] = weights[0] * visited[0];
                for (int j = 1; j < weights.Length; j++)
                {
                    indexes[j] = indexes[j - 1] + (weights[j] * visited[j]);
                }

                randomValue = Random.Range(1, indexes[indexes.Length - 1]);
                candidateIndex = 0;
                for (int j = 0; j < indexes.Length; j++)
                {
                    if (randomValue <= indexes[j])
                    {
                        candidateIndex = j;
                        break;
                    }
                }

                result[i] = candidateIndex;
                visited[candidateIndex] = 0;
            }

            return result;
        }

        #endregion

        #region Layer

        public static bool Contains(this LayerMask self, int layer)
        {
            return ((int)Mathf.Pow(2, layer) & self) != 0;
        }

        #endregion

        #region Color

        /// <summary>
        /// convert a color to hex format (#RRGGBB or #AARRGGBB) 
        /// </summary>
        public static string ColorToHex(this Color self, bool alphaChannel = false)
        {
            int a = (int)(self.a * 255);
            int r = (int)(self.r * 255);
            int g = (int)(self.g * 255);
            int b = (int)(self.b * 255);
            return "#" + (alphaChannel ? a.ToString("X2") : "") + r.ToString("X2") + g.ToString("X2") +
                   b.ToString("X2");
        }

        public static Color ToGrayscale(this Color self)
        {
            // grayscale = 0.299r + 0.587g + 0.114b.
            self.r *= .299f;
            self.g *= .587f;
            self.b *= .114f;
            return self;
        }

        #endregion

        #region UI

        public static Button SetFormattedText(this Button self, params object[] args)
        {
            if (self == null) return null;

            var view = self.GetComponentInChildren<Text>();
            if (view != null)
            {
                view.text = string.Format(view.text, args);
                return self;
            }

            return self;
        }

        #endregion

        #region Texture

        public static Texture2D DrawCircle(this Texture2D tex, Color color, int x, int y, int radius = 3)
        {
            float rSquared = radius * radius;

            for (int u = x - radius; u < x + radius + 1; u++)
                for (int v = y - radius; v < y + radius + 1; v++)
                    if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
                        tex.SetPixel(u, v, color);

            return tex;
        }

        #endregion


    }
}