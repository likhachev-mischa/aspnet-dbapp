namespace dbapp.Models
{
	public class UserToRole
	{
		public int UserToRoleId { get; set; }
		public int UserId { get; set; }
		public User? User { get; set; }
		public int RoleId { get; set; }
		public Role? Role { get; set; }
	}
}