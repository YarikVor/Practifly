using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PractiFly.WebApi.EntityDb.Users;
/*
 *          Id // IdentityUserId
        	FirstName
        	LastName
        	Email
        	Phone
        	FilePhoto //?
        	RegistrationDate
        	Note
*/
[Table("Users")]
[PrimaryKey("Id")]
public class Users
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    [Column("FirstName")]
    [MaybeNull]
    public string FirstName { get; set; }
    [Column("LastName")]
    [MaybeNull]
    public string LastName { get; set; }
    [EmailAddress]
    [Column("Email")]
    [MaybeNull]
    public string Email { get; set; }
    [Phone]
    [Column("Phone")]
    [MaybeNull]
    public string Phone { get; set; }
    [Url]
    [Column("FilePhoto")]
    [MaybeNull]
    public string FilePhoto { get; set; }
    [Column("RegistrationDate")]
    [MaybeNull]
    public DateOnly RegistrationDate { get; set; }
    [Column("Note")]
    public string? Note { get; set; }
}