using System;
using System.Collections.Generic;
using TaskNode.NodesCreation;


namespace TaskNode.Parsers.ValueParsers
{
    public class ObjectValueParser : IValueParser
    {
        public Node CurrentNode { get; set; }
        private INodesFactory NodesFactory;
        private Parser Parser { get; set; }

        public ObjectValueParser(Node node, INodesFactory nodesFactory)
        {
            CurrentNode = node;
            NodesFactory = nodesFactory;
            /////////////УШЛИ в РЕКУРСИЮ
            //////////////////////////////////////Parser = new Parser(node, nodesFactory);
        }

        public bool IsAcceptedFirstChar(char ch)
        {
            return ch == '{';
        }

        public bool AcceptChar(char ch)
        {
            if ( ch == '{' )
            {
                CurrentNode.AddChild( NodesFactory.CreateNode() );
                CurrentNode = CurrentNode.ChildList[CurrentNode.ChildList.Count - 1];
            }
                /*
            if(ch == '}')
                ....
             */
            if ( Parser == null )
                Parser = new Parser( CurrentNode, NodesFactory );
            Parser.ReceiveChar( ch );
            return true;
        }
    }
}