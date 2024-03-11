using System.Collections;

namespace LINQ_to_objects;

public class ApartmentHouse(Address address, string type) : Building(address, type), IEnumerable<Apartment>
{
	private readonly List<Apartment> _apartments = [];

	public IEnumerator<Apartment> GetEnumerator()
	{
		return _apartments.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
	
	public ApartmentHouse AddApertment(Apartment apartment)
	{
		_apartments.Add(apartment);
		return this;
	}
	
	public ApartmentHouse AddApertment(List<Apartment> apartments)
	{
		foreach(var apartment in apartments)
			_apartments.Add(apartment);
		
		return this;
	}
	
	public ApartmentHouse RemoveAppartment(Apartment apartment)
	{
		_apartments.Remove(apartment);
		return this;
	}

    public override string ToString()
    {
        return $"{base.ToString()}\tApartments count: {_apartments.Count}";
    }
}
