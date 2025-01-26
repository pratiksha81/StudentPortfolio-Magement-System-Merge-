using Application.Dto.Student;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Dto.StudentPortfolio
{
    public class StudentPortfolioDto
    {
        public List<Domain.Entities.Certification> Certifications { get; set; }
        public Domain.Entities.Academics Academics { get; set; }
        public List<ExtracurricularActivities> ExtracurricularActivities { get; set; }
    }
}
