using SelfAID.CommonLib.Dtos.Emotion;
using SelfAID.CommonLib.Services;
using Microsoft.AspNetCore.Components;
using SelfAID.WebClient.Services;

namespace SelfAID.WebClient.Pages;

public partial class EmotionDetails : ComponentBase
{
    protected string Message = string.Empty;
    protected GetEmotionDto emotion = new GetEmotionDto();

    [Parameter]
    public string Name { get; set; }

    [Inject]
    private IEmotionService emotionService { get; set; }

    [Inject]
    private NavigationManager navigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if(string.IsNullOrEmpty(Name))
        {
            // Adding a new emotion
        }
        else
        {
            // Updating a new emotion
            var apiEmotion = await emotionService.GetEmotionByName(Name);
        }
    }
}