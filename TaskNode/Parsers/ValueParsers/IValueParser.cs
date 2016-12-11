namespace TaskNode.Parsers.ValueParsers
{
    public interface IValueParser
    {
        bool IsAcceptedFirstChar(char ch);
        bool AcceptChar(char ch);
    }
}