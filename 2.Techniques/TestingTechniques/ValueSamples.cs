namespace TestingTechniques;

public class ValueSamples
{
    public string FullName = "Emir TARTAR";

    public int Age = 33;

    public DateOnly DateOfBirth = new(1993, 04, 29);

    public User AppUser = new()
    {
        FullName = "Emir TARTAR",
        Age = 33,
        DateOfBirth = new(1993, 04, 29)
    };

    public IEnumerable<User> Users = new[]
    {
        new User()
        {
            FullName = "Emir TARTAR",
            Age = 31,
            DateOfBirth = new(1993, 04, 29)
        },
        new User()
        {
            FullName = "Emirov TARTAROV",
            Age = 33,
            DateOfBirth = new(1991, 04, 29)
        },
        new User()
        {
            FullName = "Emirov TARTAROVIC",
            Age = 35,
            DateOfBirth = new(1989, 04, 29)
        }
    };

    public IEnumerable<int> Numbers = new[]
    {
        5, 10, 25, 50
    };

    public float Divide(int a, int b)
    {
        EnsureThatDivisorIsNotZero(a);
        EnsureThatDivisorIsNotZero(b);

        return a / b;
    }

    private void EnsureThatDivisorIsNotZero(int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException();
        }
    }

    public event EventHandler ExampleEvent;

    public virtual void RaiseExampleEvent()
    {
        ExampleEvent(this, EventArgs.Empty);
    }

    internal int InternalSecretNumber = 42;
}
