using System.Collections;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace LINQ_to_objects;

public class ApartmentHouse : Building, IEnumerable<Apartment>
{
	private readonly List<Apartment> _apartments = [];

	public ApartmentHouse() { }

	public ApartmentHouse(BuildingAddress address, ApartmentHouseType type) : base(address, BuildingType.ApartmentHouse)
	{
		Type = type;
	}

	public ApartmentHouseType Type { get; internal set; }

	public IEnumerator<Apartment> GetEnumerator()
	{
		return _apartments.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public ApartmentHouse Add(Apartment apartment)
	{
		_apartments.Add(apartment);
		return this;
	}

	public ApartmentHouse Add(List<Apartment> apartments)
	{
		foreach (var apartment in apartments)
			_apartments.Add(apartment);

		return this;
	}

	public ApartmentHouse Remove(Apartment apartment)
	{
		_apartments.Remove(apartment);
		return this;
	}

	public override string ToString()
	{
		return $"{base.ToString()}\tApartments count: {_apartments.Count}";
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
					Address = new();
					Address.ReadXml(reader);
					break;
				case "BuildingType":
					BuildingType = (BuildingType)Enum.Parse(typeof(BuildingType), reader.ReadElementContentAsString());
					break;
				case "ApartmentHouseType":
					Type = (ApartmentHouseType)Enum.Parse(typeof(ApartmentHouseType), reader.ReadElementContentAsString());
					break;
				case "Apartments":
					reader.ReadStartElement();
					while (reader.NodeType != XmlNodeType.EndElement)
					{
						Apartment apartment = new();
						apartment.ReadXml(reader);
						_apartments.Add(apartment);
					}
					reader.ReadEndElement();
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
		writer.WriteElementString("ApartmentHouseType", Type.ToString());

		if (_apartments.Count != 0)
		{
			writer.WriteStartElement("Apartments");
			foreach (var item in _apartments)
			{
				writer.WriteStartElement("Apartment");
				item.WriteXml(writer);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}
	}

	public static ApartmentHouse LoadFromXElement(XElement? element)
	{
		if (element is null)
			return new();

		ApartmentHouse house = new()
		{
			Address = BuildingAddress.LoadFromXElement(element.Element("Address")),
			BuildingType = BuildingType.ApartmentHouse,
			Type = Enum.Parse<ApartmentHouseType>(element.Element("Type")?.Value ?? "-1"),
		};

		foreach (var apElement in element.Descendants("Apartment"))
			house.Add(Apartment.LoadFromXElement(apElement));

		return house;
	}
}
