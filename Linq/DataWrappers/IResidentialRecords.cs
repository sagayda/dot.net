namespace LINQ_to_objects;

public record IResidentialWithAddress(IResidential Residential, Address Address)
{
	public override string ToString()
	{
		return $"{Residential}";
	}
}