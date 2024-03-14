namespace LINQ_to_objects;

public class BuildingAddress(string city, string street, string number) : Address(city, street, number)
{
	public override string ToString()
	{
		return $"{City}, {Street} {Number}";
	}
	
	public override bool Equals(object? obj)
	{
		if (obj == null || obj is not BuildingAddress address)
			return false;

		return address.Street == this.Street
			&& address.City == this.City
			&& address.Number == this.Number;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
}
