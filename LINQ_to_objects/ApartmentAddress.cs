namespace LINQ_to_objects;

public class ApartmentAddress(string city, string street, string number, int apartmentNumber, int entranceNumber) : Address(city, street, number)
{
	public int ApartmentNumber { get; } = apartmentNumber;
	public int EntranceNumber { get; } = entranceNumber;

	public override string ToString()
    {
        return $"{City}, {Street} {Number}, ent. {EntranceNumber} ap. {ApartmentNumber}";
    }
	
	public override bool Equals(object? obj)
	{
		if (obj == null || obj is not ApartmentAddress address)
			return false;

		return address.Street == this.Street
			&& address.City == this.City
			&& address.Number == this.Number
			&& address.EntranceNumber == this.EntranceNumber
			&& address.ApartmentNumber == this.ApartmentNumber;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
}
