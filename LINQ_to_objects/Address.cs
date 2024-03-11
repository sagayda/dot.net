namespace LINQ_to_objects;

public class Address
{
	public Address(string city, string streetName, string number)
	{
		City = city;
		StreetName = streetName;
		Number = number;
	}
	
	public Address(string city, string streetName, string number, int entranceNumber, int apartmentNumber)
	{
		City = city;
		StreetName = streetName;
		Number = number;
		EntranceNumber = entranceNumber;
		ApartmentNumber = apartmentNumber;
	}
	
	public string City {get;}
	public string StreetName { get; }
	public string Number {get;}
	public int? EntranceNumber {get;}
	public int? ApartmentNumber {get;}
	public bool HasEntranceNumber => EntranceNumber != null;
	public bool HasApartmentNumber => ApartmentNumber != null;

	public override bool Equals(object? obj)
	{
		if (obj == null || obj is not Address address)
			return false;
		
		return address.StreetName == this.StreetName
			&& address.City == this.City
			&& address.Number == this.Number
			&& address.EntranceNumber == this.EntranceNumber
			&& address.ApartmentNumber == this.ApartmentNumber;
	}

	public override string ToString()
	{
		string str = $"{City}, {StreetName} {Number}";
		
		if(HasEntranceNumber)
			str += $", ap. {ApartmentNumber}";
		
		if(HasApartmentNumber)
			str += $" ent. {EntranceNumber}";
		
		return str;
	}
}
