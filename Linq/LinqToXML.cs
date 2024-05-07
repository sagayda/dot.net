using System.Xml.Serialization;

namespace LINQ_to_objects;

public class LinqToXML
{
	public static readonly string BuildingsFile = "buildings.xml";
	public static readonly string TenantsFile = "tenants.xml";
	private SerializableList<Building> _buildings;
	private SerializableList<Tenant> _tenants;

	public LinqToXML()
	{
		var (buildings, tenants) = TestData.GetTestData(1);
		_buildings = [.. buildings];
		_tenants = [.. tenants];
		
		CreateXML();
	}

	public void Main()
	{
		XmlActions actions = new();
		actions.Start();
	}

	private void CreateXML()
	{
		XmlSerializer serializer = new(_buildings.GetType());
		using TextWriter writerB = File.CreateText(BuildingsFile);
		serializer.Serialize(writerB, _buildings);

		serializer = new(_tenants.GetType());
		using TextWriter writerT = File.CreateText(TenantsFile);
		serializer.Serialize(writerT, _tenants);
	}

	// public void Test()
	// {
	// 	List<Building> address = _buildings.ToList();

	// 	XmlSerializer serializer = new(typeof(List<Building>));

	// 	using TextWriter writer = File.CreateText("test.xml");
	// 	serializer.Serialize(writer, address);


	// 	using TextReader reader = File.OpenText("test.xml");
	// 	List<Building> des = (List<Building>)serializer.Deserialize(reader);

	// 	foreach (var item in des)
	// 	{
	// 		System.Console.WriteLine($"{item}");
	// 	}
	// }



	// public void SerializeViaSerializer<T>(string path, T obj)
	// {
	// 	XmlSerializer serializer = new(typeof(T));
	// 	using TextWriter writer = File.CreateText(path);

	// 	serializer.Serialize(writer, obj);
	// }

	// public void SerializeViaSerializer<T>(Stream stream, T obj)
	// {
	// 	XmlSerializer serializer = new(typeof(T));

	// 	serializer.Serialize(stream, obj);
	// }

	// public object? DeserializeViaSerializer(string path, Type type)
	// {
	// 	XmlSerializer serializer = new(type);
	// 	using TextReader reader = File.OpenText(path);

	// 	return serializer.Deserialize(reader);
	// }

	// public object? DeserializeViaSerializer(Stream stream, Type type)
	// {
	// 	XmlSerializer serializer = new(type);

	// 	return serializer.Deserialize(stream);
	// }

	// public List<Building> DeserializeBuildingsListViaXDocument(string path)
	// {
	// 	List<Building> buildings = [];

	// 	XDocument document = XDocument.Load(path);
	// 	var elements = document.Descendants("Building").ToList();

	// 	foreach (var element in elements)
	// 	{
	// 		switch (element.Attribute("Type")?.Value)
	// 		{
	// 			case "ApartmentHouse":
	// 				buildings.Add(ApartmentHouse.LoadFromXElement(element));
	// 				break;
	// 			case "PrivateHouse":
	// 				buildings.Add(PrivateHouse.LoadFromXElement(element));
	// 				break;
	// 			default:
	// 				break;
	// 		}
	// 	}

	// 	return buildings;
	// }

	// public XElement BuildXElementFromConsole(XElement? root = null)
	// {
	// 	if (root is null)
	// 	{
	// 		while (true)
	// 		{
	// 			try
	// 			{
	// 				System.Console.Write("Element name: ");
	// 				string? name = Read();
	// 				root = new(name);
	// 				break;
	// 			}
	// 			catch (System.Exception e)
	// 			{
	// 				Console.ForegroundColor = ConsoleColor.Red;
	// 				System.Console.WriteLine($"\tError: {e.Message}");
	// 				Console.ForegroundColor = ConsoleColor.White;
	// 			}
	// 		}
	// 	}
	// 	XElement element = root;

	// 	bool Build()
	// 	{
	// 		Console.ForegroundColor = ConsoleColor.Gray;
	// 		System.Console.Write($"\nCurrent building: {element.Name}");
	// 		if (element == root)
	// 			System.Console.Write(" {root}");
	// 		System.Console.WriteLine();
	// 		Console.ForegroundColor = ConsoleColor.White;

	// 		System.Console.Write("Action: ");
	// 		var input = System.Console.ReadLine();

	// 		switch (input)
	// 		{
	// 			case "0":
	// 				Console.ForegroundColor = ConsoleColor.Green;
	// 				System.Console.WriteLine();
	// 				System.Console.WriteLine("Building finished!");
	// 				Console.ForegroundColor = ConsoleColor.White;
	// 				return false;
	// 			case "1":
	// 				AddElementWithContent();
	// 				break;
	// 			case "2":
	// 				AddAttribute();
	// 				break;
	// 			case "3":
	// 				AddElement();
	// 				break;
	// 			case "4":
	// 				RemoveInnerElement();
	// 				break;
	// 			case "5":
	// 				RemoveAttribute();
	// 				break;
	// 			case "6":
	// 				RenameElement();
	// 				break;
	// 			case "h":
	// 				ShowMenu();
	// 				break;
	// 			case "r":
	// 				ShowCurrent();
	// 				break;
	// 			case "c":
	// 				Console.Clear();
	// 				break;
	// 			case "/m":
	// 				MoveToElement();
	// 				break;
	// 			case "/r":
	// 				MoveToRoot();
	// 				break;
	// 			case "/p":
	// 				MoveToParent();
	// 				break;
	// 			default:
	// 				break;
	// 		}

