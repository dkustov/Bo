namespace TaskNode.NodesCreation
{
    public class NodesFactory : INodesFactory
    {
        public NodesFactory(IIdGenerator idGenerator)
        {
            IdGenerator = idGenerator;
        }

        private IIdGenerator IdGenerator { get; }

        
        public Node CreateNode()
        {
            return new Node(IdGenerator.GetNextId());
        }
    }
}
