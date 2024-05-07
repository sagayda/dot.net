using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using JsonSerialization;

namespace DataModel.Abstract;

[JsonDerivedType(typeof(ApartmentHouse), typeDiscriminator: nameof(ApartmentHouse))]
[JsonDerivedType(typeof(PrivateHouse), typeDiscriminator: nameof(PrivateHouse))]
public abstract class Building : IJsonSerializable
{
	public enum TypeDiscriminator
	{
		PrivateHouse = 1,
		ApartmentHouse = 2,
	}

	[JsonInclude]
	public BuildingAddress Address { get; set; }

	public Building()
	{
		Address = new();
	}

	public Building(BuildingAddress address)
	{
		Address = address;
	}

	public abstract JsonNode Write();

	static IJsonSerializable? IJsonSerializable.Read(JsonElement element)
	{
		return Read(element);
	}

	public static Building? Read(JsonElement element)
	{
		if (element.ValueKind != JsonValueKind.Object)
			return null;

		if (element.TryGetProperty("$type", out var type))
		{
			Enum.TryParse(typeof(TypeDiscriminator), type.GetString(), true, out var res);

			if (res is not TypeDiscriminator discriminator)
				return null;

			return discriminator switch
			{
				TypeDiscriminator.PrivateHouse => PrivateHouse.Read(element),
				TypeDiscriminator.ApartmentHouse => ApartmentHouse.Read(element),
				_ => null,
			};
		}

		return null;
	}

	public override string ToString()
	{
		return $"\u001b[2mAddress:\u001b[22m {Address}";
	}
}
