using Application.Dto.ECA;
using Application.Interfaces.Repositories.ExtracurricularActivitiesRepository;
using Application.Interfaces.Services.ExtracurricularActivitiesService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class ExtracurricularActivitiesService : IExtracurricularActivitiesService
{
    private readonly IExtracurricularActivitiesRepository _ecaRepository;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly ILogger<ExtracurricularActivitiesService> _logger;

    public ExtracurricularActivitiesService(
        IExtracurricularActivitiesRepository ecaRepository,
        IWebHostEnvironment hostingEnvironment, ILogger<ExtracurricularActivitiesService> logger)
    {
        _logger = logger;
        _ecaRepository = ecaRepository;
        _hostingEnvironment = hostingEnvironment;
    }

    public async Task<AddExtracurricularActivitiesDto> AddExtracurricularActivitiesAsync(AddExtracurricularActivitiesDto request)
    {
        var imageUrls = new List<string>();

        if (request.Images != null && request.Images.Any())
        {
            foreach (var image in request.Images)
            {
                // Validate the image size and format
                if (image.Length > 2 * 1024 * 1024)
                {
                    throw new Exception("Image size exceeds 2 MB.");
                }

                var extension = Path.GetExtension(image.FileName);
                if (!new[] { ".jpg", ".jpeg", ".png" }.Contains(extension.ToLower()))
                {
                    throw new Exception("Supported formats are .jpg, .jpeg, and .png.");
                }

                // Save the image
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid() + extension;
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                imageUrls.Add(Path.Combine("uploads", fileName));
            }
        }

        // Create the domain entity
        var extracurricularActivity = new ExtracurricularActivities
        {
            Position = request.Position,
            Skill = request.Skill,
            Year = request.Year,
            ClubName = request.ClubName,
            ImageUrl = string.Join(";", imageUrls)
        };

        // Save to the repository
        await _ecaRepository.AddAsync(extracurricularActivity);
        await _ecaRepository.SaveChangesAsync();

        // Return the DTO
        return new AddExtracurricularActivitiesDto
        {
            Position = extracurricularActivity.Position,
            Skill = extracurricularActivity.Skill,
            Year = extracurricularActivity.Year,
            ClubName = extracurricularActivity.ClubName,
            Images = request.Images
        };
    }

    public async Task<List<ExtracurricularActivitiesDto>> GetAllExtracurricularActivitiesAsync()
    {
        _logger.LogInformation($"{nameof(GetAllExtracurricularActivitiesAsync)} method triggered");

        var activitiesList = await _ecaRepository.Queryable.ToListAsync();
        var activitiesDtos = activitiesList.Select(a => new ExtracurricularActivitiesDto
        {
            Id = a.Id,
            Position = a.Position,
            Skill = a.Skill,
            Year = a.Year,
            ClubName = a.ClubName,
            ImageUrl = a.ImageUrl
        }).ToList();

        _logger.LogInformation($"{nameof(GetAllExtracurricularActivitiesAsync)} method returned activities: {JsonConvert.SerializeObject(activitiesDtos)}");
        return activitiesDtos;
    }
}
