using System.Xml.Linq;

namespace LINQ_to_objects;

public class RecursiveQuery : BaseConsoleApp<XDocument?>
{
	private XDocument? _result;
	private XDocument _document;

	protected override string Info
	{
		get
		{
			if(_result is null || _result.Root is null)
				return "Current selected Null";
			
			return _result.Root.Name == "Root" ? $"Current selected {_result.Root.Elements().Count()} elements" : $"Current selected element {_result.Root.Name}";	
		}
	}

	public RecursiveQuery(XDocument document)
	{
		ArgumentNullException.ThrowIfNull(document);
		_document = document;

		string[] codes =[
			"s",
			"sr",
			"lr",
			"q",	
		];
		
		string[] descs = [
			"Show document",
			"Show result",
			"Load result",
			"Start query",
		];
		
		Action[] actions = [
			ShowDocument,
			ShowResult,
			LoadResult,
			StartQuery,	
		];

		InitN(codes,descs,actions);
	}

	private void ShowDocument()
	{
		LogInfo(_document.ToString());
	}

	private void ShowResult()
	{
		LogInfo(_result?.ToString() ?? "Null");
	}

	private void LoadResult()
	{
		if (_result is null)
		{
			LogError("Result is null");
			return;
		}

		_document = _result;
		_result = null;
	}

	private void StartQuery()
	{
		int t = -1;

		var res = Query(_document.Elements(), ref t) ?? [];

		if (res.Count() != 1)
		{
			_result = new(new XElement("Root", res));
		}
		else
		{
			_result = new(res.First());
		}
	}

	private IEnumerable<XElement>? Query(IEnumerable<XElement> elements, ref int selectedDepth, int recursionDepth = 0)
	{
		if (elements is null || elements.Any() == false)
		{
			LogError("No items received. Returning...");
			return null;
		}

		Console.ForegroundColor = ConsoleColor.Gray;
		System.Console.WriteLine($"\nRecursion depth = {recursionDepth}; Elements count: {elements.Count()}; Current element: {elements.First().Name}");
		Console.ForegroundColor = ConsoleColor.White;

		bool check = ReadVariant(["Select", "Check"]) == 1;

		IEnumerable<XElement> filteredElements = elements;

		if (check)
		{
			while (true)
			{
				var fName = ReadAnswer("Element with name");
				var fValue = ReadAnswer("Has value");

				filteredElements = from elem in filteredElements
								   from cElem in elem.Elements(fName)
								   where cElem.Value == fValue
								   select elem;

				System.Console.WriteLine();

				if (filteredElements.Any() == false)
				{
					LogError("No elements selected. Returning...");
					return null;
				}

				LogSucces($"Selected elements: {filteredElements.Count()}");

				if (ReadVariantHorizontaly(["No", "Yes"], "Continue?") == 0)
					break;
			}

			if (ReadVariantHorizontaly(["No", "Yes"], "Finish?") == 1)
			{
				if (selectedDepth == -1 && ReadVariantHorizontaly(["No", "Yes"], "Return selected elements?") == 1)
					selectedDepth = recursionDepth;

				return filteredElements;
			}
			System.Console.WriteLine();
		}

		var eName = ReadAnswer("Select element with name");

		if (selectedDepth == -1 && ReadVariantHorizontaly(["No", "Yes"], "Return selected elements?") == 1)
			selectedDepth = recursionDepth;

		var selected = from elem in filteredElements
					   from cElem in elem.Elements(eName)
					   select cElem;

		var returned = Query(selected, ref selectedDepth, recursionDepth + 1);

		if (returned is not null && selectedDepth < recursionDepth)
		{
			return from elem in returned
				   select elem.Parent;
		}

		return returned;
	}

	public override XDocument? GetResult()
	{
		return _result;
	}
}
