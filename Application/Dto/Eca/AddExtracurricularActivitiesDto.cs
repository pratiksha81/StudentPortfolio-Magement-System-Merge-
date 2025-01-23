using Microsoft.AspNetCore.Http;

namespace Application.Dto.ECA
{
    public class AddExtracurricularActivitiesDto
    {
        public string Position { get; set; }
        public string Skill { get; set; }
        public int Year { get; set; }
        public string ClubName { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