	// 		return true;
	// 	}

	// 	void ShowMenu()
	// 	{
	// 		System.Console.WriteLine("""

	// 		h - show this menu
	// 		r - show current builded
	// 		c - clear console

	// 		/m - move to inner element
	// 		/r - move to root element
	// 		/p - move to parent element

	// 		0 - exit building
	// 		1 - add inner element with content
	// 		2 - add attribute
	// 		3 - add empty element
	// 		4 - remove inner element
	// 		5 - remove attribute
	// 		6 - rename element
	// 		""");
	// 	}

	// 	void ShowCurrent()
	// 	{
	// 		Console.ForegroundColor = ConsoleColor.Gray;
	// 		System.Console.WriteLine();
	// 		System.Console.WriteLine(element);
	// 		Console.ForegroundColor = ConsoleColor.White;
	// 	}

	// 	void RenameElement()
	// 	{
	// 		System.Console.WriteLine("\tRenaming element");

	// 		System.Console.Write("\tNew name: ");
	// 		var eName = Read();

	// 		try
	// 		{
	// 			element.Name = eName;

	// 			Console.ForegroundColor = ConsoleColor.Green;
	// 			System.Console.WriteLine("\tSucces!");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 		}
	// 		catch (System.Exception e)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Red;
	// 			System.Console.WriteLine($"\tError: {e.Message}");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 		}
	// 	}

	// 	void AddElement()
	// 	{
	// 		System.Console.WriteLine("\tBuilding empty element");

	// 		System.Console.Write("\tName: ");
	// 		var eName = Read();

	// 		try
	// 		{
	// 			element.Add(new XElement(eName));

	// 			Console.ForegroundColor = ConsoleColor.Green;
	// 			System.Console.WriteLine("\tAdded!");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 		}
	// 		catch (System.Exception e)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Red;
	// 			System.Console.WriteLine($"\tError: {e.Message}");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 		}
	// 	}

	// 	void AddElementWithContent()
	// 	{
	// 		System.Console.WriteLine("\tBuilding element");

	// 		System.Console.Write("\tName: ");
	// 		var eName = Read();

	// 		System.Console.Write("\tContent: ");
	// 		var eContent = Read();

	// 		try
	// 		{
	// 			element.Add(new XElement(eName, eContent));

	// 			Console.ForegroundColor = ConsoleColor.Green;
	// 			System.Console.WriteLine("\tAdded!");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 		}
	// 		catch (System.Exception e)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Red;
	// 			System.Console.WriteLine($"\tError: {e.Message}");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 		}
	// 	}

	// 	void RemoveInnerElement()
	// 	{
	// 		System.Console.WriteLine("\tRemoving inner element");

	// 		System.Console.Write("\tName: ");
	// 		var eName = Read();

	// 		var selected = element.Elements(eName);

	// 		if (selected.Any() == false)
	// 		{
	// 			LogError("No elements found!");
	// 			return;
	// 		}

	// 		LogSucces($"Found {selected.Count()} elements");

	// 		System.Console.Write("\t\tIndex: ");
	// 		var iIndx = Read();
	// 		int indx = -1;

	// 		while (int.TryParse(iIndx, out indx) == false || indx < 0 || indx > selected.Count() - 1)
	// 		{
	// 			LogError("Invalid index!", "\t\t");
	// 			System.Console.Write("\t\tIndex: ");
	// 			iIndx = Read();
	// 		}

	// 		selected.ElementAt(indx).Remove();

	// 		LogSucces("Succes!", "\t\t");
	// 	}

	// 	void AddAttribute()
	// 	{
	// 		System.Console.WriteLine("\tBuilding attribute");

	// 		System.Console.Write("\tName: ");
	// 		var aName = Read();

	// 		System.Console.Write("\tValue: ");
	// 		var aValue = Read();

	// 		try
	// 		{
	// 			element.Add(new XAttribute(aName, aValue));

	// 			Console.ForegroundColor = ConsoleColor.Green;
	// 			System.Console.WriteLine("\tAdded!");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 		}
	// 		catch (System.Exception e)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Red;
	// 			System.Console.WriteLine($"\tError: {e.Message}");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 		}
	// 	}

	// 	void RemoveAttribute()
	// 	{
	// 		System.Console.WriteLine("\tRemoving attribute");

	// 		System.Console.Write("\tName: ");
	// 		var aName = Read();

	// 		var selected = element.Attribute(aName);

