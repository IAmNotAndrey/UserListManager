using System.ComponentModel.DataAnnotations;

namespace UserListManager.Models;

public class User
{
	[Key]
	public int Id { get; set; }

	public string FirstName { get; set; } = null!;

	public string LastName { get; set; } = null!;

	[Range(0, 150)]
	public int Age { get; set; }

	[EmailAddress]
	public string Email { get; set; } = null!;
}
