using UnityEngine;

namespace Moein.Trans.Mirror
{
    public interface IMirror
    {
        Vector3 GetMirrorPosition(Vector3 target);
        Quaternion GetMirrorRotation(Quaternion target);
        Vector3 GetMirrorScale(Vector3 target);
    }
}