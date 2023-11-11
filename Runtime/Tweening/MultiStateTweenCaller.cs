using UnityEngine;

namespace Moein.Tweening
{
    public class MultiStateTweenCaller : MonoBehaviour
    {
        public void PlayAndResetByGroupId(int groupId)
        {
            MultiStateTweener.PlayByGroup(groupId, true);
        }

        public void PlayByGroupId(int groupId)
        {
            MultiStateTweener.PlayByGroup(groupId);
        }

        public void StopAndResetByGroupId(int groupId)
        {
            MultiStateTweener.StopByGroup(groupId, true);
        }

        public void StopByGroupId(int groupId)
        {
            MultiStateTweener.StopByGroup(groupId);
        }
    }
}