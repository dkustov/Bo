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

        private IEnumerable<IValueParser> ValueParsers { get; set; }

        Parser parser;
        private string nodeValue
        {
            get { return CurrentNode.Value; }
            set
            {
                if ( CurrentNode == null )
                    throw new Exception( "Ошибка алгоритма - нет текущего узла" );
                CurrentNode.Value = value;
            }
        }

        public ValueCharAcceptor(Node currentNode, INodesFactory nodesFactory)
        {
            CurrentNode = currentNode;
            NodesFactory = nodesFactory;
            ValueParsers = new List<IValueParser>()
            {
                new ObjectValueParser(),
                new RowValueParser()
            };
        }


        public IValueParser CurrentParser { get; set; }

        public bool Accept(char ch)
        {
            if (CurrentParser == null)
            {
                CurrentParser = GetParser(ch);
                return CurrentParser != null || CheckFirstEmptyChar(ch);
            }

            return CurrentParser.AcceptChar(ch);

            

            if ( ObjectMode )
            {
                if ( parser == null )
                    parser = new Parser( CurrentNode, NodesCreation.NodesFactory );
                bool res = parser.ReceiveChar( ch );
            }
                
            if ( quoteMode )
            {
                if ( ch == '"' )
                {
                    quoteMode = false;
                    CurrentNode.Value = nodeValue;
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

                    CurrentNode.AddChild( NodesCreation.NodesFactory.CreateNode() );
                    CurrentNode = CurrentNode.ChildList.Last();
                    ObjectMode = true;
                    return true;
                }
                if ( ch == '}' )
                {
                    if ( CurrentNode.Parent == null )
                        throw new Exception("лишняя закрывающая скобка");
                    //treeNode = nodesFactory.CreateNode();
                    CurrentNode = CurrentNode.Parent;
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
