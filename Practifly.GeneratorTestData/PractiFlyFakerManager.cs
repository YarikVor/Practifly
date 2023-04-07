using Practifly.GeneratorTestData.Faker.Users;
using PractiFly.Tests.EntityFromDb;

namespace Practifly.GeneratorTestData;

public class PractiFlyFakerManager : FakerManager
{
    public PractiFlyFakerManager()
    {
        // Users
        AddFaker(new GroupCourseFaker());
        AddFaker(new GroupFaker());
        AddFaker(new GroupCourseFaker());
        AddFaker(new UserFaker());
        AddFaker(new UserGroupFaker());
        AddFaker(new UserHeadingFaker());
        AddFaker(new UserMaterialFaker());

        // Materials


        // Courses
        AddFaker(new CourseCompotencyFaker());
        AddFaker(new CourseDependencyFaker());
        AddFaker(new CourseDependencyTypeFaker());
        AddFaker(new CourseFaker());
        AddFaker(new CourseHeadingFaker());
        AddFaker(new CourseMaterialFaker());
        AddFaker(new ThemeFaker());
        AddFaker(new ThemeMaterialFaker());
        AddFaker(new UnitFaker());
    }
}