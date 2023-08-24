using Subsetter.Core;

namespace Subsetter.Core.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var class1 = new Class1();

        var result = class1.AddNumbers(1, 2);

        Assert.Equal(3, result);
    }
}