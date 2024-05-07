using System.Xml.Serialization;

namespace LINQ_to_objects;

public interface IResidential : IXmlSerializable
{
	public float TotalArea { get; }
	public float EffectiveArea { get; }
	public int RoomsCount { get; }
	public int FloorsCount { get; }
}
