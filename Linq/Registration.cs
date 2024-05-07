using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace LINQ_to_objects;

public class Registration : IXmlSerializable
{
	public Registration() { }
	public Registration(DateTime date, Address address)
	{
		Date = date;
		Address = address;
	}

	public DateTime? Date { get; internal set; }
	public Address? Address { get; internal set; }


	public override string ToString()
	{
		return $"Date: {Date}\tAddress: {Address}";
	}

	public XmlSchema? GetSchema()
	{
		return null;
	}

	public void ReadXml(XmlReader reader)
	{
		reader.ReadStartElement();
		while (reader.NodeType != XmlNodeType.EndElement)
		{
			switch (reader.Name)
			{
				case "Date":
					Date = reader.ReadElementContentAsDateTime();
					break;
				case "Address":
					reader.MoveToAttribute("Type");
					switch (reader.Value)
					{
						case "BuildingAddress":
							Address = new BuildingAddress();
							Address.ReadXml(reader);
							break;
						case "ApartmentAddress":
							Address = new ApartmentAddress();
							Address.ReadXml(reader);
							break;
						default:
							break;
					}
					break;
				default:
					reader.Read();
					break;
			}
		}
		reader.ReadEndElement();
	}

	public void WriteXml(XmlWriter writer)
	{
		writer.WriteAttributeString("Namespace", GetType().Namespace);
		writer.WriteAttributeString("Type", GetType().Name);

		if (Date is not null)
		{
			writer.WriteStartElement("Date");
			writer.WriteValue(Date);
			writer.WriteEndElement();
		}

		if (Address is not null)
		{
			writer.WriteStartElement("Address");
			Address.WriteXml(writer);
			writer.WriteEndElement();
		}
	}
}
