using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using JsonSerialization;

namespace DataModel;

public class Tenant : IJsonSerializable
{
	public Tenant()
	{
		Name = string.Empty;
		Middlename = string.Empty;
		Lastname = string.Empty;
		Registrations = [];
	}

	public Tenant(string name,
					string lastname,
					string middlename,
					int familyMemberCount,
					int childrenCount,
					bool debt)
	{
		Name = name;
		Lastname = lastname;
		Middlename = middlename;
		FamilyMembersCount = familyMemberCount;
		ChildrenCount = childrenCount;
		Debt = debt;
		Registrations = [];
	}

	[JsonInclude]
	public string Name { get; set; }
	[JsonInclude]
	public string Lastname { get; set; }
	[JsonInclude]
	public string Middlename { get; set; }
	[JsonInclude]
	public int FamilyMembersCount { get; set; }
	[JsonInclude]
	public int ChildrenCount { get; set; }
	[JsonInclude]
	public bool Debt { get; set; }
	[JsonInclude]
	public List<Registration> Registrations { get; set; }

	static IJsonSerializable? IJsonSerializable.Read(JsonElement element)
	{
		return Read(element);
	}

	public static Tenant? Read(JsonElement element)
	{
		if (element.ValueKind != JsonValueKind.Object)
			return null;

		Tenant tenant = new();
		string errorStr = "UNDEFINED";

		foreach (var item in element.EnumerateObject())
		{
			switch (item.Name.ToLower())
			{
				case "name":
					tenant.Name = item.Value.GetString() ?? errorStr;
					break;
				case "lastname":
					tenant.Lastname = item.Value.GetString() ?? errorStr;
					break;
				case "middlename":
					tenant.Middlename = item.Value.GetString() ?? errorStr;
					break;
				case "familymemberscount":
					tenant.FamilyMembersCount = item.Value.GetInt32();
					break;
				case "childrencount":
					tenant.ChildrenCount = item.Value.GetInt32();
					break;
				case "debt":
					tenant.Debt = item.Value.GetBoolean();
					break;
				case "registrations":
					if (item.Value.ValueKind != JsonValueKind.Array)
						break;

					foreach (var regItem in item.Value.EnumerateArray())
					{
						var reg = Registration.Read(regItem);

						if (reg != null)
							tenant.Registrations.Add(reg);
					}
					break;
			}
		}

		return tenant;
	}

	public JsonNode Write()
	{
		var jsonNode = new JsonObject
		{
			{ nameof(Name), JsonValue.Create(Name) },
			{ nameof(Lastname), JsonValue.Create(Lastname) },
			{ nameof(Middlename), JsonValue.Create(Middlename) },
			{ nameof(FamilyMembersCount), JsonValue.Create(FamilyMembersCount) },
			{ nameof(ChildrenCount), JsonValue.Create(ChildrenCount) },
			{ nameof(Debt), JsonValue.Create(Debt) },
		};

		JsonArray array = [];

		foreach (var item in Registrations)
		{
			array.Add(item.Write());
		}

		jsonNode.Add(nameof(Registrations), array);

		return jsonNode;
	}

	public override string ToString()
	{
		// return $"{Lastname} {Name} {Middlename}\tFamily: {FamilyMembersCount}[m] {ChildrenCount}[c]\tDebt: {Debt}\tRegistrations: {_registrations.Count}";
		return $"{$"{Lastname} {Name} {Middlename}",-35} \u001b[2mFamily:\u001b[22m [M]{FamilyMembersCount,-2} [C]{ChildrenCount,-2} \u001b[2mDebt:\u001b[22m {Debt,-6} \u001b[2mRegistrations:\u001b[22m {Registrations.Count}";
	}
}
