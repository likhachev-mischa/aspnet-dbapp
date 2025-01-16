namespace dbapp.Console
{
	public interface IConsoleAdapter
	{
		string TableName { get; }
		Task Display();
		Task Create();
		Task Delete(int id); 
		Task Update(int id);
	}
}
