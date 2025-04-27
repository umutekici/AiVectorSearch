using AiVectorSearch.Application.Helpers;
using AiVectorSearch.Application.Interfaces;
using AiVectorSearch.Core.Data;
using AiVectorSearch.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AiVectorSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly AIVectorSearchDbContext _context;
        private readonly IHuggingFaceService _huggingFaceService;
        private readonly IWebHostEnvironment _env;

        public DocumentController(AIVectorSearchDbContext context, IHuggingFaceService huggingFaceService, IWebHostEnvironment env)
        {
            _context = context;
            _huggingFaceService = huggingFaceService;
            _env = env;
        }

        [HttpPost("load-from-file")]
        public async Task<IActionResult> LoadFromTxtFile()
        {
            var filePath = Path.Combine(_env.WebRootPath, "docs", "Products.txt");

            if (!System.IO.File.Exists(filePath))
                return NotFound("Not found txt file.");

            var content = await System.IO.File.ReadAllTextAsync(filePath);
            var formattedContent = content.Replace(". ", ".\n");

            var segments = formattedContent.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var segment in segments)
            {
                var cleaned = segment.Trim();
                if (string.IsNullOrWhiteSpace(cleaned)) continue;

                var embedding = await _huggingFaceService.GetEmbeddingAsync(cleaned);

                var documentSegment = new DocumentSegment
                {
                    Content = cleaned,
                    EmbeddingJson = JsonConvert.SerializeObject(embedding),
                    CreatedAt = DateTime.Now
                };

                _context.DocumentSegments.Add(documentSegment);
            }

            await _context.SaveChangesAsync();
            return Ok("Data loaded successfully.");
        }


        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] string question)
        {
            var questionEmbedding = await _huggingFaceService.GetEmbeddingAsync(question);
            var segments = await _context.DocumentSegments.ToListAsync();

            var matches = new List<(string Content, double Similarity)>();

            foreach (var seg in segments)
            {
                var segmentEmbedding = JsonConvert.DeserializeObject<List<float>>(seg.EmbeddingJson);

                var similarity = VectorHelper.CosineSimilarity(questionEmbedding, segmentEmbedding);

                if (similarity > 0.50)
                {
                    matches.Add((seg.Content, similarity));
                }
            }

            var sortedMatches = matches.OrderByDescending(m => m.Similarity).Take(5).ToList();

            if (sortedMatches.Count == 0)
            {
                return Ok("No suitable answer found.");
            }

            var bestMatches = sortedMatches.Select(m => m.Content).ToList();

            return Ok(bestMatches);
        }

    }
}
