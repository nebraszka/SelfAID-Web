using SelfAID.CommonLib.Dtos.Emotion;
using Microsoft.AspNetCore.Components;
using SelfAID.CommonLib.Services;

namespace SelfAID.WebClient.Pages;

public partial class Emotions : ComponentBase
{
    [Inject]
    private IEmotionService emotionService { get; set; }
    public List<GetEmotionDto> emotions { get; set;}
    public AddEmotionDto newEmotion = new AddEmotionDto();
    public string emotionsListMessage = "";
    public string emotionsListClass = "";
    public string addEmotionstatusMessage = "";
    public string addEmotionstatusClass = "";

    protected override async Task OnInitializedAsync()
    {
        var result = await emotionService.GetAllEmotions();

        if (result != null)
        {
            if (result.Success)
            {
                emotions = result.Data;
            }
            else
            {
                emotions = new List<GetEmotionDto>();
                emotionsListMessage = $"{result.Message}";
                emotionsListClass = "alert-danger";
            }
        }
        else
        {
            emotions = new List<GetEmotionDto>();
            emotionsListMessage = "Błąd pobierania danych";
            emotionsListClass = "alert-danger";
        }
    }

    private async Task AddEmotion()
    {
        var result = await emotionService.AddEmotion(newEmotion);

        if (result.Success)
        {
            addEmotionstatusMessage = $"Emocja {newEmotion.Name} dodana";
            addEmotionstatusClass = "alert-success";
            emotions = result.Data;
        }
        else
        {
            addEmotionstatusMessage = $"{result.Message}";
            addEmotionstatusClass = "alert-danger";
            OnInitializedAsync();
        }

        newEmotion = new AddEmotionDto();
    }

    private async Task DeleteEmotion(string name)
    {
        // var result = await EmotionService.DeleteEmotionAsync(id);

        // if (result.Success)
        // {
        //     statusMessage = $"Emocja usunięta";
        //     statusClass = "alert-success";
        //     emotions = result;
        // }
        // else
        // {
        //     statusMessage = $"Error: {result.Message}";
        //     statusClass = "alert-danger";
        //     emotions = await EmotionService.GetEmotionsAsync();
        // }
    }
}