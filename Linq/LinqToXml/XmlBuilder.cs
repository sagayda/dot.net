using System.Xml.Linq;

namespace LINQ_to_objects;

public class XmlBuilder : BaseConsoleApp<XDocument>
{
	protected override string Info => $"Current: {_current.Name}{(_current == _document.Root ? "[root]" : string.Empty)}";

	private XDocument _document;
	private XElement _current;

	public XmlBuilder(XDocument? document)
	{
		_document = document is null ? new(new XElement("Root")) : document;
		_current = _document.Root ?? throw new ArgumentNullException(nameof(document), "Document has no root element");

		string[] codes = [
			"s",
			"se",
			"a",
			"ac",
			"r",
			"aa",
			"ra",
			"rnm",
			"rst",
			"/e",
			"/p",
			"/r",	
		];
		
		string[] descs = [
			"Show document",
			"Show current element",
			"Add inner element",
			"Add inner element with content",
			"Remove inner element",
			"Add attribute",
			"Remove attribute",
			"Rename current element",
			"Create new empty document",
			"Move to inner element",
			"Move to parent element",
			"Move to root element",
		];
		
		Action[] actions = [
			ShowDocument,
			ShowCurrent,
			AddElement,
			AddElementWithContent,
			RemoveElement,
			AddAttribute,
			RemoveAttribute,
			RenameElement,
			CreateNew,
			MoveToElement,
			MoveToParent,
			MoveToRoot,
		];
		
		InitN(codes,descs,actions);
	}

	private void ShowDocument()
	{
		LogInfo(_document.ToString());
	}

	private void ShowCurrent()
	{
		LogInfo(_current.ToString());
	}

	private void AddElement()
	{
		var name = ReadAnswer("Name", str => str.Length > 0);

		try
		{
			_current.Add(new XElement(name));
			LogSucces("Added");
		}
		catch (Exception e)
		{
			LogError(e.Message);
		}
	}

	private void AddElementWithContent()
	{
		var name = ReadAnswer("Name", str => str.Length > 0);
		var content = ReadAnswer("Content");

		try
		{
			_current.Add(new XElement(name, content));
			LogSucces("Added");
		}
		catch (Exception e)
		{
			LogError(e.Message);
		}
	}

	private void AddAttribute()
	{
		var name = ReadAnswer("Name", str => str.Length > 0);
		var value = ReadAnswer("Value", str => str.Length > 0);

		try
		{
			_current.Add(new XAttribute(name, value));
			LogSucces("Added");
		}
		catch (Exception e)
		{
			LogError(e.Message);
		}
	}

	private void RemoveElement()
	{
		var name = ReadAnswer("Name", str => str.Length > 0);

		var selected = _current.Elements(name);

		if (selected.Any() == false)
		{
			LogError("No elements found");
			return;
		}

		LogSucces($"Found {selected.Count()} elements");

		int indx = -1;
		ReadAnswer("Index", str => int.TryParse(str, out indx) && indx >= 0 && indx < selected.Count());

		selected.ElementAt(indx).Remove();
		LogSucces("Successfully removed");
	}

	private void RemoveAttribute()
	{
		var name = ReadAnswer("Name", str => str.Length > 0);

		var selected = _current.Attributes(name);

		if (selected.Any() == false)
		{
			LogError("No attributes found");
			return;
		}

		LogSucces($"Found {selected.Count()} attributes");

		int indx = -1;
		ReadAnswer("Index", str => int.TryParse(str, out indx) && indx >= 0 && indx < selected.Count());

		selected.ElementAt(indx).Remove();
		LogSucces("Successfully removed");
	}

	private void RenameElement()
	{
		var name = ReadAnswer("New name", str => str.Length > 0);
		
		try
		{
			_current.Name = name;
			LogSucces("Success");
		}
		catch (Exception e)
		{
			LogError(e.Message);
		}
	}
	
	private void CreateNew()
	{
		_current = new XElement("Root");
		_document = new(_current);
	}
	
	private void MoveToRoot()
	{
		_current = _document.Root ?? throw new ArgumentNullException(nameof(_current), "Document has no root element");
	}

	private void MoveToParent()
	{
		_current = _current.Parent ?? _current;
	}

	private void MoveToElement()
	{
		var name = ReadAnswer("Name", str => str.Length > 0);

		var selected = _current.Elements(name);

		if (selected.Any() == false)
		{
			LogError("No elements found");
			return;
		}

		LogSucces($"Found {selected.Count()} elements");

		int indx = -1;
		ReadAnswer("Index", str => int.TryParse(str, out indx) && indx >= 0 && indx < selected.Count());

		_current = selected.ElementAt(indx);
		LogSucces("Success");
	}

	public override XDocument GetResult()
	{
		return _document;
	}
}
