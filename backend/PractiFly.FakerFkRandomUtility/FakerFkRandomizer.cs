using Bogus;

namespace PractiFly.FakerManager;

public class FakerFkRandomizer<TEntity> : Faker<TEntity> where TEntity : class
{
    private readonly int _count;

    public FakerFkRandomizer(int count, string lang = "uk") : base(lang)
    {
        _count = count;
    }

    protected int RandomId(Faker f)
    {
        return f.Random.Int(1, _count);
    }
}