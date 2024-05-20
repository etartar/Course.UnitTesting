using System;

namespace UnderstandingDependencies.Api.Models;

public class AppUser
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int Age { get; set; }
    public DateOnly DateOfBirthDate { get; set; }
}
