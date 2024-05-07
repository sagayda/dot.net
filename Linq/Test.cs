using System.Text.Json.Serialization;

namespace LINQ_to_objects;

public class Test 
{
	public string Name { get; set; }
	public string Surname { get; private set; }

	public Test()
	{

	}

	public Test(string name, string surname)
	{
		Name = name;
		Surname = surname;
	}

	public override string ToString()
	{
		return $"Name: {Name}, Surname: {Surname}";
	}
}