using System.ComponentModel.DataAnnotations;

namespace dbapp.Models
{
	public class User
	{
		public int UserId { get; set; }
		public string? UserName { get; set; }
		public string? PassHash { get; set; }
		public string? PassSalt { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateOnly RegistrationDate { get; set; }
	}
}