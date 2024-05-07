using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace LINQ_to_objects;

public class BuildingAddress : Address
{
	public BuildingAddress() { }
	public BuildingAddress(string city, string street, string number) : base(city, street, number) { }

	public override string ToString()
	{
		return $"{City}, {Street} {Number}";
	}

	public override bool Equals(object? obj)
	{
		if (obj == null || obj is not BuildingAddress address)
			return false;

		return address.Street == this.Street
			&& address.City == this.City
			&& address.Number == this.Number;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override XmlSchema? GetSchema()
	{
		return null;
	}

	public override void ReadXml(XmlReader reader)
	{
		reader.ReadStartElement();
		while (reader.NodeType != XmlNodeType.EndElement)
		{
			switch (reader.Name)
			{
				case "City":
					City = reader.ReadElementContentAsString();
					break;
				case "Street":
					Street = reader.ReadElementContentAsString();
					break;
				case "Number":
					Number = reader.ReadElementContentAsString();
					break;
				default:
					reader.Read();
					break;
			}
		}
		reader.ReadEndElement();

	}

	public override void WriteXml(XmlWriter writer)
	{
		writer.WriteAttributeString("Namespace", GetType().Namespace);
		writer.WriteAttributeString("Type", GetType().Name);
		writer.WriteElementString("City", City);
		writer.WriteElementString("Street", Street);
		writer.WriteElementString("Number", Number);
	}

	public static BuildingAddress LoadFromXElement(XElement? element)
	{
		if (element is null)
			return new();

		return new()
		{
			City = element.Element("City")?.Value ?? "Undefined",
			Street = element.Element("Street")?.Value ?? "Undefined",
			Number = element.Element("Number")?.Value ?? "Undefined",
		};
	}
}
