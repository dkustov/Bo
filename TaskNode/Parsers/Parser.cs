using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskNode.ParseModes;

namespace TaskNode.Parsers
{
    public class Parser
    {
        private List<ICharAction> Parsers { get; }

        public Parser()
        {
            Parsers = new List<ICharAction>()
            {
                new NameCharAcceptor(),
                new AssignCharAcceptor(),
                new ValueCharAcceptor()
            };
        }

        private IEnumerator<ICharAction> _currentParserEnumerator;
        private IEnumerator<ICharAction> CurrentParserEnumerator
        {
            get
            {
                if (_currentParserEnumerator == null)
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
