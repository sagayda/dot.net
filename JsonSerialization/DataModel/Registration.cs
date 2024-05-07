using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using DataModel.Abstract;
using JsonSerialization;

namespace DataModel;

public class Registration : IJsonSerializable
{
	public Registration() { }
	public Registration(DateTime date, Address address)
	{
		Date = date;
		Address = address;
	}

	[JsonInclude]
	public DateTime? Date { get; set; }
	[JsonInclude]
	public Address? Address { get; set; }

	static IJsonSerializable? IJsonSerializable.Read(JsonElement element)
	{
		return Read(element);
	}

	public static Registration? Read(JsonElement element)
	{
		if (element.ValueKind != JsonValueKind.Object)
			return null;

		var reg = new Registration();

		foreach (var item in element.EnumerateObject())
		{
			switch (item.Name.ToLower())
			{
				case "date":
					reg.Date = item.Value.GetDateTime();
					break;
				case "address":
					reg.Address = Address.Read(item.Value);
					break;
			}
		}

		return reg;
	}

	public JsonNode Write()
	{
		var jsonNode = new JsonObject
		{
			{ nameof(Date), JsonValue.Create(Date) },
			{ nameof(Address), Address?.Write()}
		};

		return jsonNode;
	}

	public override string ToString()
	{
		// return $"Date: {Date}\tAddress: {Address}";
		return $"\u001b[2mDate:\u001b[22m {(Date.HasValue ? Date.Value.ToShortDateString() : Date),-10} \u001b[2mAddress:\u001b[22m {Address}";
	}

}
