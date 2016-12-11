namespace TaskNode.Parsers
{
    public interface ICharAction
    {
        Node currentNode { get; set; }
        bool Accept(char ch);
        
    }
}