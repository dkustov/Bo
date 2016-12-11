using TaskNode.NodesCreation;

namespace TaskNode.Parsers.ValueParsers
{
    public class ObjectValueParser : IValueParser
    {
        private Parser Parser { get; set; }

        public ObjectValueParser(Node node, INodesFactory nodesFactory)
        {
            Parser = new Parser(node, nodesFactory);
        }

        public bool IsAcceptedFirstChar(char ch)
        {
            return ch == '{';
        }

        public bool AcceptChar(char ch)
        {
            if(ch == '{')
            if(ch == '}')
                ....
            Parser.ReceiveChar(ch);
        }
    }
}