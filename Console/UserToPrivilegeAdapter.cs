using dbapp.Models;

namespace dbapp.Console
{
	public class UserToPrivilegeAdapter : IConsoleAdapter
	{
		public string TableName => "UserToPrivilege";

		private readonly TablesDbStorage m_storage;

		public UserToPrivilegeAdapter(TablesDbStorage storage)
		{
			m_storage = storage;
		}

		public async Task Display()
		{
			var list = await m_storage.GetUserToPrivilegesAsync();
			if (list == null || list.Count == 0)
			{
				System.Console.WriteLine("No entities found");
				return;
			}

			System.Console.WriteLine("{0,-5} {1,-10} {2,-15}", "Id", "UserId", "PrivilegeId");
			foreach (var ent in list)
			{
				System.Console.WriteLine("{0,-5} {1,-10} {2,-15}", ent.UserToPrivilegeId, ent.UserId, ent.PrivilegeId);
			}
		}

		public async Task Create()
		{
			await m_storage.AddUserToPrivilegeAsync(await GetEntity());
		}

		public async Task Delete(int id)
		{
			await m_storage.DeleteUserToPrivilegeAsync(id);
		}

		public async Task Update(int id)
		{
			var ent = await m_storage.GetUserToPrivilegeAsync(id);
			if (ent == null)
			{
				System.Console.WriteLine("Entity not found");
				return;
			}

			System.Console.WriteLine("Enter new data:");
			await m_storage.UpdateUserToPrivilegeAsync(await GetEntity(ent));
		}

		private async Task<UserToPrivilege> GetEntity(UserToPrivilege? prevEntity = null)
		{
			System.Console.WriteLine("Enter UserId");
			int privilegeId = int.Parse(System.Console.ReadLine());
			System.Console.WriteLine("Enter PrivilegeId");
			int userId = int.Parse(System.Console.ReadLine());

			var privilege = await m_storage.GetPrivilegeAsync(privilegeId);
			var user = await m_storage.GetUserAsync(userId);

			while (privilege == null || user == null)
			{
				System.Console.WriteLine("Foreign entities not found!");
				System.Console.WriteLine("Enter UserId");
				privilegeId = int.Parse(System.Console.ReadLine());
				System.Console.WriteLine("Enter RoleId");
				userId = int.Parse(System.Console.ReadLine());
				privilege = await m_storage.GetPrivilegeAsync(privilegeId);
				user = await m_storage.GetUserAsync(userId);
			}


			if (prevEntity == null)
			{
				return new UserToPrivilege()
				{
					UserId = userId,
					PrivilegeId = privilegeId,
				};
			}

			prevEntity.UserId = userId;
			prevEntity.PrivilegeId = privilegeId;
			return prevEntity;
		}
	}
}