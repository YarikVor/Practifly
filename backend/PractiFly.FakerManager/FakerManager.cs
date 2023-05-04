using System.Collections;

namespace PractiFly.FakerManager;

public class FakerManager : IFakerManager
{
    private readonly Dictionary<Type, IFakerGenerate> _fakers = new();

    public IEnumerable<T> Generate<T>(int count) where T : class
    {
        return (Generate(typeof(T), count) as IEnumerable<T>)!;
    }

    public IEnumerable Generate(Type type, int count)
    {
        var faker = _fakers[type];

        return faker.Generate(count);
    }

    public void AddFaker<T>(IFakerGenerate<T> faker) where T : class
    {
        if (faker == null) throw new ArgumentNullException(nameof(faker));

        _fakers.Add(typeof(T), faker);
    }
}