	// 		if (selected is null)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Red;
	// 			System.Console.WriteLine("\tAttribute not found!");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 			return;
	// 		}

	// 		selected.Remove();

	// 		Console.ForegroundColor = ConsoleColor.Green;
	// 		System.Console.WriteLine("\tSucces!");
	// 		Console.ForegroundColor = ConsoleColor.White;
	// 	}

	// 	void MoveToElement()
	// 	{
	// 		System.Console.WriteLine("\tSelecting inner element");

	// 		System.Console.Write("\tName: ");
	// 		var eName = Read();

	// 		var selected = element.Elements(eName);

	// 		if (selected.Any() == false)
	// 		{
	// 			LogError("No elements found!");
	// 			return;
	// 		}

	// 		LogSucces($"Found {selected.Count()} elements");

	// 		System.Console.Write("\t\tIndex: ");
	// 		var iIndx = Read();
	// 		int indx = -1;

	// 		while (int.TryParse(iIndx, out indx) == false || indx < 0 || indx > selected.Count() - 1)
	// 		{
	// 			LogError("Invalid index!", "\t\t");
	// 			System.Console.Write("\t\tIndex: ");
	// 			iIndx = Read();
	// 		}

	// 		element = selected.ElementAt(indx);

	// 		LogSucces("Succes!", "\t\t");
	// 	}

	// 	void MoveToParent()
	// 	{
	// 		var parent = element.Parent;

	// 		if (parent is not null)
	// 			element = parent;
	// 	}

	// 	void MoveToRoot()
	// 	{
	// 		element = root;
	// 	}

	// 	ShowMenu();

	// 	while (Build()) { }

	// 	return element;
	// }

	// public void EnterEditMode()
	// {
	// 	// ConsoleActions actions = new();
	// 	// string[] options = ["Exit", "Read document", "Deserialize document", "Save document", "Show loaded document", "Edit mode", "Build new document"];

	// 	XDocument? document = null;
	// 	string? uri = null;

	// 	LogAction("Entering editing mode");

	// 	bool Edit()
	// 	{

	// 		Console.ForegroundColor = ConsoleColor.Gray;
	// 		System.Console.WriteLine($"\nLast loaded: {(uri is null ? "null" : uri)}");
	// 		Console.ForegroundColor = ConsoleColor.White;

	// 		System.Console.Write("Action: ");
	// 		var input = System.Console.ReadLine();

	// 		switch (input)
	// 		{
	// 			case "h":
	// 				ShowMenu();
	// 				break;
	// 			case "c":
	// 				Console.Clear();
	// 				break;
	// 			case "r":
	// 				ShowDocument();
	// 				break;
	// 			case "l":
	// 				SerializeTestData();
	// 				break;
	// 			case "0":
	// 				LogAction("Exiting");
	// 				// actions.LogAction("Exiting...");
	// 				return false;
	// 			case "1":
	// 				LoadFile();
	// 				break;
	// 			case "2":
	// 				BuildDocument();
	// 				break;
	// 			case "3":
	// 				SaveToFile();
	// 				break;
	// 			case "4":
	// 				DeserializeDocument();
	// 				break;
	// 			case "5":
	// 				BuildNewDocument();
	// 				break;
	// 			default:
	// 				break;
	// 		}

	// 		return true;
	// 	}

	// 	void ShowMenu()
	// 	{
	// 		System.Console.WriteLine("""

	// 		h - show this menu
	// 		c - clear console
	// 		r - show loaded document
	// 		l - serialize test data

	// 		0 - exit
	// 		1 - read file
	// 		2 - enter edit mode
	// 		3 - save to file
	// 		4 - deserialize document
	// 		5 - build new
	// 		""");
	// 	}

	// 	void LoadFile()
	// 	{
	// 		System.Console.WriteLine("\tLoading file");
	// 		System.Console.Write("\tName: ");
	// 		var fName = Read();

	// 		try
	// 		{
	// 			using TextReader reader = File.OpenText(fName);
	// 			document = XDocument.Load(reader);
	// 			uri = fName;

	// 			Console.ForegroundColor = ConsoleColor.Green;
	// 			System.Console.WriteLine("\tLoaded!");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 		}
	// 		catch (System.Exception e)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Red;
	// 			System.Console.WriteLine($"\tError: {e.Message}");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 		}

	// 	}

	// 	void ShowDocument()
	// 	{
	// 		Console.ForegroundColor = ConsoleColor.Gray;
	// 		System.Console.WriteLine();

	// 		if (document is null)
	// 		{
	// 			System.Console.WriteLine("null\n");
	// 			return;
	// 		}

	// 		System.Console.WriteLine(document);

	// 		Console.ForegroundColor = ConsoleColor.White;
	// 	}

	// 	void BuildDocument()
	// 	{
	// 		if (document is null)
	// 		{
	// 			LogError("Document is't loaded!");
	// 			return;
	// 		}

	// 		var root = document.Root;

