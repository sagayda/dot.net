using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using JsonSerialization;

namespace DataModel.Abstract;


[JsonDerivedType(typeof(ApartmentAddress), typeDiscriminator: nameof(ApartmentAddress))]
[JsonDerivedType(typeof(BuildingAddress), typeDiscriminator: nameof(BuildingAddress))]
public abstract class Address : IJsonSerializable
{
	public enum TypeDiscriminator
	{
		BuildingAddress = 1,
		ApartmentAddress = 2,
	}

	public Address()
	{
		City = string.Empty;
		Street = string.Empty;
		Number = string.Empty;
	}

	public Address(string city, string street, string number)
	{
		City = city;
		Street = street;
		Number = number;
	}

	[JsonInclude]
	public string City { get; set; }
	[JsonInclude]
	public string Street { get; set; }
	[JsonInclude]
	public string Number { get; set; }

	public abstract JsonNode Write();

	static IJsonSerializable? IJsonSerializable.Read(JsonElement element)
	{
		return Read(element);
	}
	
	public static Address? Read(JsonElement element)
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
				TypeDiscriminator.BuildingAddress => BuildingAddress.Read(element),
				TypeDiscriminator.ApartmentAddress => ApartmentAddress.Read(element),
				_ => null,
			};
		}

		return null;
	}

	public override string ToString()
	{
		return $"{City}, {Street} {Number}";
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(City, Street, Number);
	}

	public override abstract bool Equals(object? obj);
}