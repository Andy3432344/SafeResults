using static SafeResults.ResultExtensions;

namespace SafeResults.Test.Machine;

public class TestMachine : ITestMachine
{

    public IResult<ITestMachine> TestClassSuccess(ITestMachine machine)
    {
        return ok(this as ITestMachine);
    }


    public IResult<ITestMachine> TestClassFail(ITestMachine machine)
    {
        return error<ITestMachine>(nameof(TestClassFail));
    }


    public IResult<string> TestStringSuccess(string input)
    {
        return ok(input);
    }

    public IResult<string> TestStringFail(string input)
    {
        return error<string>(input);
    }


    public IResult<int> TestIntSuccess(int input)
    {
        return ok(input);
    }

    public IResult<int> TestIntFail(int input)
    {
        return error<int>("ERROR MESSAGE");
    }
}

