namespace TaskNode.Parsers
{
    public interface ICharAction
    {
        Node CurrentNode { get; set; }
        bool Accept(char ch);
        
    }
}