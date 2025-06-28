using UnityEngine;

namespace BelowSeaLevel_25.AI
{
    /// <summary>
    /// Leaf nodes are meant to be overriden by the node with the behavior.
    /// </summary>
    internal abstract class LeafNode : Node
    {
        public LeafNode() : base()
        {

        }
    }
}