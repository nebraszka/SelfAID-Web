using System.ComponentModel.DataAnnotations;

namespace SelfAID.CommonLib.Dtos.Emotion
{
    public class AddEmotionDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}