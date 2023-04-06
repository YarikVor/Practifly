using System.Linq.Expressions;
using Practifly.Checkers.Builder;
using Xunit.Abstractions;

namespace Practifly.Checkers.Tests;

public class CheckerTests
{
    static Checker _checker;

    private ITestOutputHelper _logger;
    
    public CheckerTests(ITestOutputHelper _logger)
    {
        this._logger = _logger;
        Init();
    }
    
    public static void Init()
    {
        var options = new CheckerOptionBuilder()
            .Init()
            .Build();
        
        _checker = new Checker(options);
    }
    
    [Fact]
    public void EmptyAllProperties_Throws()
    {
        var classForTests = new
        {
            StructProperty = 0,
            StringProperty = (string)null!,
            Value = (int?)null
        };

        try
        {
            _checker.Check(classForTests);
        }
        catch (CheckerPropertiesException e)
        {
            _logger.WriteLine(e.Message);
            Assert.Equal(2, e.Message.Count(c => c == ','));
            return;
        }
        
        Assert.Fail("Exception not thrown");
    }
    
    [Fact]
    public void EmptyValueProperties_Throws()
    {
        var classForTests = new
        {
            StructProperty = 0,
            StringProperty = "text"
        };
        
        Assert.Throws<CheckerPropertiesException>(() => _checker.Check(classForTests));
    }
    
    [Fact]
    public void EmptyRefProperties_Throws()
    {
        var classForTests = new
        {
            StructProperty = 32,
            StringProperty = (string)null!
        };
        
        Assert.Throws<CheckerPropertiesException>(() => _checker.Check(classForTests));
    }
    
    [Fact]
    public void SkipRefType_Skip()
    {
        var classForTests = new
        {
            StructProperty = 32,
            StringProperty = (string)null!
        };
        
        var options = new CheckerOptionBuilder()
            .Init()
            .SkipType<string>()
            .Build();
        
        var checker = new Checker(options);
        
        checker.Check(classForTests);
    }
    
    [Fact]
    public void SkipValueType_Skip()
    {
        var classForTests = new
        {
            StructProperty = 0,
            StringProperty = "text"
        };
        
        var options = new CheckerOptionBuilder()
            .Init()
            .SkipType<int>()
            .Build();
        
        var checker = new Checker(options);
        
        checker.Check(classForTests);
    }
    
    [Fact]
    public void SkipNullableType_Skip()
    {
        var classForTests = new
        {
            StructProperty = 1,
            Value = (int?)null
        };
        
        var options = new CheckerOptionBuilder()
            .Init()
            .SkipNullableTypes()
            .Build();
        
        var checker = new Checker(options);
        
        checker.Check(classForTests);
    }
    
    [Fact]
    public void SkipStartWith_Skip()
    {
        var classForTests = new
        {
            StructProperty = 0,
            Value = "23"
        };
        
        var options = new CheckerOptionBuilder()
            .Init()
            .SkipStartWith("Struct")
            .Build();
        
        var checker = new Checker(options);
        
        checker.Check(classForTests);
    }
    
    [Fact]
    public void SkipEndWith_Skip()
    {
        var classForTests = new
        {
            StructProperty = 0,
            Value = "23"
        };
        
        var options = new CheckerOptionBuilder()
            .Init()
            .SkipEndWith("Property")
            .Build();
        
        var checker = new Checker(options);
        
        checker.Check(classForTests);
    }
    
    [Fact]
    public void SkipSubstring_Skip()
    {
        var classForTests = new
        {
            StructProperty = 0,
            Value = "23"
        };
        
        var options = new CheckerOptionBuilder()
            .Init()
            .SkipSubstring("ctPro")
            .Build();
        
        var checker = new Checker(options);
        
        checker.Check(classForTests);
    }
    
    [Fact]
    public void SkipByExpression_Skip()
    {
        var classForTests = new
        {
            StructProperty = 0,
            Value = (string)null!
        };
        
        _checker.Check(classForTests, e => e.StructProperty, e => e.Value);
    }

}
