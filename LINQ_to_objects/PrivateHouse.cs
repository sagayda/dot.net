namespace LINQ_to_objects;

public class PrivateHouse(BuildingAddress address,
						  PrivateHouseType type,
						  float totalArea,
						  float effectiveArea,
						  int roomsCount,
						  int floorsCount) : Building(address, BuildingType.PrivateHouse), IResidential
{
	public PrivateHouseType Type { get; } = type;
	public float TotalArea { get; } = totalArea;
	public float EffectiveArea { get; } = effectiveArea;
	public int RoomsCount { get; } = roomsCount;
	public int FloorsCount { get; } = floorsCount;

	public override string ToString()
	{
		return $"{base.ToString()}\n\tArea: {TotalArea}\n\t[E]Area: {EffectiveArea}\n\tRooms: {RoomsCount}\n\tFloors:{FloorsCount}";
	}
}
