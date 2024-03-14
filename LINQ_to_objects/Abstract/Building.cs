namespace LINQ_to_objects;

public abstract class Building(BuildingAddress address, BuildingType type)
{
	public BuildingAddress Address { get; } = address;
	public BuildingType BuildingType { get; } = type;

    public override string ToString()
    {
        return $"Address: {Address}\tType: {BuildingType}";
    }
}