	// 		if (root is null)
	// 		{
	// 			LogError($"Document contains no elements!");
	// 			return;
	// 		}

	// 		LogSucces("Entering editor mode...");

	// 		BuildXElementFromConsole(root);
	// 	}

	// 	void SerializeTestData()
	// 	{
	// 		CreateXML();
	// 		document = null;
	// 		uri = null;
	// 	}

	// 	void SaveToFile()
	// 	{
	// 		if (document is null)
	// 		{
	// 			LogError("Document is't loaded!");
	// 			return;
	// 		}

	// 		System.Console.Write("\tFile name: ");
	// 		var iName = Read();

	// 		while (iName.Length == 0 || iName.EndsWith(".xml") == false)
	// 		{
	// 			LogError("Ivalid file name!");
	// 			System.Console.Write("\tFile name: ");
	// 			iName = Read();
	// 		}

	// 		document.Save(iName);
	// 		uri = iName;

	// 		LogSucces("Saved!");
	// 	}

	// 	void DeserializeDocument()
	// 	{
	// 		if (document is null)
	// 		{
	// 			LogError("Document is't loaded!");
	// 			return;
	// 		}

	// 		System.Console.Write("\tType name: ");
	// 		var tName = Read();

	// 		while (tName.Length == 0 || Type.GetType(tName) is null)
	// 		{
	// 			LogError("Ivalid type name!");
	// 			System.Console.Write("\tFile name: ");
	// 			tName = Read();
	// 		}

	// 		LogSucces("Type found!", "\t\t");
	// 		Type type = Type.GetType(tName);

	// 		using MemoryStream stream = new();
	// 		document.Save(stream);
	// 		stream.Seek(0, SeekOrigin.Begin);

	// 		try
	// 		{
	// 			var res = DeserializeViaSerializer(stream, type);
	// 			LogSucces("Deserialized!\n");
	// 			PrintObject(res);

	// 		}
	// 		catch (System.Exception e)
	// 		{
	// 			LogError($"{e.Message}\n\t{e.InnerException?.Message}");
	// 		}



	// 	}

	// 	void BuildNewDocument()
	// 	{
	// 		LogSucces("Entering editor mode...");
	// 		System.Console.WriteLine();

	// 		document = new(BuildXElementFromConsole());
	// 	}

	// 	ShowMenu();

	// 	while (Edit()) { }

	// }

	// public void EnterQueryMode()
	// {
	// 	using TextReader reader = File.OpenText(BuildingsFile);
	// 	XDocument? document = XDocument.Load(reader);
	// 	string? uri = BuildingsFile;
	// 	IEnumerable<XElement>? result = [];

	// 	LogAction("Entering query mode");

	// 	bool Query()
	// 	{
	// 		Console.ForegroundColor = ConsoleColor.Gray;
	// 		System.Console.WriteLine($"\nCurrent loaded: {(uri is null ? "null" : uri)}");
	// 		Console.ForegroundColor = ConsoleColor.White;

	// 		System.Console.Write("Action: ");
	// 		var input = System.Console.ReadLine();

	// 		if (document is null && (input != "1" && input != "h" && input != "0" && input != "c"))
	// 		{
	// 			LogError("No document loaded!");
	// 			return true;
	// 		}

	// 		switch (input)
	// 		{
	// 			case "h":
	// 				ShowMenu();
	// 				break;
	// 			case "c":
	// 				Console.Clear();
	// 				break;
	// 			case "r":
	// 				ShowDocument();
	// 				break;
	// 			case "rr":
	// 				ShowResult();
	// 				break;
	// 			case "lr":
	// 				LoadResult();
	// 				break;
	// 			case "s":
	// 				SaveToFile();
	// 				break;
	// 			case "0":
	// 				LogAction("Exiting");
	// 				return false;
	// 			case "1":
	// 				LoadFile();
	// 				break;
	// 			case "2":
	// 				SelectInnerElements();
	// 				break;
	// 			case "3":
	// 				SelectInnerElementsWhere();
	// 				break;
	// 			case "4":
	// 				SelectInnerElementsWhereRecursivelyStart();
	// 				break;
	// 			case "q1":
	// 				Query1();
	// 				break;
	// 			case "q2":
	// 				Query2();
	// 				break;
	// 			case "q3":
	// 				Query3();
	// 				break;
	// 			case "q4":
	// 				Query4();
	// 				break;
	// 			case "q5":
	// 				Query5();
	// 				break;
	// 			case "q6":
	// 				Query6();
	// 				break;
	// 			case "q7":
	// 				Query7();
	// 				break;
	// 			case "q8":
	// 				Query8();
	// 				break;
	// 			case "q9":
	// 				Query9();
	// 				break;
	// 			case "q10":
	// 				Query10();
	// 				break;
	// 			case "q11":
	// 				Query11();
	// 				break;
	// 			default:
	// 				break;
	// 		}

	// 		return true;
	// 	}

	// 	void ShowMenu()
	// 	{
	// 		System.Console.WriteLine("""

