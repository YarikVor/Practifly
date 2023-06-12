using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.WebApi.AutoMapper.Profiles;

namespace PractiFly.WebApi.AutoMappers;

public class PractiFlyMapperConfigurationExpression : MapperConfigurationExpression
{
    public PractiFlyMapperConfigurationExpression(IPractiflyContext context)
    {
        var profiles = new Profile[]
        {
            new CourseDetailsProfile(context),
            new CourseThemesProfile(context),
            new CourseMaterialsProfile(context),
            new HeadingProfile(),
            new MyCourseProfile(context),
            new UserProfile(),
            new MaterialBlockProfile(),
            new HeadingCourseProfile(context),
            new AdminProfile(context),
            new CourseDataProfile(context)
        };

        AddProfiles(profiles);
    }
}