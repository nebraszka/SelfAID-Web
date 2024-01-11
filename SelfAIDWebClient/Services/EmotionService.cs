using System.Text;
using SelfAID.CommonLib.Dtos.Emotion;
using SelfAID.CommonLib.Models;
using SelfAID.CommonLib.Services;

using Newtonsoft.Json;

namespace SelfAID.WebClient.Services;

internal class EmotionService : IEmotionService
{
    private readonly HttpClient _httpClient;

    public EmotionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ServiceResponse<List<GetEmotionDto>>?> AddEmotion(AddEmotionDto emotion)
    {
        try
        {
            var itemJson = new StringContent(JsonConvert.SerializeObject(emotion), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("", itemJson);

            /*
            TODO: Teraz nie mogę tego tak zrobić, bo gdy emocja nie jest unikalna to 
            wtedy jest zwracany Success = false i nie wyłapuję tej odpowiedzi z tym ifem
            if(response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
            }

            return null;
            */

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ServiceResponse<List<GetEmotionDto>>>(content);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<GetEmotionDto>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<List<GetEmotionDto>>?> DeleteEmotion(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServiceResponse<List<GetEmotionDto>>>(responseBody);
            }
            return null;
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<GetEmotionDto>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<List<GetEmotionDto>>?> DeleteAllEmotions()
    {
        try
        {
            var response = await _httpClient.DeleteAsync("");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServiceResponse<List<GetEmotionDto>>>(responseBody);
            }
            return null;
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<GetEmotionDto>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<List<GetEmotionDto>>?> GetAllEmotions()
    {
        try
        {
            var response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServiceResponse<List<GetEmotionDto>>>(responseBody);
            }
            return null;
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<GetEmotionDto>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<GetEmotionDto>?> UpdateEmotion(UpdateEmotionDto emotion)
    {
        try
        {
            var itemJson = new StringContent(JsonConvert.SerializeObject(emotion), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{emotion.Name}", itemJson);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServiceResponse<GetEmotionDto>>(responseBody);
            }
            return null;
        }
        catch (Exception ex)
        {
            return new ServiceResponse<GetEmotionDto>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<GetEmotionDto>?> GetEmotionByName(string name)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{name}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServiceResponse<GetEmotionDto>>(responseBody);
            }
            return null;
        }
        catch (Exception ex)
        {
            return new ServiceResponse<GetEmotionDto>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<List<GetEmotionDto>>?> DeleteEmotion(string name)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{name}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServiceResponse<List<GetEmotionDto>>>(responseBody);
            }
            return null;
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<GetEmotionDto>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public Task<IEnumerable<GetEmotionDto>?> GetEmotionsAsync(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<int?> GetTotalEmotionsCountAsync()
    {
        throw new NotImplementedException();
    }
}
