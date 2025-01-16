namespace dbapp.Models
{
	public class UserToPrivilege
	{
		public int UserToPrivilegeId { get; set; }
		public int UserId { get; set; }
		public User? User { get; set; }
		public int PrivilegeId { get; set; }
		public Privilege? Privilege { get; set; }
	}
}