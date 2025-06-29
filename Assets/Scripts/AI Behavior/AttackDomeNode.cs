using System.Collections;
using UnityEngine;
using static BelowSeaLevel_25.Globals;


namespace BelowSeaLevel_25.AI
{
    internal class AttackDomeNode : LeafNode
    {
        private NodeState m_NodeState = NodeState.Running;
        private MonoEntity m_MonoEntity;
        private bool m_IsRunning;
        public AttackDomeNode() : base()
        {

        }

        public override void Reset()
        {
            m_MonoEntity?.StopCoroutine(AttackDomeSequence());
            m_IsRunning = false;
            m_NodeState = NodeState.Running;
        }

        public override NodeState Process(MonoEntity monoEntity)
        {
            m_MonoEntity = monoEntity;

            if (!m_IsRunning)
            {
                monoEntity.StartCoroutine(AttackDomeSequence());
            }

            return m_NodeState;
        }

        private IEnumerator AttackDomeSequence()
        {
            m_IsRunning = true;
            var enemyEntity = m_MonoEntity as MonoEnemyEntity;

            Vector3 targetDirection = GameState.ActivePlayer.transform.position - enemyEntity.transform.position;
            targetDirection = targetDirection.normalized;
            targetDirection *= -1;

            float backupSwimSpeed = 2;
            enemyEntity.SetTargetVelocity(targetDirection * backupSwimSpeed);
            enemyEntity.SetRelativeFacingDirection(targetDirection);
            yield return new WaitForSeconds(1.0f);

            enemyEntity.StartAttackEffect();

            targetDirection = GameState.ActivePlayer.transform.position - enemyEntity.transform.position;
            targetDirection = targetDirection.normalized;

            float attackSwimSpeed = 10;
            enemyEntity.SetTargetVelocity(targetDirection * attackSwimSpeed);
            enemyEntity.SetRelativeFacingDirection(targetDirection);
            
            yield return new Coroutines.WaitTillReachedDistance(m_MonoEntity.transform, GameState.ActivePlayer.transform, 1);
            m_IsRunning = false;
            m_NodeState = NodeState.Success;
        }
    }
}