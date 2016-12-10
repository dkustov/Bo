namespace TaskNode.IdGeneration
{
    public class IntIdGenerator : IIdGenerator
    {
        private int Id { get; set; }

        public string GetNextId()
        {
            return (++Id).ToString();
        }
    }
}
