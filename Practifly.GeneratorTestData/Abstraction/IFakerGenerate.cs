using System.Collections;

namespace PractiFly.Tests.EntityFromDb;

public interface IFakerGenerate
{
    IEnumerable Generate(int count, string ruleSets = null);
}

public interface IFakerGenerate<T> : IFakerGenerate
{
    List<T> Generate(int count, string ruleSets = null);

    IEnumerable IFakerGenerate.Generate(int count, string ruleSets = null) => Generate(count, ruleSets);
}