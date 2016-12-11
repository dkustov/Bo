namespace TaskNode.Parsers.ValueParsers
{
    public class RowValueParser: IValueParser
    {
        public bool IsAcceptedFirstChar(char ch)
        {
            return ch == '"';
        }

        public bool AcceptChar(char ch)
        {
            throw new System.NotImplementedException();
        }
    }
}