using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskNode.Parsers;
using TaskNode.NodesCreation;

namespace TaskNode.Parsers
{
    public class Parser
    {
        List<ICharAction> _Parsers;

        public List<ICharAction> Parsers
        {
            get
            {
                return _Parsers;
            }
            set
            {
                if ( _Parsers == value )
                    return;
                _Parsers = value;
            }
        }

        public Parser( Node n, INodesFactory nf )
        {
            Parsers = new List<ICharAction>()
            {
                new NameCharAcceptor( n, nf ),
                new AssignCharAcceptor( n, nf ),
                new ValueCharAcceptor( n, nf)
            };
        }

        private IEnumerator<ICharAction> _currentParserEnumerator;
        private IEnumerator<ICharAction> CurrentParserEnumerator
        {
            get
            {
                if ( _currentParserEnumerator == null )
                {
                    _currentParserEnumerator = Parsers.GetEnumerator();
                    _currentParserEnumerator.MoveNext();
                }
                return _currentParserEnumerator;
            }
        }
                
        public bool ReceiveChar(char ch)
        {
            if (!CurrentParserEnumerator.Current.Accept(ch))
            {
                return CurrentParserEnumerator.MoveNext();
            }
            return true;
        }
        
    }
}
