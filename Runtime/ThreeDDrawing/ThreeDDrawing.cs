using UnityEngine;

namespace Moein.Trans.Drawing
{
    public abstract class ThreeDDrawing : MonoBehaviour
    {
        public Color color = Color.black;
        public float length = 1;

        protected virtual void OnDrawGizmos()
        {
        }
    }
}