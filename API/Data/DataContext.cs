using Microsoft.EntityFrameworkCore;
using SelfAID.CommonLib.Models;
using SelfAID.API.Data.DataGenerators;
using SelfAID.CommonLib.Models.User;

namespace SelfAID.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Emotion> Emotions => Set<Emotion>();
        public DbSet<User> Users => Set<User>();

        public void SeedDatabase()
        {
            var emotionGenerator = new EmotionGenerator();
            var emotions = emotionGenerator.CreateEmotionFaker().Generate(50);
            this.Emotions.AddRange(emotions);
            this.SaveChanges();
        }
    }
}