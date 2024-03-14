namespace LINQ_to_objects;

public class Apartment(ApartmentAddress address,
					   ApartmentType type,
					   float totalArea,
					   float effectiveArea,
					   int roomsCount,
					   int floorsCount) : IResidential
{
	public ApartmentAddress Address {get; } = address;
	public ApartmentType Type { get; } = type;
	public float TotalArea { get; } = totalArea;
	public float EffectiveArea { get; } = effectiveArea;
	public int RoomsCount { get; } = roomsCount;
	public int FloorsCount { get; } = floorsCount;

	public override string ToString()
	{
		return $"Address: {Address}\n\tType: {Type}\n\tArea: {TotalArea}\n\t[E]Area: {EffectiveArea}\n\tRooms: {RoomsCount}\n\tFloors:{FloorsCount}";
	}
}
