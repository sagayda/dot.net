using System.Text.Json;
using System.Text.Json.Nodes;
using DataModel.Abstract;
using DataModel.Type;
using JsonSerialization;

namespace DataModel;

public class Apartment : IResidential, IJsonSerializable
{
	public Apartment()
	{
		Address = new();
	}

	public Apartment(ApartmentAddress address,
				  ApartmentType type,
				  float totalArea,
				  float effectiveArea,
				  int roomsCount,
				  int floorsCount)
	{
		Address = address;
		Type = type;
		TotalArea = totalArea;
		EffectiveArea = effectiveArea;
		RoomsCount = roomsCount;
		FloorsCount = floorsCount;
	}

	public ApartmentAddress Address { get; set; }
	public ApartmentType Type { get; set; }
	public float TotalArea { get; set; }
	public float EffectiveArea { get; set; }
	public int RoomsCount { get; set; }
	public int FloorsCount { get; set; }

	static IJsonSerializable? IJsonSerializable.Read(JsonElement element)
	{
		return Read(element);
	}

	public static Apartment? Read(JsonElement element)
	{
		if (element.ValueKind != JsonValueKind.Object)
			return null;

		Apartment apartment = new();

		foreach (var item in element.EnumerateObject())
		{
			switch (item.Name.ToLower())
			{
				case "address":
					apartment.Address = ApartmentAddress.Read(item.Value) ?? new();
					break;
				case "type":
					apartment.Type = (ApartmentType)item.Value.GetInt32();
					break;
				case "totalarea":
					apartment.TotalArea = item.Value.GetSingle();
					break;
				case "effectivearea":
					apartment.EffectiveArea = item.Value.GetSingle();
					break;
				case "roomscount":
					apartment.RoomsCount = item.Value.GetInt32();
					break;
				case "floorscount":
					apartment.FloorsCount = item.Value.GetInt32();
					break;
			}
		}

		return apartment;
	}


	public JsonNode Write()
	{
		var jsonNode = new JsonObject()
		{
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
		//\u001b[2m
		// return $"Address: {Address}\n\tType: {Type}\n\tArea: {TotalArea}\n\t[E]Area: {EffectiveArea}\n\tRooms: {RoomsCount}\n\tFloors:{FloorsCount}";
		return $"\u001b[2mType:\u001b[22m {(int)Type,-3} \u001b[2mArea:\u001b[22m {TotalArea,-5} \u001b[2m[E]Area:\u001b[22m {EffectiveArea,-5} \u001b[2mRooms:\u001b[22m {RoomsCount,-3} \u001b[2mFloors:\u001b[22m {FloorsCount,-3} \u001b[2mAddress:\u001b[22m {Address}";
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Address, Type, TotalArea, EffectiveArea, RoomsCount, FloorsCount);
	}

	public override bool Equals(object? obj)
	{
		if (obj == null || obj is not Apartment apartment)
			return false;

		return FloorsCount == apartment.FloorsCount
			&& RoomsCount == apartment.RoomsCount
			&& EffectiveArea == apartment.EffectiveArea
			&& TotalArea == apartment.TotalArea
			&& Type == apartment.Type
			&& Address == apartment.Address;
	}
}
