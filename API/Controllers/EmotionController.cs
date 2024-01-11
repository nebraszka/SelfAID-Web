using SelfAID.CommonLib.Services;
using SelfAID.CommonLib.Dtos.Emotion;
using SelfAID.CommonLib.Models;
using Microsoft.AspNetCore.Mvc;

namespace SelfAID.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmotionController : ControllerBase
    {
        private readonly IEmotionService _emotionService;

        public EmotionController(IEmotionService emotionService)
        {
            _emotionService = emotionService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetEmotionDto>>>> GetAllEmotion() 
        {
            var response = await _emotionService.GetAllEmotions();
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        [HttpGet("{name}")]
        public async Task<ActionResult<ServiceResponse<GetEmotionDto>>> GetEmotionByName(string name)
        {
            var response = await _emotionService.GetEmotionByName(name);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetEmotionDto>>>> PostEmotion([FromBody] AddEmotionDto emotion)
        {
            var response = await _emotionService.AddEmotion(emotion);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<UpdateEmotionDto>>> PutEmotion([FromBody] UpdateEmotionDto updateEmotionDto)
        {
            var response = await _emotionService.UpdateEmotion(updateEmotionDto);
            return Ok(response);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> DeleteEmotion(string name)
        {
            var response = await _emotionService.DeleteEmotion(name);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAllEmotions()
        {
            var response = await _emotionService.DeleteAllEmotions();
            return Ok(response);
        }

        [HttpGet("page/{pageNumber}/pageSize/{pageSize}")]
        public async Task<ActionResult<IEnumerable<Emotion>>> GetEmotionsAsync(int pageNumber, int pageSize)
        {
            var response = await _emotionService.GetEmotionsAsync(pageNumber, pageSize);
            return Ok(response);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetTotalEmotionsCountAsync()
        {
            var response = await _emotionService.GetTotalEmotionsCountAsync();
            return Ok(response);
        }
    }
} 