	// 		h - show this menu
	// 		c - clear console
	// 		r - show loaded document
	// 		rr - show result
	// 		lr - load result as document
	// 		s - save document to file

	// 		0 - exit
	// 		1 - read file
	// 		2 - select inner elements
	// 		3 - select inner elements where
	// 		4 - select inner elements where recursively

	// 		q1 - вивести наймачів, відсортованих по першій (за часом) реєстрації
	// 		q2 - порахувати кількість наймачів
	// 		q3 - порахувати загальну кількіть квартир
	// 		q4 - вивести наймачів з боргом
	// 		q5 - вивести всіх наймачів, що живуть на вулиці Соборна
	// 		q6 - вивести всіх наймачів з приватним будинком
	// 		q7 - приватні будинки, відсортовані по еффективній площі
	// 		q8 - адреси двоповерхових квартир
	// 		q9 - квартири, згруповані по номеру під'їзда
	// 		q10 - наймачі без реєстрацій
	// 		""");
	// 	}

	// 	void ShowDocument()
	// 	{
	// 		Console.ForegroundColor = ConsoleColor.Gray;
	// 		System.Console.WriteLine();

	// 		if (document is null)
	// 		{
	// 			System.Console.WriteLine("null\n");
	// 			return;
	// 		}

	// 		System.Console.WriteLine(document);

	// 		Console.ForegroundColor = ConsoleColor.White;
	// 	}

	// 	void ShowResult()
	// 	{
	// 		PrintXElements(result);
	// 	}

	// 	void SaveToFile()
	// 	{
	// 		if (document is null)
	// 		{
	// 			LogError("Document is't loaded!");
	// 			return;
	// 		}

	// 		System.Console.Write("\tFile name: ");
	// 		var iName = Read();

	// 		while (iName.Length == 0 || iName.EndsWith(".xml") == false)
	// 		{
	// 			LogError("Ivalid file name!");
	// 			System.Console.Write("\tFile name: ");
	// 			iName = Read();
	// 		}

	// 		document.Save(iName);
	// 		uri = iName;

	// 		LogSucces("Saved!");
	// 	}

	// 	void LoadFile()
	// 	{
	// 		System.Console.WriteLine("\tLoading file");
	// 		System.Console.Write("\tName: ");
	// 		var fName = Read();

	// 		try
	// 		{
	// 			using TextReader reader = File.OpenText(fName);
	// 			document = XDocument.Load(reader);
	// 			uri = fName;

	// 			Console.ForegroundColor = ConsoleColor.Green;
	// 			System.Console.WriteLine("\tLoaded!");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 		}
	// 		catch (System.Exception e)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Red;
	// 			System.Console.WriteLine($"\tError: {e.Message}");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 		}

	// 	}

	// 	void LoadResult()
	// 	{
	// 		if (result is null || result.Any() == false)
	// 		{
	// 			LogError("Result is empty!");
	// 			return;
	// 		}

	// 		document = new(new XElement("Root", result));
	// 		result = [];
	// 		uri = "#result#";
	// 		LogSucces("Succes!");
	// 	}

	// 	void SelectInnerElements()
	// 	{
	// 		System.Console.Write("\tElement name: ");
	// 		var iName = Read();

	// 		result = document?.Descendants(iName);

	// 		LogSucces("Finished!");
	// 	}

	// 	void SelectInnerElementsWhere()
	// 	{
	// 		var selected = document?.Descendants();

	// 		System.Console.Write("\tElement with name: ");
	// 		var eName = Read();
	// 		System.Console.Write("\tHas value: ");
	// 		var eValue = Read();
	// 		System.Console.Write("\tSelect parents? (> 1 for parent of parent): ");
	// 		var iParent = Read();
	// 		System.Console.Write("\tDistinct? (f for false): ");
	// 		var iDisinct = Read();

	// 		var res = from elem in selected
	// 				  where elem.Name == eName
	// 				  where elem.Value == eValue
	// 				  select elem;

	// 		if (int.TryParse(iParent, out int parentsCount))
	// 		{
	// 			for (int i = 1; i <= parentsCount; i++)
	// 			{
	// 				res = from elem in res
	// 					  where elem.Parent is not null
	// 					  select elem.Parent;
	// 			}
	// 		}

	// 		if (iDisinct != "f")
	// 		{
	// 			res = res.Distinct();
	// 		}

	// 		LogSucces("Finished!");
	// 		result = res;
	// 	}

	// 	void SelectInnerElementsWhereRecursivelyStart()
	// 	{
	// 		int t = -1;
	// 		result = SelectInnerElementsWhereRecursively(document.Elements(), ref t);
	// 		LogSucces("Finished!");
	// 	}

	// 	IEnumerable<XElement>? SelectInnerElementsWhereRecursively(IEnumerable<XElement> elements, ref int selectedDepth, int recursionDepth = 0)
	// 	{
	// 		if (elements is null || elements.Any() == false)
	// 		{
	// 			LogError("No items received. Returning...");
	// 			return null;
	// 		}

