using System;
namespace TaskNode.Parsers.ValueParsers
{
    public class RowValueParser: IValueParser
    {
        public Node CurrentNode { get; set; }
        private string RowValue
        {
            get { return CurrentNode.Value; }
            set
            {
                if ( CurrentNode == null )
                    throw new Exception( "Ошибка алгоритма - нет текущего узла" );
                CurrentNode.Value = value;
            }
        }

        public RowValueParser( Node currentNode )
        {
            CurrentNode = currentNode;
        }

        public bool IsAcceptedFirstChar(char ch)
        {
            return ch == '"';
        }

        public bool AcceptChar(char ch)
        {
            if ( ch!='"' )
                RowValue = RowValue + ch;
            return ch != '"';
        }
    }
}