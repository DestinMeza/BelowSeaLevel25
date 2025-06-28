using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BelowSeaLevel_25.AI
{
    /// <summary>
    /// This data structure contains AI behaviors
    /// </summary>
    internal class Tree
    {
        public MonoEntity MonoEntity => m_MonoEntity;
        private MonoEntity m_MonoEntity;

        public Node RootNode => m_RootNode;
        private Node m_RootNode;

        public Tree(MonoEntity monoEntity, Node rootNode)
        {
            m_MonoEntity = monoEntity;
            m_RootNode = rootNode;
        }

        public void Process()
        {
            m_RootNode.Process(MonoEntity);
        }

    }
}