	// 		Console.ForegroundColor = ConsoleColor.Gray;
	// 		System.Console.WriteLine($"\nRecursion depth = {recursionDepth}; Elements count: {elements.Count()}; Current element: {elements.First().Name}");
	// 		Console.ForegroundColor = ConsoleColor.White;

	// 		System.Console.Write("\tSelect or check value? (c for check): ");
	// 		bool check = Read() == "c";

	// 		IEnumerable<XElement> filteredElements = elements;

	// 		if (check)
	// 		{
	// 			while (true)
	// 			{
	// 				System.Console.Write("\t\tElement with name: ");
	// 				var fName = Read();
	// 				System.Console.Write("\t\tHas value: ");
	// 				var fValue = Read();

	// 				filteredElements = from elem in filteredElements
	// 								   from cElem in elem.Elements(fName)
	// 								   where cElem.Value == fValue
	// 								   select elem;

	// 				if (filteredElements.Any() == false)
	// 				{
	// 					LogError("No elements selected. Returning...", "\t\t");
	// 					return null;
	// 				}

	// 				LogSucces($"Selected elements: {filteredElements.Count()}", "\t\t");

	// 				System.Console.Write("\t\t\tContinue? (t for true): ");
	// 				if (Read() != "t")
	// 					break;

	// 			}

	// 			System.Console.Write("\t\tFinish? (t for true): ");
	// 			if (Read() == "t")
	// 			{
	// 				if (selectedDepth == -1)
	// 				{
	// 					System.Console.Write("\tReturn selected elements? (t for true): ");
	// 					if (Read() == "t")
	// 						selectedDepth = recursionDepth;
	// 				}

	// 				return filteredElements;
	// 			}
	// 			System.Console.WriteLine();
	// 		}

	// 		System.Console.Write("\tSelect element with name: ");
	// 		var eName = Read();

	// 		if (selectedDepth == -1)
	// 		{
	// 			System.Console.Write("\tReturn selected elements? (t for true): ");
	// 			if (Read() == "t")
	// 				selectedDepth = recursionDepth;
	// 		}

	// 		var selected = from elem in filteredElements
	// 					   from cElem in elem.Elements(eName)
	// 					   select cElem;

	// 		var returned = SelectInnerElementsWhereRecursively(selected, ref selectedDepth, recursionDepth + 1);

	// 		if (returned is not null && selectedDepth < recursionDepth)
	// 		{
	// 			return from elem in returned
	// 				   select elem.Parent;
	// 		}

	// 		return returned;
	// 	}

	// 	void PrintXElements(IEnumerable<XElement>? elements)
	// 	{

	// 		if (elements == null)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Red;
	// 			System.Console.WriteLine("null");
	// 			Console.ForegroundColor = ConsoleColor.White;

	// 			return;
	// 		}

	// 		if (elements.Any() == false)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Red;
	// 			System.Console.WriteLine("Empty");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 			return;
	// 		}

	// 		int i = 0;
	// 		foreach (var item in elements)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Blue;
	// 			System.Console.WriteLine($"\n\tElement: {i}");
	// 			Console.ForegroundColor = ConsoleColor.Gray;
	// 			System.Console.WriteLine(item);
	// 			i++;
	// 		}

	// 		Console.ForegroundColor = ConsoleColor.White;
	// 	}

	// 	void PrintXElementsAsObjects<T>(IEnumerable<XElement>? elements)
	// 	{
	// 		if (result is null)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Red;
	// 			System.Console.WriteLine("Null");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 			return;
	// 		}

	// 		var resDoc = new XDocument(new XElement($"ArrayOf{typeof(T).Name}", elements));

	// 		XmlSerializer serializer = new(typeof(List<T>));

	// 		var res = serializer.Deserialize(resDoc.CreateReader());
	// 		List<T> objects = res is null ? [] : (List<T>)res;

	// 		if (objects.Count == 0)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Red;
	// 			System.Console.WriteLine("Empty");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 			return;
	// 		}

	// 		int i = 0;
	// 		foreach (var item in objects)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Blue;
	// 			System.Console.WriteLine($"\nElement: {i}");
	// 			Console.ForegroundColor = ConsoleColor.Gray;
	// 			System.Console.WriteLine(item);
	// 			i++;
	// 		}

	// 		Console.ForegroundColor = ConsoleColor.White;
	// 	}

	// 	XDocument LoadDocument(string uri)
	// 	{
	// 		using TextReader reader = File.OpenText(uri);
	// 		return XDocument.Load(reader);
	// 	}

	// 	//вивести наймачів відсортованих по реїстрації
	// 	void Query1()
	// 	{
	// 		var document = LoadDocument(TenantsFile);

	// 		var res = from tenant in document.Root?.Elements("Tenant")
	// 				  where tenant.Element("Registrations") is not null
	// 				  from registration in tenant.Element("Registrations").Elements("Registration")
	// 				  orderby registration?.Element("Date")?.Value
	// 				  select tenant;

