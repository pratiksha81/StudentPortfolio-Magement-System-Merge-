using Application.Dto.AdminProfile;
using Application.Interfaces.Repositories.AdminProfileRepository;
using FluentValidation;

namespace Application.Validators
{
    public class AdminProfileValidators : AbstractValidator<AddAdminProfileDto>
    {
        private readonly IAdminProfileRepository _adminProfileRepository;

        public AdminProfileValidators(IAdminProfileRepository adminProfileRepository)
        {
            _adminProfileRepository = adminProfileRepository;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(2, 100).WithMessage("First name must be between 2 and 100 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(2, 100).WithMessage("Last name must be between 2 and 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync(async (email, cancellation) =>
                    !(await _adminProfileRepository.AnyAsync(x => x.Email == email))
                ).WithMessage("Email already exists.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^(\+?\d{1,2}\s?)?(\()?(\d{3})(?(2)\))[\s\-]?\d{3}[\s\-]?\d{4}$")
                .WithMessage("Invalid phone number format.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .Length(5, 255).WithMessage("Address must be between 5 and 255 characters.");

            RuleFor(x => x.Bio)
                .NotEmpty().WithMessage("Bio is required.")
                .Length(5, 500).WithMessage("Bio must be between 5 and 500 characters.");




        }
    }
}
