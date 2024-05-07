using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace LINQ_to_objects;

public class PrivateHouse : Building, IResidential
{
	public PrivateHouse() { }

	public PrivateHouse(BuildingAddress address,
					 PrivateHouseType type,
					 float totalArea,
					 float effectiveArea,
					 int roomsCount,
					 int floorsCount) : base(address, BuildingType.PrivateHouse)
	{
		Type = type;
		TotalArea = totalArea;
		EffectiveArea = effectiveArea;
		RoomsCount = roomsCount;
		FloorsCount = floorsCount;
	}

	public PrivateHouseType Type { get; internal set; }
	public float TotalArea { get; internal set; }
	public float EffectiveArea { get; internal set; }
	public int RoomsCount { get; internal set; }
	public int FloorsCount { get; internal set; }

	public override string ToString()
	{
		return $"{base.ToString()}\n\tType: {Type}\n\tArea: {TotalArea}\n\t[E]Area: {EffectiveArea}\n\tRooms: {RoomsCount}\n\tFloors:{FloorsCount}";
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
				case "Address":
					Address = new BuildingAddress();
					Address.ReadXml(reader);
					break;
				case "BuildingType":
					BuildingType = (BuildingType)Enum.Parse(typeof(BuildingType), reader.ReadElementContentAsString());
					break;
				case "PrivateHouseType":
					Type = (PrivateHouseType)Enum.Parse(typeof(PrivateHouseType), reader.ReadElementContentAsString());
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

	public override void WriteXml(XmlWriter writer)
	{
		writer.WriteAttributeString("Namespace", GetType().Namespace);
		writer.WriteAttributeString("Type", GetType().Name);
		if (Address is not null)
		{
			writer.WriteStartElement("Address");
			Address.WriteXml(writer);
			writer.WriteEndElement();
		}
		writer.WriteElementString("BuildingType", BuildingType.ToString());
		writer.WriteElementString("PrivateHouseType", Type.ToString());
		writer.WriteElementString("TotalArea", TotalArea.ToString());
		writer.WriteElementString("EffectiveArea", EffectiveArea.ToString());
		writer.WriteElementString("RoomsCount", RoomsCount.ToString());
		writer.WriteElementString("FloorsCount", FloorsCount.ToString());
	}

	public static PrivateHouse LoadFromXElement(XElement element)
	{
		if (element is null)
			return new();

		return new()
		{
			Address = BuildingAddress.LoadFromXElement(element.Element("Address")),
			BuildingType = BuildingType.PrivateHouse,
			Type = Enum.Parse<PrivateHouseType>(element.Element("Type")?.Value ?? "-1"),
			TotalArea = int.Parse(element.Element("TotalArea")?.Value ?? "-1"),
			EffectiveArea = int.Parse(element.Element("EffectiveArea")?.Value ?? "-1"),
			RoomsCount = int.Parse(element.Element("RoomsCount")?.Value ?? "-1"),
			FloorsCount = int.Parse(element.Element("FloorsCount")?.Value ?? "-1"),
		};
	}
}
