using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using DataModel.Abstract;
using DataModel.Type;

namespace DataModel;

public class PrivateHouse : Building, IResidential
{
	public PrivateHouse() { }

	public PrivateHouse(BuildingAddress address,
					 PrivateHouseType type,
					 float totalArea,
					 float effectiveArea,
					 int roomsCount,
					 int floorsCount) : base(address)
	{
		Type = type;
		TotalArea = totalArea;
		EffectiveArea = effectiveArea;
		RoomsCount = roomsCount;
		FloorsCount = floorsCount;
	}

	[JsonInclude]
	public PrivateHouseType Type { get; set; }
	[JsonInclude]
	public float TotalArea { get; set; }
	[JsonInclude]
	public float EffectiveArea { get; set; }
	[JsonInclude]
	public int RoomsCount { get; set; }
	[JsonInclude]
	public int FloorsCount { get; set; }

	public new static PrivateHouse? Read(JsonElement element)
	{
		if (element.ValueKind != JsonValueKind.Object)
			return null;

		PrivateHouse house = new();

		foreach (var item in element.EnumerateObject())
		{
			switch (item.Name.ToLower())
			{
				case "type":
					house.Type = (PrivateHouseType)item.Value.GetInt32();
					break;
				case "address":
					house.Address = BuildingAddress.Read(item.Value) ?? new();
					break;
				case "totalarea":
					house.TotalArea = item.Value.GetSingle();
					break;
				case "effectivearea":
					house.EffectiveArea = item.Value.GetSingle();
					break;
				case "roomscount":
					house.RoomsCount = item.Value.GetInt32();
					break;
				case "floorscount":
					house.FloorsCount = item.Value.GetInt32();
					break;
			}
		}

		return house;
	}

	public override JsonNode Write()
	{
		var jsonNode = new JsonObject
		{
			{ "$type", TypeDiscriminator.PrivateHouse.ToString()},
			{ nameof(Address), Address.Write() },
			{ nameof(Type), JsonValue.Create(Type) },
			{ nameof(TotalArea), JsonValue.Create(TotalArea) },
			{ nameof(EffectiveArea), JsonValue.Create(EffectiveArea) },
			{ nameof(RoomsCount), JsonValue.Create(RoomsCount) },
			{ nameof(FloorsCount), JsonValue.Create(FloorsCount) }
		};

		return jsonNode;
	}

	public override string ToString()
	{
		return $"\u001b[2mType:\u001b[22m {(int)Type,-3} \u001b[2mArea:\u001b[22m {TotalArea,-5} \u001b[2m[E]Area:\u001b[22m {EffectiveArea,-5} \u001b[2mRooms:\u001b[22m {RoomsCount,-3} \u001b[2mFloors:\u001b[22m {FloorsCount,-3} {base.ToString()}";
	}
}
