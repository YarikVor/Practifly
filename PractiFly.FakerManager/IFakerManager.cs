using System.Collections;

namespace PractiFly.FakerManager;

public interface IFakerManager
{
    IEnumerable<T> Generate<T>(int count) where T : class;
    IEnumerable Generate(Type type, int count);
}