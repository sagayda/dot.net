using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace LINQ_to_objects;

public class Apartment : IResidential
{
	public Apartment() { }

	public Apartment(ApartmentAddress address,
				  ApartmentType type,
				  float totalArea,
				  float effectiveArea,
				  int roomsCount,
				  int floorsCount)
	{
		Address = address;
		Type = type;
		TotalArea = totalArea;
		EffectiveArea = effectiveArea;
		RoomsCount = roomsCount;
		FloorsCount = floorsCount;
	}

	public ApartmentAddress? Address { get; internal set; }
	public ApartmentType Type { get; internal set; }
	public float TotalArea { get; internal set; }
	public float EffectiveArea { get; internal set; }
	public int RoomsCount { get; internal set; }
	public int FloorsCount { get; internal set; }

	public override string ToString()
	{
		return $"Address: {Address}\n\tType: {Type}\n\tArea: {TotalArea}\n\t[E]Area: {EffectiveArea}\n\tRooms: {RoomsCount}\n\tFloors:{FloorsCount}";
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
				case "Address":
					Address = new ApartmentAddress();
					Address.ReadXml(reader);
					break;
				case "ApartmentType":
					Type = (ApartmentType)Enum.Parse(typeof(ApartmentType), reader.ReadElementContentAsString());
					break;
				case "TotalArea":
					TotalArea = reader.ReadElementContentAsInt();
					break;
				case "EffectiveArea":
					EffectiveArea = reader.ReadElementContentAsInt();
					break;
				case "RoomsCount":
					RoomsCount = reader.ReadElementContentAsInt();
					break;
				case "FloorsCount":
					FloorsCount = reader.ReadElementContentAsInt();
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
		writer.WriteStartElement("Address");
		Address?.WriteXml(writer);
		writer.WriteEndElement();
		writer.WriteElementString("ApartmentType", Type.ToString());
		writer.WriteElementString("TotalArea", TotalArea.ToString());
		writer.WriteElementString("EffectiveArea", EffectiveArea.ToString());
		writer.WriteElementString("RoomsCount", RoomsCount.ToString());
		writer.WriteElementString("FloorsCount", FloorsCount.ToString());
	}

	public static Apartment LoadFromXElement(XElement? element)
	{
		if (element is null)
			return new();

		return new Apartment()
		{
			Address = ApartmentAddress.LoadFromXElement(element.Element("Address")),
			Type = Enum.Parse<ApartmentType>(element.Element("Type")?.Value ?? "-1"),
			TotalArea = int.Parse(element.Element("TotalArea")?.Value ?? "-1"),
			EffectiveArea = int.Parse(element.Element("EffectiveArea")?.Value ?? "-1"),
			RoomsCount = int.Parse(element.Element("RoomsCount")?.Value ?? "-1"),
			FloorsCount = int.Parse(element.Element("FloorsCount")?.Value ?? "-1"),
		};
	}
}
