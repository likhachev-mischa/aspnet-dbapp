namespace dbapp.Models
{
	public class RoleToPrivilege
	{
		public int RoleToPrivilegeId { get; set; }
		public int RoleId { get; set; }

		public Role? Role { get; set; }
		public int PrivilegeId { get; set; }
		public Privilege? Privilege{ get; set; }
	}
}
