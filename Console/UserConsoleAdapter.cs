using dbapp.Models;

namespace dbapp.Console
{
	public class UserConsoleAdapter : IConsoleAdapter
	{
		public string TableName => "Users";
		private readonly TablesDbStorage m_storage;

		public UserConsoleAdapter(TablesDbStorage storage)
		{
			m_storage = storage;
		}

		public async Task Display()
		{
			var list = await m_storage.GetUsersAsync();
			if (list == null)
			{
				System.Console.WriteLine("No entites found");
				return;
			}

			System.Console.WriteLine("{0,-5} {1,-10} {2,-20} {3,-20} {4,-10}", "Id", "Name", "PassHash", "PassSalt",
				"RegDate");
			foreach (var user in list)
			{
				System.Console.WriteLine("{0,-5} {1,-10} {2,-20} {3,-20} {4,-10}", user.UserId, user.UserName,
					user.PassHash, user.PassSalt, user.RegistrationDate);
			}
		}

		public async Task Create()
		{
			await m_storage.AddUserAsync(GetUser());
		}

		public async Task Delete(int id)
		{
			await m_storage.DeleteUserAsync(id);
		}

		public async Task Update(int id)
		{
			User? user = await m_storage.GetUserAsync(id);
			if (user == null)
			{
				System.Console.WriteLine("Entity not found!");
				return;
			}

			System.Console.WriteLine("Enter new data:");
			await m_storage.UpdateUserAsync(GetUser(user));
		}

		private User GetUser(User? prevUser=null)
		{
			System.Console.WriteLine("Enter UserName");
			string? name = System.Console.ReadLine();
			System.Console.WriteLine("Enter PassHash");
			string? passHash = System.Console.ReadLine();
			System.Console.WriteLine("Enter PassSalt");
			string? passSalt = System.Console.ReadLine();
			System.Console.WriteLine("Enter RegDate");
			string? dateStr = System.Console.ReadLine();
			DateOnly date;
			while (!DateOnly.TryParse(dateStr, out date))
			{
				System.Console.WriteLine("Incorrect date!");
				System.Console.WriteLine("Enter RegDate");
				dateStr = System.Console.ReadLine();
			}

			if (prevUser == null)
			{
				User user = new User()
					{ UserName = name, PassHash = passHash, PassSalt = passSalt, RegistrationDate = date };
				return user;
			}

			prevUser.UserName = name;
			prevUser.PassHash = passHash;
			prevUser.PassSalt = passSalt;	
			prevUser.RegistrationDate = date;
			return prevUser;
		}
	}
}