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
        var response = await emotionService.GetAllEmotions();

        if (response != null)
        {
            if (response.Success)
            {
                emotions = response.Data;
            }
            else
            {
                emotions = new List<GetEmotionDto>();
                emotionsListMessage = $"{response.Message}";
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
}