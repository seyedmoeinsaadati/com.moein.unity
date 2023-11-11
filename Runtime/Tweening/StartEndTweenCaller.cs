using UnityEngine;

namespace Moein.Tweening
{
    public class StartEndTweenCaller : MonoBehaviour
    {
        public void ToStartAndResetByGroupId(int groupId)
        {
            StartEndTweener.ToStartByGroup(groupId, true);
        }

        public void ToStartByGroupId(int groupId)
        {
            StartEndTweener.ToStartByGroup(groupId);
        }

        public void ToEndAndResetByGroupId(int groupId)
        {
            StartEndTweener.ToEndByGroup(groupId, true);
        }

        public void ToEndByGroupId(int groupId)
        {
            StartEndTweener.ToEndByGroup(groupId);
        }
    }
}