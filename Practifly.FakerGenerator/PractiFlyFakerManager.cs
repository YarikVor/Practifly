using Practifly.FakerGenerator.Faker.Courses;
using Practifly.FakerGenerator.Faker.Materials;
using Practifly.FakerGenerator.Faker.Users;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator;

public class PractiFlyFakerManager : FakerManager
{
   
    public PractiFlyFakerManager(int count = 5)
    {
        // Users
        AddFaker(new GroupCourseFaker(count));
        AddFaker(new GroupFaker(count));
        AddFaker(new UserCourseFaker(count));
        AddFaker(new UserFaker());
        AddFaker(new UserGroupFaker(count));
        AddFaker(new UserHeadingFaker(count));
        AddFaker(new UserMaterialFaker(count));
        AddFaker(new UserThemeFaker(count));

        // Materials
        AddFaker(new CompetencyFaker(count));
        AddFaker(new HeadingCompetencyFaker(count));
        AddFaker(new HeadingFaker());
        AddFaker(new HeadingMaterialFaker(count));
        AddFaker(new LanguageFaker());
        AddFaker(new LevelFaker());
        AddFaker(new MaterialFaker(count));
        AddFaker(new MaterialBlockFaker(count));
        AddFaker(new MaterialCompetencyFaker(count));
        AddFaker(new UnitFaker(count));

        // Courses
        AddFaker(new CourseCompotencyFaker(count));
        AddFaker(new CourseDependencyFaker(count));
        AddFaker(new CourseDependencyTypeFaker());
        AddFaker(new CourseFaker(count));
        AddFaker(new CourseHeadingFaker(count));
        AddFaker(new CourseMaterialFaker(count));
        AddFaker(new ThemeFaker(count));
        AddFaker(new ThemeMaterialFaker(count));
    }
}