using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace LINQ_to_objects;

public abstract class Address : IXmlSerializable
{
	public Address()
	{
		City = string.Empty;
		Street = string.Empty;
		Number = string.Empty;
	}

	public Address(string city, string street, string number)
	{
		City = city;
		Street = street;
		Number = number;
	}

	[JsonInclude]
	public string City { get; protected set; }
	[JsonInclude]
	public string Street { get; protected set; }
	[JsonInclude]
	public string Number { get; protected set; }

	public abstract override string ToString();
	public override abstract bool Equals(object? obj);
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public static bool Equals(Address first, Address second)
	{
		return first.City == second.City && first.Street == second.Street && first.Number == second.Number;
	}

	public abstract XmlSchema? GetSchema();

	public abstract void ReadXml(XmlReader reader);

	public abstract void WriteXml(XmlWriter writer);
}