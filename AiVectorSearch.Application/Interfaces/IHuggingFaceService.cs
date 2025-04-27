namespace AiVectorSearch.Application.Interfaces
{
    public interface IHuggingFaceService
    {
        Task<List<float>> GetEmbeddingAsync(string text);
    }
}
