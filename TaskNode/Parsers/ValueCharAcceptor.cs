using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskNode.Parsers;
using TaskNode.NodesCreation;
using TaskNode.Parsers.ValueParsers;

namespace TaskNode.Parsers
{
    public class ValueCharAcceptor: ICharAction
    {
        public Node CurrentNode { get; set; }
        private INodesFactory NodesFactory { get; set; }
        private IValueParser CurrentParser { get; set; }
        private IEnumerable<IValueParser> ValueParsers { get; set; }

        public ValueCharAcceptor(Node currentNode, INodesFactory nodesFactory)
        {
            CurrentNode = currentNode;
            NodesFactory = nodesFactory;
            ValueParsers = new List<IValueParser>()
            {
                new ObjectValueParser( currentNode, nodesFactory ),
                new RowValueParser( currentNode )
            };
        }


       

        public bool Accept(char ch)
        {
            if (CurrentParser == null)
            {
                CurrentParser = GetParser(ch);
                return CurrentParser != null || CheckFirstEmptyChar(ch);
            }

            return CurrentParser.AcceptChar(ch);
        }

        private bool CheckFirstEmptyChar(char ch)
        {
            return char.IsWhiteSpace(ch) || char.IsControl(ch);
        }

        private IValueParser GetParser(char ch)
        {
            return ValueParsers.FirstOrDefault(valueParser => valueParser.IsAcceptedFirstChar(ch));
        }
    }
}
