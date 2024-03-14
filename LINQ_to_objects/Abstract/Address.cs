namespace LINQ_to_objects;

public abstract class Address(string city, string street, string number)
{
	public string City { get; } = city;
	public string Street { get; } = street;
	public string Number { get; } = number;

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
}