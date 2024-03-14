namespace LINQ_to_objects;

public class Registration(DateOnly date, Address address)
{
	public DateOnly Date {get;} = date;
	public Address Address {get;} = address;
}
