using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using practice.domain.Kernel.Domain;
using practice.domain.Events;

namespace practice.domain.Entities;

/// <summary>
/// first name
/// last name
/// email
/// phone
/// organization
/// unit
/// </summary>
public class User:BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; } = string.Empty;
    public string Organization { get; private set; } = string.Empty;
    public string Unit { get; private set; } = string.Empty;

    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;

    [JsonIgnore]
    public List<RefreshToken> RefreshTokens { get; set; }

    [NotMapped]
    public bool IsEmpty => _isEmpty;
    private bool _isEmpty;

    protected User()
    {
        
    }

    protected User(bool isEmpty)
    {
        _isEmpty = isEmpty;
    }

    internal User(string firstName, string lastName, string email)
        => (FirstName, LastName, Email,_isEmpty) = (firstName, lastName, email,false);

    public static User CreateNew(string firstName, string lastName, string email)
        => new User(firstName, lastName, email);

    public static User NoUser() => new User(true);

    public void Register(string firstName,string lastName,string email,string password)
    {
        if(IsEmpty)
            AddEvent(new RegisteredDomainEvent(firstName, lastName, email, password));
    }

    public void UpdateEmail(string email)
        =>Email = email;

    public void UpdatePhone(string phone)
        =>Phone = phone;

    public void UpdateOrganization(string organization)
        => Organization = organization;
    public void UpdateUnit(string unit)
        => Unit = unit;
}