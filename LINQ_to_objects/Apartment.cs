namespace LINQ_to_objects;

public class Apartment(Address address,
					   string type,
					   float totalArea,
					   float effectiveArea,
					   int roomsCount,
					   int floorsCount) : IResidential
{
	public Address Address {get; } = address;
	public string Type { get; } = type;
	public float TotalArea { get; } = totalArea;
	public float EffectiveArea { get; } = effectiveArea;
	public int RoomsCount { get; } = roomsCount;
	public int FloorsCount { get; } = floorsCount;

    public override string ToString()
    {
        return $"Address:\n{Address}\n\tType: {Type}\n\tArea: {TotalArea}\n\t[E]Area: {EffectiveArea}\n\tRooms: {RoomsCount}\n\tFloors:{FloorsCount}";
    }
}
