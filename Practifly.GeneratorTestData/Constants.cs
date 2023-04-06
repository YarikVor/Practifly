using Bogus;

namespace Practifly.GeneratorTestData;

public static class Constants
{
    public const int MIN_ID = 1;
    public const int MAX_ID = 5;
    
    public const int COUNT_GENERATED_USERS = 5;


    public static int RandomId(this Faker faker) => faker.Random.Int(MIN_ID, MAX_ID);
}