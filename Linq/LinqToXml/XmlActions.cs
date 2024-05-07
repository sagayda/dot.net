using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LINQ_to_objects;

public class XmlActions : BaseConsoleApp<XDocument?>
{
	private XDocument? _result;
	private SerializableList _resultObjects;
	private XDocument? _document;

	protected override string Info => $"Source: {_document?.Root?.Name ?? "null"};\tResult: {_result?.Root?.Name ?? "null"}\tResult objects: {(_resultObjects.Count <= 0 ? "Null" : $"{_resultObjects.Count} of type {_resultObjects.First()?.GetType().Name ?? "Null"}")}";

	public XmlActions()
	{
		string[] codes = [
			"s",
			"sr",
			"so",
			"l",
			"sv",
			"lr",
			"qe",
			"q",
			"b",
			"dsrl",
			"srl",
		];

		string[] descs = [
			"Show source document",
			"Show edited document",
			"Show result objects",
			"Load document from URI",
			"Save document to file",
			"Load result",
			"Query examples",
			"Query to document",
			"Build document",
			"Deserialize",
			"Serialize",
		];

		Action[] actions = [
			ShowDocument,
			ShowEditedDocument,
			ShowResultObjects,
			LoadDocument,
			SaveDocument,
			LoadResult,
			QueryExample,
			QueryDocument,
			BuildDocument,
			Deserialize,
			Serialize,
		];

		_resultObjects = [];

		InitN(codes, descs, actions);
	}

	public void ShowDocument()
	{
		System.Console.WriteLine();

		LogInfo(_document?.ToString() ?? "Empty");
	}

	public void ShowEditedDocument()
	{
		System.Console.WriteLine();

		LogInfo(_result?.ToString() ?? "Empty");
	}

	public void ShowResultObjects()
	{
		System.Console.WriteLine();
		if (_resultObjects.Count <= 0)
		{
			LogError("No objects to show");
			return;
		}

		LogSequence($"{_resultObjects.Count} objects of type {_resultObjects.First()?.GetType().Name ?? "Null"}", _resultObjects);
	}

	public void LoadDocument()
	{
		var uri = ReadAnswer("Enter URI", File.Exists);

		using TextReader reader = File.OpenText(uri);

		try
		{
			_document = XDocument.Load(reader);
			LogSucces("Successfully loaded!");
		}
		catch (Exception e)
		{
			LogError(e.Message);
		}
	}

	public void LoadResult()
	{
		if (_result is null)
		{
			LogError("Result is null");
			return;
		}

		_document = _result;
		_result = null;
	}

	public void SaveDocument()
	{
		if (_document is null)
		{
			LogError("Document is null");
			return;
		}

		var uri = ReadAnswer("Enter URI", (str) => str.EndsWith(".xml"));

		using TextWriter writer = File.CreateText(uri);

		try
		{
			_document.Save(writer);
			LogSucces("Successfully saved");
		}
		catch (Exception e)
		{
			LogError(e.Message);
		}
	}

	public void QueryExample()
	{
		QueryExamples examples = new(XDocument.Load(LinqToXML.TenantsFile), XDocument.Load(LinqToXML.BuildingsFile));
		examples.Start();
	}

	public void QueryDocument()
	{
		if (_document is null)
		{
			LogError("Document is null");
			return;
		}

		RecursiveQuery query = new(_document);
		query.Start();

		_result = query.GetResult();
	}

	public void BuildDocument()
	{
		XmlBuilder builder;

		if (_document is null)
			builder = new(null);
		else
			builder = new(new XDocument(_document));

		LogAction("Entering building mode...");
		builder.Start();

		_result = builder.GetResult();
	}

	// private void Deserialize()
	// {
	// 	if (_document is null)
	// 	{
	// 		LogError("Document is null");
	// 		return;
	// 	}

	// 	bool list = ReadVariantHorizontaly(["No", "Yes"], "As list") == 1;
	// 	var stype = ReadAnswer("Type", str => Type.GetType(str) is not null);
	// 	Type? type = Type.GetType(stype);

	// 	if (type is null)
	// 	{
	// 		LogError("Invalid type");
	// 		return;
	// 	}

	// 	XmlSerializer serializer = new(type);
	// 	var reader = _document.CreateReader();

	// 	if (list)
	// 	{
	// 		List<object?> res = [];

	// 		try
	// 		{
	// 			reader.ReadStartElement();
	// 			while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
	// 			{
	// 				res.Add(serializer.Deserialize(reader));
	// 			}

	// 			_resultObjects.Clear();
	// 			_resultObjects.AddRange(res);

