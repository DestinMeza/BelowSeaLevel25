using System.Collections;
using UnityEngine;
using static BelowSeaLevel_25.Globals;


namespace BelowSeaLevel_25.AI
{
    internal class MoveToDomeNode : LeafNode
    {
        private MonoEntity m_MonoEntity;
        private float m_StartTime;
        private bool m_IsRunning;
        public MoveToDomeNode() : base()
        {

        }

        public override NodeState Process(MonoEntity monoEntity)
        {
            m_MonoEntity = monoEntity;

            if (!m_IsRunning)
            {
                m_StartTime = Time.time;
                monoEntity.StartCoroutine(MoveTowardDomeSequence());
            }

            return NodeState.Running;
        }

        private IEnumerator MoveTowardDomeSequence()
        {
            var enemyEntity = m_MonoEntity as MonoEnemyEntity;

            Vector3 targetDirection = GameState.ActivePlayer.transform.position - enemyEntity.transform.position;
            targetDirection = targetDirection.normalized;

            enemyEntity.SetTargetVelocity(targetDirection * enemyEntity.GetSpeed());
            yield return new Coroutines.WaitTillReachedDistance(m_MonoEntity.transform, GameState.ActivePlayer.transform, 2);

            m_IsRunning = false;
        }
    }
}