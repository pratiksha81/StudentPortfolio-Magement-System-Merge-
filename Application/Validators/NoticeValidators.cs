using Application.Dto.Notice;
using Application.Interfaces.Repositories.NoticeRepository;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Application.Validators
{
    public class NoticeValidators : AbstractValidator<AddNoticeDto>
    {
        private readonly INoticeRepository _noticeRepository;

        public NoticeValidators(INoticeRepository noticeRepository)
        {
            _noticeRepository = noticeRepository;

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Notice title is required.")
                .Length(2, 200).WithMessage("Notice title must be between 2 and 200 characters.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required.")
                .GreaterThan(DateTime.Now).WithMessage("Date must be in the future.");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Notice image is required.")
                .Must(image => ValidateImageExtension(image))
                .WithMessage("Notice image must be a .jpg, .jpeg, or .png file.");

            //    // Make sure to add this if your AddNoticeDto contains NoticeId
            //    RuleFor(x => x.NoticeId)
            //        .GreaterThan(0).WithMessage("Notice ID must be a positive number.");
            //}
        }

        // Custom method to validate image extensions
        private bool ValidateImageExtension(IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName).ToLower();
            return extension == ".jpg" || extension == ".jpeg" || extension == ".png";
        }
    }
}
