using LINQ_to_objects;

internal class Program
{
	private static void Main(string[] args)
	{
		while(true)
		{
			Select();
		}
	}

	private static void ShowMenu()
	{
		System.Console.WriteLine("""
			1 - Linq to Objects
			2 - Linq to XML
			
			""");
	}

	private static void Select()
	{
		LinqToXML xml = new();
		LinqToObjects objects = new();
		JsonSerialization json = new();

		ShowMenu();

		System.Console.Write("Action: ");
		var input = System.Console.ReadLine();

		switch (input)
		{
			case "1":
				objects.Main();
				break;
			case "2":
				xml.Main();
				break;
			case "3":
				json.Main();
				break;
			default:
				break;
		}
	}

}