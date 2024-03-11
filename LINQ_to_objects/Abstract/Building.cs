namespace LINQ_to_objects;

public abstract class Building(Address address, string type)
{
	public Address Address { get; } = address;
	public string Type { get; } = type;

    public override string ToString()
    {
        return $"Address: {Address}\tType: {Type}";
    }
}
