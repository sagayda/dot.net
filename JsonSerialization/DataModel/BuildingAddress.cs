using System.Text.Json;
using System.Text.Json.Nodes;
using DataModel.Abstract;

namespace DataModel;

public class BuildingAddress : Address
{
	public BuildingAddress() { }
	public BuildingAddress(string city, string street, string number) : base(city, street, number) { }

	public new static BuildingAddress? Read(JsonElement element)
	{
		if (element.ValueKind != JsonValueKind.Object)
			return null;

		BuildingAddress address = new();
		string errorStr = "UNDEFINED";

		foreach (var item in element.EnumerateObject())
		{
			switch (item.Name.ToLower())
			{
				case "city":
					address.City = item.Value.GetString() ?? errorStr;
					break;
				case "street":
					address.Street = item.Value.GetString() ?? errorStr;
					break;
				case "number":
					address.Number = item.Value.GetString() ?? errorStr;
					break;
				default:
					break;
			}
		}

		return address;
	}

	public override JsonNode Write()
	{
		var jsonNode = new JsonObject
		{
			{"$type", TypeDiscriminator.BuildingAddress.ToString() },
			{ nameof(City), JsonValue.Create(City) },
			{ nameof(Street), JsonValue.Create(Street) },
			{ nameof(Number), JsonValue.Create(Number) },
		};

		return jsonNode;
	}
	
	public override string ToString()
	{
		return base.ToString();
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
		return HashCode.Combine(City, Street, Number);
	}
}
