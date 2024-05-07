
namespace LINQ_to_objects;

public class TestApp : BaseConsoleApp
{
    protected override string Info => string.Empty;

    public TestApp() : base()
	{
		Dictionary<string, Action> actions = new() 
		{
			{"prt", PrintTest}	
		};
		
		Dictionary<string,string> desc = new()
		{
			{"prt", "Print test"}
		};
		
		base.Init(actions,desc);
	}

	private void PrintTest()
	{
		System.Console.WriteLine("TEST");
	}
	
}
