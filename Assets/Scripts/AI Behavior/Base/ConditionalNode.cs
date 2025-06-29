using System;

namespace BelowSeaLevel_25.AI
{
    internal class ConditionalNode : Node
    {
        protected Func<bool> m_condition;
        protected Node m_nodeToExecuteOnSucess;
        public ConditionalNode(Func<bool> condition, Node nodeToExecuteOnSucess) : base()
        {
            m_condition = condition;
            m_nodeToExecuteOnSucess = nodeToExecuteOnSucess;
        }

        public override void Reset()
        {
            m_nodeToExecuteOnSucess.Reset();
        }

        public override NodeState Process(MonoEntity monoEntity)
        {
            bool state = m_condition();

            if (state)
            {
                return m_nodeToExecuteOnSucess.Process(monoEntity);
            }

            return NodeState.Success;
        }
    }
}