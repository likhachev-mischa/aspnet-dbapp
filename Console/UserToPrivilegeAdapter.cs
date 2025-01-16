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
			await m_storage.AddUserToPrivilegeAsync(GetEntity());
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
			await m_storage.UpdateUserToPrivilegeAsync(GetEntity(ent));
		}

		private UserToPrivilege GetEntity(UserToPrivilege? prevEntity = null)
		{
			System.Console.WriteLine("Enter UserId");
			int roleId = int.Parse(System.Console.ReadLine());
			System.Console.WriteLine("Enter PrivilegeId");
			int userId = int.Parse(System.Console.ReadLine());

			if (prevEntity == null)
			{
				return new UserToPrivilege()
				{
					UserId = userId,
					PrivilegeId = roleId,
				};
			}

			prevEntity.UserId = userId;
			prevEntity.PrivilegeId = roleId;
			return prevEntity;
		}
	}
}