using FluentAssertions;

namespace TestingTechniques.Tests.Unit;

public class ValueSamplesTests
{
    private readonly ValueSamples _sut = new();

    [Fact]
    public void StringAssertionExample()
    {
        var fullName = _sut.FullName;

        fullName.Should().NotBeEmpty();
        fullName.Should().Be("Emir TARTAR");
        fullName.Should().StartWith("Emir");
        fullName.Should().EndWith("TARTAR");
    }

    [Fact]
    public void NumberAssertionExample()
    {
        var age = _sut.Age;

        age.Should().Be(33);
        age.Should().BePositive();
        age.Should().BeGreaterThan(20);
        age.Should().BeLessThanOrEqualTo(33);
        age.Should().BeInRange(20, 50);
    }

    [Fact]
    public void DateOnlyAssertionExample()
    {
        var dateOfBirth = _sut.DateOfBirth;

        dateOfBirth.Should().Be(new(1993, 04, 29));
        dateOfBirth.Should().BeAfter(new(1990, 01, 01));
        dateOfBirth.Should().BeBefore(new(1995, 01, 01));
    }

    [Fact]
    public void ObjectAssertionExample()
    {
        var expected = new User()
        {
            FullName = "Emir TARTAR",
            Age = 33,
            DateOfBirth = new(1993, 04, 29)
        };

        var user = _sut.AppUser;

        user.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void EnumerableObjectAssertionExample()
    {
        var expected = new User()
        {
            FullName = "Emir TARTAR",
            Age = 31,
            DateOfBirth = new(1993, 04, 29)
        };

        var user = _sut.Users.As<User[]>();

        user.Should().ContainEquivalentOf(expected);
        user.Should().HaveCount(3);
        user.Should().Contain(x => x.FullName.StartsWith("Emirov") && x.Age > 31);
    }

    [Fact]
    public void EnumerableNumberAssertionExample()
    {
        var numbers = _sut.Numbers.As<int[]>();

        numbers.Should().Contain(5);
    }

    [Fact]
    public void ExceptionThrownAssertionExample()
    {
        Action result = () => _sut.Divide(1, 0);

        result.Should().Throw<DivideByZeroException>().WithMessage("Attempted to divide by zero.");
    }

    [Fact]
    public void EventRaisedAssertionExample()
    {
        var monitorSubject = _sut.Monitor();

        _sut.RaiseExampleEvent();

        monitorSubject.Should().Raise("ExampleEvent");
    }

    [Fact]
    public void TestingInternalMembersExample()
    {
        var number = _sut.InternalSecretNumber;

        number.Should().Be(42);
    }
}
