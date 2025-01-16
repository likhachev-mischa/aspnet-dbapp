using dbapp.Models;

namespace dbapp.Console
{
	public class ConsoleEntryPoint
	{
		private IConsoleAdapter m_selectedTable;

		private List<IConsoleAdapter> m_consoleAdapters = new();

		public ConsoleEntryPoint(TablesDbStorage storage)
		{
			m_consoleAdapters.Add(new UserConsoleAdapter(storage));
			m_consoleAdapters.Add(new RoleConsoleAdapter(storage));
			m_consoleAdapters.Add(new PrivilegeConsoleAdapter(storage));
			m_consoleAdapters.Add(new UserToRoleAdapter(storage));
			m_consoleAdapters.Add(new RoleToPrivilegeAdapter(storage));
			m_consoleAdapters.Add(new UserToPrivilegeAdapter(storage));
		}

		public async Task Start()
		{
			System.Console.WriteLine("System start");
			int code = -1;
			while (!ExitOnCode(code))
			{
				code = SelectTable();
				System.Console.Clear();
				if (ExitOnCode(code))
					return;

				m_selectedTable = m_consoleAdapters[code - 1];
				int action = -1;
				while (!ExitOnCode(action))
				{
					action = SelectAction();
					System.Console.Clear();

					await ProcessAction(action);
				}
			}
		}

		private int SelectTable()
		{
			System.Console.WriteLine("Avaliable tables:");

			for (var i = 0; i < m_consoleAdapters.Count; i++)
			{
				System.Console.WriteLine($"{i + 1}. {m_consoleAdapters[i].TableName}");
			}

			System.Console.WriteLine("Enter table id:");

			int tableId = int.Parse(System.Console.ReadLine());

			while (tableId < 0 || tableId > m_consoleAdapters.Count)
			{
				System.Console.WriteLine("Incorrect table id");
				System.Console.WriteLine("Enter table id:");
				tableId = int.Parse(System.Console.ReadLine());
			}

			return tableId;
		}

		private int SelectAction()
		{
			System.Console.WriteLine("1.get entities\n2.create entity\n3.delete entity\n4.update entity ");
			System.Console.WriteLine("Choose action:");
			int input = int.Parse(System.Console.ReadLine());
			if (input < 0 || input > 4)
			{
				System.Console.WriteLine("Incorrect input");
				System.Console.WriteLine("Choose action:");
				input = int.Parse(System.Console.ReadLine());
			}

			return input;
		}

		private async Task ProcessAction(int action)
		{
			switch (action)
			{
				case 0:
					return;
				case 1:
					await m_selectedTable.Display();
					break;
				case 2:
					await m_selectedTable.Create();
					break;
				case 3:
				{
					System.Console.WriteLine("Enter entity id:");
					int id = int.Parse(System.Console.ReadLine());
					await m_selectedTable.Delete(id);
					break;
				}
				case 4:
				{
					System.Console.WriteLine("Enter entity id:");
					int id = int.Parse(System.Console.ReadLine());
					await m_selectedTable.Update(id);
					break;
				}
				default:
					System.Console.WriteLine("Incorrect input");
					break;
			}
		}

		private bool ExitOnCode(int code)
		{
			if (code == 0)
			{
				System.Console.WriteLine("Terminating...");
				return true;
			}

			return false;
		}
	}
}