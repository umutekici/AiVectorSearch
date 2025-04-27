namespace AiVectorSearch.Core.Entities
{
    public class DocumentSegment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? EmbeddingJson { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
