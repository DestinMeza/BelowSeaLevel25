using System.Collections;
using UnityEngine;

namespace BelowSeaLevel_25
{
    public static class Globals
    {
        public static class GameState
        {
            public static bool IsPlaying;
            public static MonoCannonEntity ActivePlayer;
            public static Vector3 PlayerStartingPosition;
        }

        public static class Enums
        {
            public enum UIState
            {
                FreePointer,
                HandMode,
                PlayCardMode,
                SelectDiscard
            }
        }

        public static class Coroutines
        {
            public class WaitTillReachedDistance : CustomYieldInstruction
            {
                private Transform m_ApprochingTransform;
                private Transform m_TargetTransform;
                private float m_Distance;

                public WaitTillReachedDistance(Transform approchingTransform, Transform targetTransform, float distance)
                {
                    m_ApprochingTransform = approchingTransform;
                    m_TargetTransform = targetTransform;
                    m_Distance = distance;
                }

                public override bool keepWaiting => Vector3.Distance(m_ApprochingTransform.position, m_TargetTransform.position) > m_Distance;
            }
        }
    }
}