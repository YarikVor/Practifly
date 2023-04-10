using System.Collections;

namespace PractiFly.Tests.EntityFromDb;

public interface IFakerGenerate
{
    IEnumerable Generate(int count, string ruleSets = null);
}

public interface IFakerGenerate<T> : IFakerGenerate
{
    IEnumerable IFakerGenerate.Generate(int count, string ruleSets = null)
    {
        return Generate(count, ruleSets);
    }

    List<T> Generate(int count, string ruleSets = null);
}