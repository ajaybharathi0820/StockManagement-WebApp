namespace Polisher.Domain.Entities;

using Common.Models;

public class Polisher : AuditableEntity
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string ContactNumber { get; private set; }

    public Polisher(Guid id, string firstName, string lastName, string contactNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        ContactNumber = contactNumber;
    }

    // For EF Core
    private Polisher() { }

    public void Update(string firstName, string lastName, string contactNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        ContactNumber = contactNumber;
    }
}
