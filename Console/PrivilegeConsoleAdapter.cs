using dbapp.Models;

namespace dbapp.Console
{
	public class PrivilegeConsoleAdapter : IConsoleAdapter
	{
		public string TableName => "Privilege";
		private TablesDbStorage m_storage;

		public PrivilegeConsoleAdapter(TablesDbStorage storage)
		{
			m_storage = storage;
		}

		public async Task Display()
		{
			var list = await m_storage.GetPrivilegesAsync();
			if (list == null || list.Count == 0)
			{
				System.Console.WriteLine("No entities found");
				return;
			}

			System.Console.WriteLine("{0,-5} {1,-10}", "Id", "Name");
			foreach (var ent in list)
			{
				System.Console.WriteLine("{0,-5} {1,-10}", ent.PrivilegeId, ent.PrivilegeName);
			}
		}

		public async Task Create()
		{
			await m_storage.AddPrivilegeAsync(GetPrivilige());
		}

		public async Task Delete(int id)
		{
			await m_storage.DeletePrivilegeAsync(id);
		}

		public async Task Update(int id)
		{
			Privilege? priv = await m_storage.GetPrivilegeAsync(id);
			if (priv == null)
			{
				System.Console.WriteLine("Entity not found!");
				return;
			}

			System.Console.WriteLine("Enter new data:");
			await m_storage.UpdatePrivilegeAsync(GetPrivilige(priv));
		}

		private Privilege GetPrivilige(Privilege? prevPriv = null)
		{
			System.Console.WriteLine("Enter Privilige Name");
			string? name = System.Console.ReadLine();
			if (prevPriv == null)
			{
				Privilege priv = new Privilege() { PrivilegeName = name };
				return priv;
			}

			prevPriv.PrivilegeName = name;
			return prevPriv;
		}
	}
}