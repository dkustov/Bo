using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskNode.Parsers
{
    public abstract class CharAction : ICharAction
    {
        public Node CurrentNode {get; set;}

        public abstract CharAction( Node node );
       
        public abstract bool Accept( char ch );
    }
}
