namespace LINQ_to_objects;

public class PrivateHouse(Address address,
						  string type,
						  float totalArea,
						  float effectiveArea,
						  int roomsCount,
						  int floorsCount) : Building(address, type), IResidential
{
	public float TotalArea { get; } = totalArea;
	public float EffectiveArea { get; } = effectiveArea;
	public int RoomsCount { get; } = roomsCount;
	public int FloorsCount { get; } = floorsCount;

    public override string ToString()
    {
        return $"{base.ToString()}\n\tArea: {TotalArea}\n\t[E]Area: {EffectiveArea}\n\tRooms: {RoomsCount}\n\tFloors:{FloorsCount}";
    }
}
