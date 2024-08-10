using SafeResults.Test.Machine;
using static SafeResults.ResultExtensions;

namespace SafeResults.Test;

public class ResultUnitTests
{
    private static Lazy<NoTestMachine> noTestMachine = new();
    private TestMachine machine = new();
    protected const int BadInt = -1;
    protected readonly ITestMachine BadTestMachine = noTestMachine.Value;
    protected const string BadString = "Don't use this value";

    [Fact]
    public void TestStringResults()
    {
        var opt = option(BadString);
        string actualString = Guid.NewGuid().ToString();

        var testString = opt | machine.TestStringFail(actualString);
        Assert.Equal(BadString, testString);
        Assert.True(opt.None);

        testString = opt | machine.TestStringSuccess(actualString);
        Assert.Equal(actualString, testString);
        Assert.True(opt.Some);

    }

    [Fact]
    public void TestIntResults()
    {
        var opt = option(BadInt);

        int actualInt = Guid.NewGuid().GetHashCode();

        var testInt = opt | machine.TestIntSuccess(actualInt);
        Assert.Equal(actualInt, testInt);
        Assert.True(opt.Some);

        testInt = opt | machine.TestIntFail(BadInt);
        Assert.Equal(BadInt, testInt);
        Assert.True(opt.None);
    }

    [Fact]
    public void TestClassResults()
    {
        var opt = option(BadTestMachine);
        ITestMachine actual = new TestMachine();

        ITestMachine testMachine = opt | machine.TestClassSuccess(actual);
        Assert.IsAssignableFrom<TestMachine>(testMachine);
        Assert.True(opt.Some);

        testMachine = opt | machine.TestClassFail(actual);
        Assert.IsAssignableFrom<NoTestMachine>(testMachine);
        Assert.True(opt.None);
    }

}
