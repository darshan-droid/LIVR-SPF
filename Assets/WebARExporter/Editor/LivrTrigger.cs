using UnityEngine;

namespace Livr.WebAR
{
    [DisallowMultipleComponent]
    public class LivrTrigger : MonoBehaviour
    {
        public string TriggerId = "Trigger_01";
        public string TargetTag = "Player";
        public string OnEnterAction = "ScoreUp";
        public float Radius = 0.15f;
    }
}
