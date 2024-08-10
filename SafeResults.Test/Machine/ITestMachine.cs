namespace SafeResults.Test.Machine;

public interface ITestMachine
{
    IResult<ITestMachine> TestClassSuccess(ITestMachine machine);
    IResult<int> TestIntFail(int input);
    IResult<int> TestIntSuccess(int input);
    IResult<string> TestStringFail(string input);
    IResult<string> TestStringSuccess(string input);
}