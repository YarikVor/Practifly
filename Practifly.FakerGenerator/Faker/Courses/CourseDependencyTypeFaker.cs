using Bogus;
using PractiFly.DbEntities.Courses;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator.Faker.Courses;

public sealed class CourseDependencyTypeFaker : Faker<CourseDependencyType>, IFakerGenerate<CourseDependencyType>
{
    public CourseDependencyTypeFaker() : base("uk")
    {
        RuleFor(cdt => cdt.Id, f => f.IndexFaker + 1);
        RuleFor(cdt => cdt.Name, f => f.Lorem.Word());
        RuleFor(cdt => cdt.Description, f => f.Lorem.Sentence());
    }
}