using Practifly.GeneratorTestData.Faker.Users;

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
        
        
    }
}