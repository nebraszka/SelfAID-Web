using SelfAID.API.Data;
using AutoMapper;
using SelfAID.CommonLib.Dtos.Emotion;
using SelfAID.CommonLib.Models;
using SelfAID.CommonLib.Services;
using Microsoft.EntityFrameworkCore;

// Mapowanie repos
// try catch middleware

namespace SelfAID.API.Services.EmotionService
{
    public class EmotionService : IEmotionService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public EmotionService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetEmotionDto>>?> AddEmotion(AddEmotionDto addEmotionDto)
        {
            var serviceResponse = new ServiceResponse<List<GetEmotionDto>>();
            // Emotion name cannot be empty, but description can
            if (addEmotionDto.Name == string.Empty)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Emotion name is required.";
                return serviceResponse;
            }
            try
            {
                if (await _context.Emotions.AnyAsync(e => e.Name.ToLower() == addEmotionDto.Name.ToLower()))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Emotion name must be unique. Emotion with name {addEmotionDto.Name} already exists.";
                    return serviceResponse;
                }

                var emotion = new Emotion
                {
                    Name = addEmotionDto.Name,
                    Description = addEmotionDto.Description
                };

                await _context.Emotions.AddAsync(emotion);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Emotions.Select(e => _mapper.Map<GetEmotionDto>(e)).ToListAsync();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error adding emotion: " + ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetEmotionDto>>?> GetAllEmotions()
        {
            var serviceResponse = new ServiceResponse<List<GetEmotionDto>>();
            try
            {
                var dbEmotions = await _context.Emotions
                                                .Select(e => _mapper.Map<GetEmotionDto>(e))
                                                .ToListAsync();

                serviceResponse.Data = dbEmotions;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error getting emotions: " + ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetEmotionDto>?> GetEmotionByName(string name)
        {
            var serviceResponse = new ServiceResponse<GetEmotionDto>();
            try
            {
                var emotion = await _context.Emotions
                                            .Where(e => e.Name == name)
                                            .Select(e => _mapper.Map<GetEmotionDto>(e))
                                            .FirstOrDefaultAsync();

                if (emotion == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Emotion {name} not found.";
                    return serviceResponse;
                }

                serviceResponse.Data = emotion;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error getting emotion {name}: " + ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetEmotionDto>?> UpdateEmotion(UpdateEmotionDto updateEmotionDto)
        {
            var serviceResponse = new ServiceResponse<GetEmotionDto>();
            try
            {
                var emotion = await _context.Emotions.FirstOrDefaultAsync(e => e.Name == updateEmotionDto.Name);
                if (emotion == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Emotion {updateEmotionDto.Name} not found.";
                    return serviceResponse;
                }
                if (updateEmotionDto.Name != string.Empty)
                {
                    emotion.Name = updateEmotionDto.Name;
                }
                if (updateEmotionDto.Description != string.Empty)
                {
                    emotion.Description = updateEmotionDto.Description;
                }
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetEmotionDto>(emotion);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error updating emotion: " + ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEmotionDto>>?> DeleteEmotion(string name)
        {
            var serviceResponse = new ServiceResponse<List<GetEmotionDto>>();
            try
            {
                var emotion = await _context.Emotions.FirstOrDefaultAsync(e => e.Name == name);

                if (emotion == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Emotion {name} not found.";
                    return serviceResponse;
                }

                _context.Emotions.Remove(emotion);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Emotions.Select(e => _mapper.Map<GetEmotionDto>(e)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error deleting emotion {name}: " + ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEmotionDto>>?> DeleteAllEmotions()
        {
            var serviceResponse = new ServiceResponse<List<GetEmotionDto>>();
            try
            {
                var emotions = await _context.Emotions.ToListAsync();
                if (emotions == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"No emotions found.";
                    return serviceResponse;
                }
                _context.Emotions.RemoveRange(emotions);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Emotions.Select(e => _mapper.Map<GetEmotionDto>(e)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error deleting emotions: " + ex.Message;
            }
            return serviceResponse;
        }

        public async Task<IEnumerable<GetEmotionDto>?> GetEmotionsAsync(int pageNumber, int pageSize)
        {
            var emotions = await _context.Emotions
                                            .Skip((pageNumber - 1) * pageSize)
                                            .Take(pageSize)
                                            .Select(e => _mapper.Map<GetEmotionDto>(e))
                                            .ToListAsync();
            return emotions;
        }

        public async Task<int?> GetTotalEmotionsCountAsync()
        {
            var totalEmotionsCount = await _context.Emotions.CountAsync();
            return totalEmotionsCount;
        }
    }
}