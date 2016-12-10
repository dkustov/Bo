using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskNode.IdGeneration;

namespace TaskNode
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