	// 		PrintXElementsAsObjects<Tenant>(res);
	// 	}

	// 	// порахувати кількість наймачів
	// 	void Query2()
	// 	{
	// 		var document = LoadDocument(TenantsFile);

	// 		var res = document.Root?.Elements("Tenant").Count();
	// 		LogSucces($"{res}");
	// 	}

	// 	//порахувати загальну кількіть квартир
	// 	void Query3()
	// 	{
	// 		var document = LoadDocument(BuildingsFile);

	// 		var res = document.Root?.Elements("Building")?.Elements("Apartments")?.Elements("Apartment")?.Count();
	// 		LogSucces($"{res}");
	// 	}

	// 	//вивести наймачів з боргом
	// 	void Query4()
	// 	{
	// 		var document = LoadDocument(TenantsFile);

	// 		var res = from tenant in document.Root?.Elements("Tenant")
	// 				  where tenant.Element("Debt").Value == "true"
	// 				  select tenant;

	// 		PrintXElementsAsObjects<Tenant>(res);
	// 	}

	// 	//вивести всіх наймачів, що живуть на вулиці Соборна
	// 	void Query5()
	// 	{
	// 		var document = LoadDocument(TenantsFile);

	// 		var res = from tenant in document.Root?.Elements("Tenant")
	// 				  where tenant.Element("Registrations") is not null
	// 				  from registration in tenant.Element("Registrations").Elements("Registration")
	// 				  where registration.Element("Address").Element("Street").Value == "Соборна"
	// 				  select tenant;

	// 		PrintXElementsAsObjects<Tenant>(res);
	// 	}

	// 	//вивести всіх наймачів з приватним будинком
	// 	void Query6()
	// 	{
	// 		var buildingsDoc = LoadDocument(BuildingsFile);
	// 		var tenantsDoc = LoadDocument(TenantsFile);

	// 		var res = from building in buildingsDoc.Root.Elements("Building")
	// 				  where building.FirstAttribute.Value == "PrivateHouse"
	// 				  from tenant in tenantsDoc.Root.Elements("Tenant")
	// 				  where tenant.Element("Registrations") is not null
	// 				  where tenant.Element("Registrations").Elements("Registration")
	// 				  		.Any(reg => reg.Element("Address").Value.ToString() == building.Element("Address").Value.ToString())
	// 				  select tenant;

	// 		PrintXElementsAsObjects<Tenant>(res);
	// 	}

	// 	//приватні будинки, відсортовані по еффективній площі
	// 	void Query7()
	// 	{
	// 		var doc = LoadDocument(BuildingsFile);

	// 		// var res = from building in doc.Root.Elements("Building")
	// 		// 		  where building.FirstAttribute.Value == "PrivateHouse"
	// 		// 		  orderby int.Parse(building.Element("EffectiveArea").Value)
	// 		// 		  select building;

	// 		// var res1 = res.Select(x => { x.Name = "PrivateHouse"; return x; });

	// 		var res = doc.Root?.Elements("Building")
	// 						.Where(building => building.FirstAttribute?.Value == "PrivateHouse")
	// 						.OrderBy(building => int.Parse(building?.Element("EffectiveArea")?.Value ?? "-1"))
	// 						.Select(building => { building.Name = "PrivateHouse"; return building; });


	// 		PrintXElementsAsObjects<PrivateHouse>(res);
	// 	}

	// 	//адреси двоповерхових квартир
	// 	void Query8()
	// 	{
	// 		var doc = LoadDocument(BuildingsFile);

	// 		// var res = from building in doc.Root.Elements("Building")
	// 		// 		  where building.FirstAttribute.Value == "ApartmentHouse"
	// 		// 		  from apartment in building.Element("Apartments").Elements("Apartment")
	// 		// 		  where apartment.Element("FloorsCount").Value == "2"
	// 		// 		  select apartment.Element("Address");

	// 		// var res1 = res.Select(x => { x.Name = "ApartmentAddress"; return x; });

	// 		var res = doc.Root?.Elements("Building")
	// 						.Where(building => building.FirstAttribute?.Value == "ApartmentHouse")
	// 						.SelectMany(building => (building.Element("Apartments")?.Elements("Apartment") ?? [])
	// 							.Where(apartment => apartment.Element("FloorsCount")?.Value == "2")
	// 							.Select(apartment => { apartment.Name = "ApartmentAddress"; return apartment; }));

	// 		PrintXElementsAsObjects<ApartmentAddress>(res);
	// 	}

	// 	//квартири, згруповані по номеру під'їзда
	// 	void Query9()
	// 	{
	// 		var doc = LoadDocument(BuildingsFile);

	// 		var res = from building in doc.Root.Elements("Building")
	// 				  where building.FirstAttribute.Value == "ApartmentHouse"
	// 				  from apartment in building.Element("Apartments").Elements("Apartment")
	// 				  group apartment by apartment.Element("Address").Element("EntranceNumber").Value;

