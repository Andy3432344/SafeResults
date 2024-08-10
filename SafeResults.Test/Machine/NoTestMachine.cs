using static SafeResults.ResultExtensions;

namespace SafeResults.Test.Machine;

public class NoTestMachine : ITestMachine
{
    public IResult<ITestMachine> TestClassSuccess()
    {
        return error<ITestMachine>(nameof(NoTestMachine));
    }

    public IResult<ITestMachine> TestClassSuccess(ITestMachine machine)
    {
        return error<ITestMachine>(nameof(NoTestMachine));
    }

    public IResult<int> TestIntFail(int input)
    {
        return error<int>(nameof(NoTestMachine));
    }

    public IResult<int> TestIntSuccess(int input)
    {
        return error<int>(nameof(NoTestMachine));
    }

    public IResult<string> TestStringFail(string input)
    {
        return error<string>(nameof(NoTestMachine));
    }

    public IResult<string> TestStringSuccess(string input)
    {
        return error<string>(nameof(NoTestMachine));
    }
}