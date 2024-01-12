using SelfAID.CommonLib.Dtos.Emotion;
using SelfAID.CommonLib.Services;
using Microsoft.AspNetCore.Components;

namespace SelfAID.WebClient.Pages;

public partial class EmotionDetails : ComponentBase
{
    protected string Message = string.Empty;
    protected GetEmotionDto emotion {get; set;} = new GetEmotionDto();

    [Parameter]
    public string Name { get; set; }

    [Inject]
    private IEmotionService emotionService { get; set; }

    [Inject]
    private NavigationManager navigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if(!string.IsNullOrEmpty(Name))
        {
            var emotionResponse = await emotionService.GetEmotionByName(Name);
            if(emotionResponse != null && emotionResponse.Data != null)
            {
                emotion = emotionResponse.Data;
            }
        }
    }

    protected async void HandleValidRequest()
    {
        if(string.IsNullOrEmpty(Name))
        {
            var result = await emotionService.AddEmotion(new AddEmotionDto { Name = emotion.Name, Description = emotion.Description });
            if(result != null && result.Success)
            {
                GoToEmotions();
            }
            else
            {
                Message = "Something went wrong. Please try again.";
            }
        }
        else
        {
            var updateResult = await emotionService.UpdateEmotion(new UpdateEmotionDto { Name = emotion.Name, Description = emotion.Description });
            if(updateResult != null && updateResult.Success && updateResult.Data != null)
            {
                GoToEmotions();
            }
            else
            {
                Message = "Something went wrong. Please try again.";
            }
        }
    }

    protected void HandleInvalidRequest()
    {
        Message = "There are some validation errors. Please try again.";
    }

    protected void GoToEmotions()
    {
        navigationManager.NavigateTo("/emocje");
    }

      protected async Task DeleteEmotion()
    {
        Message = string.Empty;
        var emotionResponse = await emotionService.DeleteEmotion(emotion.Name);
        if(emotionResponse != null && emotionResponse.Success)
        {
            navigationManager.NavigateTo("/emocje");
        }
        else
        {
            Message = "Something went wrong. Please try again.";
        }
    }
}