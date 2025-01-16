using dbapp.Models;

namespace dbapp.Console
{
	public class RoleToPrivilegeAdapter : IConsoleAdapter
	{
		public string TableName => "RoleToPrivilege";

		private readonly TablesDbStorage m_storage;

		public RoleToPrivilegeAdapter(TablesDbStorage storage)
		{
			m_storage = storage;
		}

		public async Task Display()
		{
			var list = await m_storage.GetRoleToPrivilegesAsync();
			if (list == null || list.Count == 0)
			{
				System.Console.WriteLine("No entities found");
				return;
			}

			System.Console.WriteLine("{0,-5} {1,-10} {2,-15}", "Id", "RoleId", "PrivilegeId");
			foreach (var ent in list)
			{
				System.Console.WriteLine("{0,-5} {1,-10} {2,-15}", ent.RoleToPrivilegeId, ent.RoleId, ent.PrivilegeId);
			}
		}

		public async Task Create()
		{
			await m_storage.AddRoleToPrivilegeAsync(await GetEntity());
		}

		public async Task Delete(int id)
		{
			await m_storage.DeleteRoleToPrivilegeAsync(id);
		}

		public async Task Update(int id)
		{
			var ent = await m_storage.GetRoleToPrivilegeAsync(id);
			if (ent == null)
			{
				System.Console.WriteLine("Entity not found");
				return;
			}

			System.Console.WriteLine("Enter new data:");
			await m_storage.UpdateRoleToPrivilegeAsync(await GetEntity(ent));
		}

		private async Task<RoleToPrivilege> GetEntity(RoleToPrivilege? prevEntity = null)
		{
			System.Console.WriteLine("Enter RoleId");
			int roleId = int.Parse(System.Console.ReadLine());
			System.Console.WriteLine("Enter PrivilegeId");
			int privilegeId = int.Parse(System.Console.ReadLine());

			var privilege = await m_storage.GetPrivilegeAsync(privilegeId);
			var role = await m_storage.GetRoleAsync(roleId);

			while (privilege == null || role == null)
			{
				System.Console.WriteLine("Foreign entities not found!");
				System.Console.WriteLine("Enter UserId");
				privilegeId = int.Parse(System.Console.ReadLine());
				System.Console.WriteLine("Enter RoleId");
				roleId = int.Parse(System.Console.ReadLine());
				privilege = await m_storage.GetPrivilegeAsync(privilegeId);
				role = await m_storage.GetRoleAsync(roleId);
			}

			if (prevEntity == null)
			{
				return new RoleToPrivilege()
				{
					RoleId = roleId,
					PrivilegeId = privilegeId
				};
			}

			prevEntity.RoleId = roleId;
			prevEntity.PrivilegeId = privilegeId;
			return prevEntity;
		}
	}
}