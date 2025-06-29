namespace BelowSeaLevel_25.AI
{
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }

    internal abstract class Node
    {
        public Node()
        {

        }

        public virtual void Reset() { }

        public abstract NodeState Process(MonoEntity monoEntity);
    }
}