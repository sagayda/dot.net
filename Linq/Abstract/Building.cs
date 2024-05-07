using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace LINQ_to_objects;


public abstract class Building : IXmlSerializable
{
	public BuildingAddress? Address { get; internal set; }
	public BuildingType BuildingType { get; internal set; }

	public Building() { }

	public Building(BuildingAddress address, BuildingType type)
	{
		Address = address;
		BuildingType = type;
	}

	public override string ToString()
	{
		return $"Address: {Address}\tBuildingType: {BuildingType}";
	}

	public abstract XmlSchema? GetSchema();

	public abstract void ReadXml(XmlReader reader);

	public abstract void WriteXml(XmlWriter writer);
}
