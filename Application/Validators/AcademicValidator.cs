using Application.Interfaces.Repositories.AcademicsRepository;
using Application.Interfaces.Repositories.AcademicsRepository;
using Domain.Entities;
using FluentValidation;

public class AcademicValidator : AbstractValidator<Academics>
{
    private readonly IAcademicsRepository _academicRepository;

    public AcademicValidator(IAcademicsRepository academicRepository)
    {
        _academicRepository = academicRepository;

        // Validate Section
        RuleFor(x => x.Section)
            .NotEmpty().WithMessage("Section is required")
            .MaximumLength(50).WithMessage("Section must not exceed 50 characters");

        // Validate GPA
        RuleFor(x => x.GPA)
            .InclusiveBetween(0, 4).WithMessage("GPA must be between 0 and 4");

        // Validate Joined date
        RuleFor(x => x.Joined)
            .NotEmpty().WithMessage("Joined date is required")
            .LessThan(x => x.Ended).WithMessage("Joined date must be before Ended date");

        // Validate Ended date
        RuleFor(x => x.Ended)
            .NotEmpty().WithMessage("Ended date is required")
            .GreaterThan(x => x.Joined).WithMessage("Ended date must be after Joined date");

        // Validate Semester
        RuleFor(x => x.Semester)
            .NotEmpty().WithMessage("Semester is required")
            .MaximumLength(20).WithMessage("Semester must not exceed 20 characters");

        // Validate Scholarship
        RuleFor(x => x.Scholarship)
            .GreaterThanOrEqualTo(0).WithMessage("Scholarship must be a positive value");

        // Example: Unique check for Section (optional)
        RuleFor(x => x.Section)
            .MustAsync(async (model, section, cancellation) => await IsSectionUniqueAsync(model.Id, section))
            .WithMessage("Section already exists");
    }

    private async Task<bool> IsSectionUniqueAsync(int id, string section)
    {
        return !await _academicRepository.AnyAsync(a => a.Id != id && a.Section == section);
    }
}
