
namespace Practifly.GeneratorTestData;

public static class Constants
{
    public const int MinId = 1;
    public const int MaxId = 5;

    public const int CountGeneratedUsers = 5;


    public static int RandomId(this Bogus.Faker faker) => faker.Random.Int(MinId, MaxId);
}