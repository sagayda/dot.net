using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace LINQ_to_objects;

public class JsonSerialization
{
	private IEnumerable<Tenant> _tenants;
	private IEnumerable<Building> _buildings;

	public JsonSerialization()
	{

	}

	public void Main()
	{
		Address address = new BuildingAddress("Irpin", "Sadova", "65");

		var opt = new JsonSerializerOptions() { WriteIndented = true };
		var json = JsonSerializer.SerializeToElement(address, opt);
		
		System.Console.WriteLine(json);
		System.Console.WriteLine();

		var res = JsonSerializer.Deserialize<BuildingAddress>(json);
		System.Console.WriteLine(res);
	}
}
