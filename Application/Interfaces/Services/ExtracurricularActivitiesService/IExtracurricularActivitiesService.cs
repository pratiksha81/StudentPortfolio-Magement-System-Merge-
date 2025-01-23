using Application.Dto.ECA;

namespace Application.Interfaces.Services.ExtracurricularActivitiesService
{
    public interface IExtracurricularActivitiesService
    {
        public Task<List<ExtracurricularActivitiesDto>> GetAllExtracurricularActivitiesAsync();
        public Task<AddExtracurricularActivitiesDto> AddExtracurricularActivitiesAsync(AddExtracurricularActivitiesDto request);
    }
}
