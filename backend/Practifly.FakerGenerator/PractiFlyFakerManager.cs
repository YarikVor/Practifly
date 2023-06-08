using System.Reflection;
using PractiFly.FakerManager;

namespace Practifly.FakerGenerator;

public class PractiFlyFakerManager : FakerManager
{
    public PractiFlyFakerManager(int count = 5)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        var typeOfIFakerGenerate = typeof(IFakerGenerate);
        var fakersTypes = currentAssembly
            .ExportedTypes
            .Where(t => t
                .GetInterfaces()
                .Contains(typeOfIFakerGenerate)
            );

        foreach (var fakerType in fakersTypes)
        {
            var faker = (IFakerGenerate)Instance();

            AddFaker(faker, fakerType);

            object Instance()
            {
                if (fakerType.GetConstructor(Type.EmptyTypes) != null) 
                    return Activator.CreateInstance(fakerType)!;

                if (fakerType.GetConstructor(new[] { typeof(int) }) != null)
                    return Activator.CreateInstance(fakerType, count)!;

                throw new Exception($"Constructor is not available for {fakerType.Name}");
            }
        }
    }
}