	// 		foreach (var item in res)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Blue;
	// 			System.Console.WriteLine($"\nGroup: {item.Key}");
	// 			Console.ForegroundColor = ConsoleColor.Gray;

	// 			foreach (var groupItem in item)
	// 			{
	// 				Console.WriteLine();
	// 				System.Console.WriteLine(groupItem);
	// 			}
	// 		}
	// 		Console.ForegroundColor = ConsoleColor.White;

	// 	}

	// 	//наймачі без реєстрацій
	// 	void Query10()
	// 	{
	// 		var document = LoadDocument(TenantsFile);

	// 		var res = from tenant in document.Root.Elements("Tenant")
	// 				  where tenant.Element("Registrations") is null
	// 				  select tenant;

	// 		PrintXElementsAsObjects<Tenant>(res);

	// 		// PrintXElements(res);
	// 	}

	// 	void Query11()
	// 	{
	// 		var res = GetResidentialsWithTenant();


	// 		if (res == null)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Red;
	// 			System.Console.WriteLine("null");
	// 			Console.ForegroundColor = ConsoleColor.White;

	// 			return;
	// 		}

	// 		if (res.Any() == false)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Red;
	// 			System.Console.WriteLine("Empty");
	// 			Console.ForegroundColor = ConsoleColor.White;
	// 			return;
	// 		}

	// 		int i = 0;
	// 		foreach (var item in res)
	// 		{
	// 			Console.ForegroundColor = ConsoleColor.Blue;
	// 			System.Console.WriteLine($"\n\tElement: {i}");
	// 			Console.ForegroundColor = ConsoleColor.Gray;
	// 			System.Console.WriteLine(item);
	// 			i++;
	// 		}

	// 		Console.ForegroundColor = ConsoleColor.White;
	// 	}



	// 	IEnumerable<XElementWithXElement> GetResidentialsWithTenant()
	// 	{
	// 		var buildingsDoc = LoadDocument(BuildingsFile);
	// 		var tenantsDoc = LoadDocument(TenantsFile);

	// 		var privateHouses = from building in buildingsDoc.Root.Elements("Building")
	// 							where building.FirstAttribute.Value == "PrivateHouse"
	// 							select building;

	// 		var apartments = from building in buildingsDoc.Root?.Elements("Building") ?? []
	// 						 where building.FirstAttribute.Value == "ApartmentHouse"
	// 						 from apartment in building.Element("Apartments").Elements()
	// 						 select apartment;

	// 		var residentials = privateHouses.Concat(apartments);

	// 		var res = from tenant in tenantsDoc?.Root?.Elements() ?? []
	// 				  from registration in tenant?.Element("Registrations")?.Elements() ?? []
	// 				  join residential in residentials on registration.Element("Address")?.Value equals residential.Element("Address")?.Value
	// 				  select new XElementWithXElement(residential, tenant);

	// 		return res;
	// 	}


	// 	ShowMenu();

	// 	while (Query()) { }
	// }

	// private string Read()
	// {
	// 	Console.ForegroundColor = ConsoleColor.Cyan;
	// 	var r = System.Console.ReadLine();
	// 	Console.ForegroundColor = ConsoleColor.White;
	// 	return r is null ? string.Empty : r;
	// }

	// private void LogError(string message, string pad = "\t")
	// {
	// 	Console.ForegroundColor = ConsoleColor.Red;
	// 	System.Console.WriteLine($"{pad}Error: {message}");
	// 	Console.ForegroundColor = ConsoleColor.White;
	// }

	// private void LogSucces(string message, string pad = "\t")
	// {
	// 	Console.ForegroundColor = ConsoleColor.Green;
	// 	System.Console.WriteLine($"{pad}{message}");
	// 	Console.ForegroundColor = ConsoleColor.White;
	// }

	// private void LogTitle(string message)
	// {
	// 	Console.ForegroundColor = ConsoleColor.DarkYellow;
	// 	System.Console.WriteLine();
	// 	Console.WriteLine(new string('=', message.Length + 4));
	// 	Console.CursorLeft = 2;
	// 	System.Console.WriteLine(message);
	// 	Console.WriteLine(new string('=', message.Length + 4));
	// 	Console.ForegroundColor = ConsoleColor.White;
	// }

	// private void LogAction(string message)
	// {
	// 	Console.ForegroundColor = ConsoleColor.Yellow;
	// 	System.Console.WriteLine($"\n{message}...");
	// 	Console.ForegroundColor = ConsoleColor.White;
	// }

	// public void PrintObject(object obj)
	// {
	// 	Console.ForegroundColor = ConsoleColor.Blue;

	// 	System.Console.WriteLine($"Object type: {obj.GetType().Name}\n");
	// 	if (obj is IEnumerable values)
	// 	{
	// 		foreach (var item in values)
	// 		{
	// 			System.Console.WriteLine(item);
	// 		}
	// 	}
	// 	else
	// 	{
	// 		System.Console.WriteLine(obj);
	// 	}

	// 	Console.ForegroundColor = ConsoleColor.White;
	// }

}
