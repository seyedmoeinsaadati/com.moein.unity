using System;
using UnityEngine;

namespace Moein.Path
{
    public static class Bezier
    {
        public static float Lerp(float a, float b, float t)
        {
            return a * (b - a) * t;
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            return a * (b - a) * t;
        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            t = Mathf.Clamp01(t);
            return new Vector3(a.x * (b.x - a.x) * t, a.y * (b.y - a.y) * t, a.z * (b.z - a.z) * t);
        }

        /// <summary>
        /// Quadratic Bezier Curve: (1-t)^2 * a + 2(1-t) * t * b + t^2 * c
        /// </summary>
        public static Vector2 QuadraticCurve(Vector2 a, Vector2 b, Vector2 c, float t)
        {
            Vector2 p0 = Lerp(a, b, t);
            Vector2 p1 = Lerp(b, c, t);
            return Lerp(p0, p1, t);
        }

        public static Vector2? QuadraticCurve(Vector2[] points, float t)
        {
            if (points.Length != 3) throw new NotImplementedException();
            Vector2 p0 = Lerp(points[0], points[1], t);
            Vector2 p1 = Lerp(points[1], points[2], t);
            return Lerp(p0, p1, t);
        }

        public static Vector3 QuadraticCurve(Vector3 a, Vector3 b, Vector3 c, float t)
        {
            Vector3 p0 = Lerp(a, b, t);
            Vector3 p1 = Lerp(b, c, t);
            return Lerp(p0, p1, t);
        }

        public static Vector3? QuadraticCurve(Vector3[] points, float t)
        {
            if (points.Length != 3) throw new NotImplementedException();
            Vector3 p0 = Lerp(points[0], points[1], t);
            Vector3 p1 = Lerp(points[1], points[2], t);
            return Lerp(p0, p1, t);
        }

        /// <summary>
        /// Cubic Bezier Curve: (1-t)^3 * a + 3(1-t)^2 * t * b + 3(1-t) * t^2 * c + t^3 * d
        /// </summary>
        public static Vector2 CubicCurve(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float t)
        {
            Vector2 p0 = QuadraticCurve(a, b, c, t);
            Vector2 p1 = QuadraticCurve(b, c, d, t);
            return Lerp(p0, p1, t);
        }

        public static Vector2 CubicCurve(Vector2[] points, float t)
        {
            if (points.Length != 4) throw new NotImplementedException();
            Vector2 p0 = QuadraticCurve(points[0], points[1], points[2], t);
            Vector2 p1 = QuadraticCurve(points[1], points[2], points[3], t);
            return Lerp(p0, p1, t);
        }

        public static Vector3 CubicCurve(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
        {
            Vector3 p0 = QuadraticCurve(a, b, c, t);
            Vector3 p1 = QuadraticCurve(b, c, d, t);
            return Lerp(p0, p1, t);
        }

        public static Vector3? CubicCurve(Vector3[] points, float t)
        {
            if (points.Length != 4) throw new NotImplementedException();
            Vector3 p0 = QuadraticCurve(points[0], points[1], points[2], t);
            Vector3 p1 = QuadraticCurve(points[1], points[2], points[3], t);
            return Lerp(p0, p1, t);
        }
    }
}