using System.Collections;
using UnityEngine;
using static BelowSeaLevel_25.Globals;


namespace BelowSeaLevel_25.AI
{
    internal class MoveToDomeNode : LeafNode
    {
        private NodeState m_NodeState = NodeState.Running;
        private MonoEntity m_MonoEntity;
        private bool m_IsRunning;
        public MoveToDomeNode() : base()
        {

        }

        public override void Reset()
        {
            m_MonoEntity?.StopCoroutine(MoveTowardDomeSequence());
            m_IsRunning = false;
            m_NodeState = NodeState.Running;
        }

        public override NodeState Process(MonoEntity monoEntity)
        {
            m_MonoEntity = monoEntity;

            if (!m_IsRunning)
            {
                monoEntity.StartCoroutine(MoveTowardDomeSequence());
            }

            return m_NodeState;
        }

        private IEnumerator MoveTowardDomeSequence()
        {
            m_IsRunning = true;
            var enemyEntity = m_MonoEntity as MonoEnemyEntity;

            Vector3 targetDirection = GameState.ActivePlayer.transform.position - enemyEntity.transform.position;
            targetDirection = targetDirection.normalized;

            enemyEntity.SetTargetVelocity(targetDirection * enemyEntity.GetSpeed());
            enemyEntity.SetRelativeFacingDirection(targetDirection * -1);
            yield return new Coroutines.WaitTillReachedDistance(m_MonoEntity.transform, GameState.ActivePlayer.transform, 2);

            enemyEntity.SetTargetVelocity(new Vector2(0, 0));

            m_IsRunning = false;
            m_NodeState = NodeState.Success;
        }
    }
}