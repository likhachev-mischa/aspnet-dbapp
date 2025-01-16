using dbapp.Models;

namespace dbapp.Console
{
	public class UserToRoleAdapter : IConsoleAdapter
	{
		public string TableName => "UserToRole";

		private readonly TablesDbStorage m_storage;

		public UserToRoleAdapter(TablesDbStorage storage)
		{
			m_storage = storage;
		}

		public async Task Display()
		{
			var list = await m_storage.GetUserToRolesAsync();
			if (list == null || list.Count == 0)
			{
				System.Console.WriteLine("No entities found");
				return;
			}

			System.Console.WriteLine("{0,-5} {1,-10} {2,-15}", "Id", "UserId", "RoleId");
			foreach (var ent in list)
			{
				System.Console.WriteLine("{0,-5} {1,-10} {2,-15}", ent.UserToRoleId, ent.UserId, ent.RoleId);
			}
		}

		public async Task Create()
		{
			await m_storage.AddUserToRoleAsync(await GetEntity());
		}

		public async Task Delete(int id)
		{
			await m_storage.DeleteRoleToPrivilegeAsync(id);
		}

		public async Task Update(int id)
		{
			var ent = await m_storage.GetUserToRoleAsync(id);
			if (ent == null)
			{
				System.Console.WriteLine("Entity not found");
				return;
			}

			System.Console.WriteLine("Enter new data:");
			await m_storage.UpdateUserToRoleAsync(await GetEntity(ent));
		}

		private async Task<UserToRole> GetEntity(UserToRole? prevEntity = null)
		{
			System.Console.WriteLine("Enter UserId");
			int userId = int.Parse(System.Console.ReadLine());
			System.Console.WriteLine("Enter RoleId");
			int roleId = int.Parse(System.Console.ReadLine());

			var role = await m_storage.GetRoleAsync(roleId);
			var user = await m_storage.GetUserAsync(userId);

			while (role == null || user == null)
			{
				System.Console.WriteLine("Foreign entities not found!");
				System.Console.WriteLine("Enter UserId");
				roleId = int.Parse(System.Console.ReadLine());
				System.Console.WriteLine("Enter RoleId");
				userId = int.Parse(System.Console.ReadLine());
				role = await m_storage.GetRoleAsync(roleId);
				user = await m_storage.GetUserAsync(userId);
			}

			if (prevEntity == null)
			{
				return new UserToRole()
				{
					UserId = userId,
					RoleId = roleId,
				};
			}

			prevEntity.UserId = userId;
			prevEntity.RoleId = roleId;
			return prevEntity;
		}
	}
}