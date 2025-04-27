namespace AiVectorSearch.Application.DTOs
{
    public class SearchRequest
    {
        public string Query { get; set; }

        public SearchRequest(string query)
        {
            Query = query;
        }
    }
}
