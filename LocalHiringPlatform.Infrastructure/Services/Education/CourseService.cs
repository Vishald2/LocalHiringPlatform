using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Domain.Models.Education;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(
        ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<List<CourseModel>> GetByEducationIdAsync(
        int educationId)
    {
        var courses =
            await _courseRepository.GetByEducationIdAsync(
                educationId);

        return courses
            .Select(x => new CourseModel
            {
                CourseId = x.CourseId,
                EducationId = x.EducationId,
                Name = x.Name,
                Code = x.Code
            })
            .ToList();
    }
}