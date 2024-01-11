using Microsoft.EntityFrameworkCore;
using SelfAID.CommonLib.Models;
using SelfAID.API.Data.DataGenerators;

namespace SelfAID.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Emotion> Emotions => Set<Emotion>();

        public void SeedDatabase()
        {
            var emotionGenerator = new EmotionGenerator();
            var emotions = emotionGenerator.CreateEmotionFaker().Generate(50);
            this.Emotions.AddRange(emotions);
            this.SaveChanges();
        }
    }
}