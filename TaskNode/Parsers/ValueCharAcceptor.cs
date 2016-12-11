using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskNode.Parsers;
using TaskNode.NodesCreation;

namespace TaskNode.Parsers
{
    public class ValueCharAcceptor: ICharAction
    {
        public Node currentNode { get; set; }
        public INodesFactory nodesFactory { get; set; }
        bool quoteMode = false;
        bool ObjectMode = false;
        Parser parser;
        private string nodeValue
        {
            get { return currentNode.Value; }
            set
            {
                if ( currentNode == null )
                    throw new Exception( "Ошибка алгоритма - нет текущего узла" );
                currentNode.Value = value;
            }
        }

        public ValueCharAcceptor( Node n, INodesFactory nf )
        {
            currentNode = n;
            nodesFactory = nf;
        }



        public bool Accept(char ch)
        {
            if ( ObjectMode )
            {
                if ( parser == null )
                    parser = new Parser( currentNode, nodesFactory );
                bool res = parser.ReceiveChar( ch );
            }
                
            if ( quoteMode )
            {
                if ( ch == '"' )
                {
                    quoteMode = false;
                    currentNode.Value = nodeValue;
                    return false;
                }
                else
                {
                    nodeValue = nodeValue + ch;
                    return true;
                }
            }
            else
            {
                if (ch == '{')
                {

                    currentNode.AddChild( nodesFactory.CreateNode() );
                    currentNode = currentNode.ChildList.Last();
                    ObjectMode = true;
                    return true;
                }
                if ( ch == '}' )
                {
                    if ( currentNode.Parent == null )
                        throw new Exception("лишняя закрывающая скобка");
                    //treeNode = nodesFactory.CreateNode();
                    currentNode = currentNode.Parent;
                    ObjectMode = false;

                    return false;
                }


                if ( ch == '"' )
                {
                    quoteMode = true;
                }
            }


           //if ( !quoteMode 

            return true;
        }
    }
}
