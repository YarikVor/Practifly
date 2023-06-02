using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.WebApi.AutoMapper.Profiles;

namespace PractiFly.WebApi.AutoMappers;

public class PractiFlyMapperConfigurationExpression : MapperConfigurationExpression
{
    public PractiFlyMapperConfigurationExpression(IPractiflyContext _context)
    {
        var profiles = new Profile[]
        {
            new CourseDetailsProfile(_context),
            new CourseThemesProfile(_context),
            new CourseMaterialsProfile(_context),
            new HeadingProfile(),
            new MyCourseProfile(_context),
            new UserProfile(),
            new MaterialBlockProfile(_context),
            new HeadingCourseProfile(_context),
            new AdminProfile(_context),
            new CourseDataProfile(_context)
        };

        AddProfiles(profiles);
    }
}