using Xunit.Abstractions;

namespace CalculatorLibrary.Test;

public class CalculatorTestsBySetup : IDisposable, IAsyncLifetime
{
    private readonly Calculator _sut = new();
    private readonly ITestOutputHelper _outputHelper;

    public CalculatorTestsBySetup(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _outputHelper.WriteLine("Constructor called");
    }

    [Fact]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        // Act
        int result = _sut.Add(5, 4);

        _outputHelper.WriteLine("Add method called");

        // Assert
        Assert.Equal(9, result);
    }
    
    [Fact]
    public void Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        // Act
        int result = _sut.Subtract(10, 7);

        _outputHelper.WriteLine("Subtract method called");

        // Assert
        Assert.Equal(3, result);
    }

    public void Dispose()
    {
        _outputHelper.WriteLine("Dispose called");
    }

    public async Task InitializeAsync()
    {
        _outputHelper.WriteLine("InitializeAsync called");

        await Task.Delay(1);
    }

    public async Task DisposeAsync()
    {
        _outputHelper.WriteLine("DisposeAsync called");

        await Task.Delay(1);
    }
}
