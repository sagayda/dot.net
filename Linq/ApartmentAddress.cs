using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace LINQ_to_objects;

public class ApartmentAddress : Address
{
	public ApartmentAddress() { }

	public ApartmentAddress(string city, string street, string number, int apartmentNumber, int entranceNumber) : base(city, street, number)
	{
		ApartmentNumber = apartmentNumber;
		EntranceNumber = entranceNumber;
	}

	public int ApartmentNumber { get; private set; }
	public int EntranceNumber { get; private set; }

	public override string ToString()
	{
		return $"{City}, {Street} {Number}, ent. {EntranceNumber} ap. {ApartmentNumber}";
	}

	public override bool Equals(object? obj)
	{
		if (obj == null || obj is not ApartmentAddress address)
			return false;

		return address.Street == this.Street
			&& address.City == this.City
			&& address.Number == this.Number
			&& address.EntranceNumber == this.EntranceNumber
			&& address.ApartmentNumber == this.ApartmentNumber;
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
				case "ApartmentNumber":
					ApartmentNumber = reader.ReadElementContentAsInt();
					break;
				case "EntranceNumber":
					EntranceNumber = reader.ReadElementContentAsInt();
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
		writer.WriteElementString("ApartmentNumber", ApartmentNumber.ToString());
		writer.WriteElementString("EntranceNumber", EntranceNumber.ToString());
	}

	public static ApartmentAddress LoadFromXElement(XElement? element)
	{
		if (element is null)
			return new();

		return new()
		{
			City = element.Element("City")?.Value ?? "Undefined",
			Street = element.Element("Street")?.Value ?? "Undefined",
			Number = element.Element("Number")?.Value ?? "Undefined",
			ApartmentNumber = int.Parse(element.Element("ApartmentNumber")?.Value ?? "-1"),
			EntranceNumber = int.Parse(element.Element("EntranceNumber")?.Value ?? "-1")
		};
	}
}
