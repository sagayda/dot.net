using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using DataModel.Abstract;

namespace DataModel;

public class ApartmentAddress : Address
{
	public ApartmentAddress() { }

	public ApartmentAddress(string city, string street, string number, int apartmentNumber, int entranceNumber) : base(city, street, number)
	{
		ApartmentNumber = apartmentNumber;
		EntranceNumber = entranceNumber;
	}

	[JsonInclude]
	public int ApartmentNumber { get; set; }
	[JsonInclude]
	public int EntranceNumber { get; set; }

	public new static ApartmentAddress? Read(JsonElement element)
	{
		if (element.ValueKind != JsonValueKind.Object)
			return null;

		ApartmentAddress address = new();
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
				case "apartmentnumber":
					address.ApartmentNumber = item.Value.GetInt32();
					break;
				case "entrancenumber":
					address.EntranceNumber = item.Value.GetInt32();
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
			{"$type", TypeDiscriminator.ApartmentAddress.ToString() },
			{ nameof(City), JsonValue.Create(City) },
			{ nameof(Street), JsonValue.Create(Street) },
			{ nameof(Number), JsonValue.Create(Number) },
			{ nameof(ApartmentNumber), JsonValue.Create(ApartmentNumber) },
			{ nameof(EntranceNumber), JsonValue.Create(EntranceNumber) }
		};

		return jsonNode;
	}

	public override string ToString()
	{
		return $"{base.ToString()}, \u001b[2ment.\u001b[22m {EntranceNumber} \u001b[2map.\u001b[22m {ApartmentNumber}";
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
		return HashCode.Combine(City, Street, Number, ApartmentNumber, EntranceNumber);
	}
}
