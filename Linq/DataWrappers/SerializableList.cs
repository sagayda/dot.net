using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace LINQ_to_objects;

public class SerializableList<T> : List<T>, IXmlSerializable where T : IXmlSerializable
{
	public XmlSchema? GetSchema()
	{
		return null;
	}

	public void ReadXml(XmlReader reader)
	{

		reader.ReadStartElement();
		while (reader.NodeType != XmlNodeType.EndElement)
		{
			Type type = Type.GetType($"{reader.GetAttribute("Namespace")}.{reader.GetAttribute("Type")}") ?? throw new InvalidOperationException("Failed to determine type");
			var obj = (T)(Activator.CreateInstance(type) ?? throw new InvalidOperationException("Failed to create instance"));

			obj.ReadXml(reader);
			Add(obj);
		}
		reader.ReadEndElement();

	}

	public void WriteXml(XmlWriter writer)
	{
		foreach (var item in this)
		{
			writer.WriteStartElement(typeof(T).Name);
			item.WriteXml(writer);
			writer.WriteEndElement();
		}
	}
}

public class SerializableList : List<object?>, IXmlSerializable
{
	public XmlSchema? GetSchema()
	{
		return null;
	}

	public void ReadXml(XmlReader reader)
	{

		reader.ReadStartElement();
		while (reader.NodeType != XmlNodeType.EndElement)
		{
			Type? type = Type.GetType($"{reader.GetAttribute("Namespace")}.{reader.GetAttribute("Type")}") ?? Type.GetType($"LINQ_to_objects.{reader.Name}") ?? Type.GetType(reader.Name);
			object? obj = null;

			try
			{
				if(type is null)
					throw new InvalidOperationException("Failed to determine type"); 
				
				obj = Activator.CreateInstance(type) ?? throw new InvalidOperationException("Failed to create instance");

				if (type.GetInterface("IXmlSerializable") is not null)
				{
					((IXmlSerializable)obj).ReadXml(reader);
				}
				else
				{
					// XmlSerializer serializer = new(type);
					XmlSerializer serializer;

					if (reader.Name != type.Name)
						serializer = new(type, new XmlRootAttribute(reader.Name));
					else
						serializer = new(type);

					obj = serializer.Deserialize(reader);
				}
			}
			catch (Exception)
			{
				// System.Console.WriteLine($"Failed to deserialize object.\n{e.Message}\n{e.InnerException?.Message}");
				obj = null;
				reader.ReadInnerXml();
			}

			Add(obj);
		}
		reader.ReadEndElement();

	}

	public void WriteXml(XmlWriter writer)
	{
		foreach (var item in this)
		{
			if (item is null)
				continue;

			writer.WriteStartElement(item.GetType().Name);

			try
			{
				if (item is IXmlSerializable serializable)
				{
					serializable.WriteXml(writer);
				}
				else
				{
					Type itemType = item.GetType();

					writer.WriteAttributeString("Namespace", itemType.Namespace);
					writer.WriteAttributeString("Type", itemType.Name);
					XmlSerializer serializer = new(itemType);
					serializer.Serialize(writer, serializer);
				}
			}
			catch (Exception)
			{
				// System.Console.WriteLine($"Failed to serialize object {item}.\n{e.Message}\n{e.InnerException?.Message}");
			}

			writer.WriteEndElement();
		}
	}
}