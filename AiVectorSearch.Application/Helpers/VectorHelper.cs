namespace AiVectorSearch.Application.Helpers
{
    public static class VectorHelper
    {
        public static double CosineSimilarity(List<float> vectorA, List<float> vectorB)
        {
            if (vectorA.Count != vectorB.Count)
                throw new InvalidOperationException("vectors must be the same size.");

            double dotProduct = 0;
            double magnitudeA = 0;
            double magnitudeB = 0;

            for (int i = 0; i < vectorA.Count; i++)
            {
                dotProduct += vectorA[i] * vectorB[i];
                magnitudeA += Math.Pow(vectorA[i], 2);
                magnitudeB += Math.Pow(vectorB[i], 2);
            }

            if (magnitudeA == 0 || magnitudeB == 0) return 0;

            return dotProduct / (Math.Sqrt(magnitudeA) * Math.Sqrt(magnitudeB));
        }
    }
}
