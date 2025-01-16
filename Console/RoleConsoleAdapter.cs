using dbapp.Models;

namespace dbapp.Console
{
	public class RoleConsoleAdapter : IConsoleAdapter
	{
		public string TableName => "Roles";

		private readonly TablesDbStorage m_storage;

		public RoleConsoleAdapter(TablesDbStorage storage)
		{
			m_storage = storage;
		}

		public async Task Display()
		{
			var list = await m_storage.GetRolesAsync();
			if (list == null || list.Count == 0)
			{
				System.Console.WriteLine("No entities found");
				return;
			}

			System.Console.WriteLine("{0,-5} {1,-10}", "Id", "Name");
			foreach (var role in list)
			{
				System.Console.WriteLine("{0,-5} {1,-10}", role.RoleId, role.RoleName);
			}
		}

		public async Task Create()
		{
			await m_storage.AddRoleAsync(GetRole());
		}

		public async Task Delete(int id)
		{
			await m_storage.DeleteRoleAsync(id);
		}

		public async Task Update(int id)
		{
			Role? role = await m_storage.GetRoleAsync(id);
			if (role == null)
			{
				System.Console.WriteLine("Entity not found!");
				return;
			}

			System.Console.WriteLine("Enter new data:");
			await m_storage.UpdateRoleAsync(GetRole(role));
		}

		private Role GetRole(Role? prevRole = null)
		{
			System.Console.WriteLine("Enter RoleName");
			string? name = System.Console.ReadLine();
			if (prevRole == null)
			{
				Role role = new Role() { RoleName = name };
				return role;
			}

			prevRole.RoleName = name;
			return prevRole;
		}
	}
}