using DataModel.Abstract;
using DataModel.Type;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DataModel;

public class ApartmentHouse : Building
{
	[JsonInclude]
	public List<Apartment> Apartments { get; } = [];

	public ApartmentHouse() { }

	public new static ApartmentHouse? Read(JsonElement element)
	{
		if (element.ValueKind != JsonValueKind.Object)
			return null;

		ApartmentHouse house = new();

		foreach (var item in element.EnumerateObject())
		{
			switch (item.Name.ToLower())
			{
				case "apartments":
					if (item.Value.ValueKind != JsonValueKind.Array)
						break;

					foreach (var apartmentItem in item.Value.EnumerateArray())
					{
						var apartment = Apartment.Read(apartmentItem);

						if (apartment is not null)
							house.Apartments.Add(apartment);
					}
					break;
				case "address":
					house.Address = BuildingAddress.Read(item.Value) ?? new();
					break;
			}
		}

		return house;
	}

	public ApartmentHouse(BuildingAddress address, ApartmentHouseType type) : base(address)
	{
		Type = type;
	}

	public ApartmentHouseType Type { get; set; }

	public override JsonNode Write()
	{
		var jsonNode = new JsonObject
		{
			{ "$type", TypeDiscriminator.ApartmentHouse.ToString()},
			{ nameof(Address), Address.Write() },
			{ nameof(Type), JsonValue.Create(Type) },
		};

		JsonArray array = [];

		foreach (var item in Apartments)
		{
			array.Add(item.Write());
		}

		jsonNode.Add(nameof(Apartments), array);

		return jsonNode;
	}

	public override string ToString()
	{
		// return $"{base.ToString()}\tApartments count: {_apartments.Count}";
		return $"\u001b[2mType:\u001b[22m {(int)Type,-3} \u001b[2mApartments:\u001b[22m {Apartments.Count,-4} {base.ToString()}";
	}

}
