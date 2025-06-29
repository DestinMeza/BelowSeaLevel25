using System;

namespace BelowSeaLevel_25.AI
{
    internal class SelectorNode : Node
    {
        private int currentIndex;
        private Node[] m_childrenNodes;
        public SelectorNode(params Node[] childrenNodes) : base()
        {
            m_childrenNodes = childrenNodes;
        }

        public override void Reset()
        {
            currentIndex = 0;
            for (int i = 0; i < m_childrenNodes.Length; i++)
            {
                m_childrenNodes[i].Reset();
            }
        }

        public override NodeState Process(MonoEntity monoEntity)
        {
            if (currentIndex >= m_childrenNodes.Length)
            {
                currentIndex = 0;
                return NodeState.Success;
            }

            NodeState state = m_childrenNodes[currentIndex].Process(monoEntity);

            switch (state)
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Success:
                    currentIndex++;
                    return NodeState.Success;
                case NodeState.Failure:
                    return NodeState.Failure;
            }

            return state;
        }
    }
}