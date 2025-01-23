using Application.Dto.ECA;
using Application.Dto.HealthTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.ExtracurricularActivitiesService
{
    public interface IExtracurricularActivitiesService
    {
        public Task<List<ExtracurricularActivitiesDto>> GetAllExtracurricularActivitiesAsync();
        public Task<AddExtracurricularActivitiesDto> AddExtracurricularActivitiesAsync(AddExtracurricularActivitiesDto request);
    }
}
