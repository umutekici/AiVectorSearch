using AiVectorSearch.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiVectorSearch.Core.Data
{
    public class AIVectorSearchDbContext : DbContext
    {
        public AIVectorSearchDbContext(DbContextOptions<AIVectorSearchDbContext> options) : base(options) { }

        public DbSet<DocumentSegment> DocumentSegments { get; set; }
    }
}