	// 			LogSucces("Succes");
	// 			// LogSequence($"\nGot {res.Count} elements of type {res.First()?.GetType().Name ?? "Null"}", res);
	// 		}
	// 		catch (Exception e)
	// 		{
	// 			LogError(e.Message);
	// 			return;
	// 		}

	// 	}
	// 	else
	// 	{
	// 		try
	// 		{
	// 			var res = serializer.Deserialize(reader);
	// 			_resultObjects.Clear();
	// 			_resultObjects.Add(res);
	// 			// LogInfo($"Got element of type {res?.GetType().Name ?? "Null"}\n{res?.ToString()}");
	// 			LogSucces("Success");
	// 		}
	// 		catch (Exception e)
	// 		{
	// 			LogError(e.Message);
	// 		}
	// 	}
	// }

	// private void Serialize()
	// {
	// 	if (_resultObjects.Count <= 0)
	// 	{
	// 		LogError("No objects to serialize");
	// 		return;
	// 	}

	// 	string uri = ReadAnswer("URI", (str) => str.EndsWith(".xml"));

	// 	using TextWriter textWriter = File.CreateText(uri);
	// 	using XmlWriter xmlWriter = XmlWriter.Create(textWriter, new() { Indent = true });

	// 	Type? type = _resultObjects.First()?.GetType();

	// 	if (type is null)
	// 	{
	// 		LogError("Error getting type");
	// 		return;
	// 	}

	// 	try
	// 	{
	// 		XmlSerializer serializer = new(type);

	// 		xmlWriter.WriteStartElement($"ArrayOf{type.Name}");
	// 		foreach (var item in _resultObjects)
	// 		{
	// 			serializer.Serialize(xmlWriter, item);
	// 		}
	// 		xmlWriter.WriteEndElement();

	// 		LogSucces("Success");
	// 	}
	// 	catch (Exception e)
	// 	{
	// 		LogError(e.Message);
	// 	}
	// }

	private void Deserialize()
	{
		if (_document is null)
		{
			LogError("Document is null");
			return;
		}

		if (_document.Root is null)
		{
			LogError("Document root is null");
			return;
		}

		bool isList = ReadVariantHorizontaly(["Yes", "No"], "Is list?") == 0;

		try
		{
			XDocument documentToDeserialize;
			if (isList)
				documentToDeserialize = new(new XElement(_document.Root) { Name = "SerializableList" });
			else
				documentToDeserialize = new(new XElement("SerializableList", new XElement(_document.Root)));

			XmlSerializer serializer = new(typeof(SerializableList));
			_resultObjects = (SerializableList?)serializer.Deserialize(documentToDeserialize.CreateReader()) ?? [];

			LogSucces($"Deserialized {_resultObjects.Count} elements");
		}
		catch (Exception e)
		{
			LogError(e.Message);
		}
	}

	private void Serialize()
	{
		if (_resultObjects.Count <= 0)
		{
			LogError("No objects to serialize");
			return;
		}

		// string uri = ReadAnswer("URI", (str) => str.EndsWith(".xml"));

		try
		{
			_result = new();
			
			// using TextWriter textWriter = File.CreateText(uri);
			// using XmlWriter xmlWriter = XmlWriter.Create(textWriter, new() { Indent = true });

			XmlSerializer serializer = new(typeof(SerializableList));
			// serializer.Serialize(textWriter, _resultObjects);
			using XmlWriter writer = _result.CreateWriter();
			serializer.Serialize(writer, _resultObjects);

			LogSucces("Success");
		}
		catch (Exception e)
		{
			LogError(e.Message);
		}
	}
	// public T? DeserializeAs<T>(XDocument document)
	// {
	// 	XmlSerializer serializer = new(typeof(T));

	// 	try
	// 	{
	// 		var res = (T?)serializer.Deserialize(document.CreateReader());
	// 		LogSucces("Deserializer");
	// 		return res;
	// 	}
	// 	catch (Exception e)
	// 	{
	// 		LogError(e.Message);
	// 		return default;
	// 	}

	// }

	// public List<T>? DeserializeListAs<T>(XDocument document)
	// {
	// 	document.Root.Name = $"ArrayOf{typeof(T).Name}";

	// 	Type listType = typeof(List<>);
	// 	Type genericListType = listType.MakeGenericType(typeof(T));

	// 	XmlSerializer serializer = new(genericListType);

	// 	try
	// 	{
	// 		List<T> res = serializer.Deserialize(_document.CreateReader()) as List<T>;
	// 		LogSucces("Deserialized");
	// 		return res;
	// 	}
	// 	catch (Exception e)
	// 	{
	// 		LogError(e.Message);
	// 		return null;
	// 	}
	// }

	public override XDocument? GetResult()
	{
		return _result;
	}
}
