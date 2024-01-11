using SelfAID.CommonLib.Models;
using SelfAID.CommonLib.Dtos.Emotion;

namespace SelfAID.CommonLib.Services
{
    public interface IEmotionService
    {
        Task<ServiceResponse<List<GetEmotionDto>>?> GetAllEmotions();
        Task<ServiceResponse<GetEmotionDto>?> GetEmotionByName(string name);
        Task<ServiceResponse<List<GetEmotionDto>>?> AddEmotion(AddEmotionDto emotion);
        Task<ServiceResponse<GetEmotionDto>?> UpdateEmotion(UpdateEmotionDto emotion);
        Task<ServiceResponse<List<GetEmotionDto>>?> DeleteEmotion(string name);
        Task<ServiceResponse<List<GetEmotionDto>>?> DeleteAllEmotions();
        Task<IEnumerable<GetEmotionDto>?> GetEmotionsAsync(int pageNumber, int pageSize);
        Task<int?> GetTotalEmotionsCountAsync();
    }
}