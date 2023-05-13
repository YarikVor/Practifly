using Xunit.Abstractions;

namespace TestProject;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;
    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }
    [Fact]
    public void Test1()
    {

    }
}