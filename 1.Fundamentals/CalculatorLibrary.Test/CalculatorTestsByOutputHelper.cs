using Xunit.Abstractions;

namespace CalculatorLibrary.Test;

public class CalculatorTestsByOutputHelper
{
    private readonly Calculator _sut = new();
    private readonly Guid _guid = Guid.NewGuid();
    private readonly ITestOutputHelper _outputHelper;

    public CalculatorTestsByOutputHelper(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        // Act
        int result = _sut.Add(5, 4);

        // Assert
        Assert.Equal(9, result);
    }

    [Fact]
    public void TestGuid1()
    {
        _outputHelper.WriteLine(_guid.ToString());
    }

    [Fact]
    public void TestGuid2()
    {
        _outputHelper.WriteLine(_guid.ToString());
    }
}
