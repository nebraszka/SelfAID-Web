using Bogus;
using SelfAID.CommonLib.Models;

namespace SelfAID.API.Data.DataGenerators
{
    public class EmotionGenerator
    {
        public Faker<Emotion> CreateEmotionFaker()
        {
            var emotionFaker = new Faker<Emotion>()
                .RuleFor(e => e.Name, f => f.Lorem.Word())
                .RuleFor(e => e.Description, f => f.Lorem.Sentence(5));

            return emotionFaker;
        }
    }
}