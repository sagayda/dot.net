namespace LINQ_to_objects;

using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

public class QueryExamples : BaseConsoleApp
{
	private readonly XDocument _tenants;
	private readonly XDocument _buildings;

	protected override string Info => string.Empty;

	public QueryExamples(XDocument tenants, XDocument buildings)
	{
		ArgumentNullException.ThrowIfNull(tenants);
		ArgumentNullException.ThrowIfNull(buildings);

		_tenants = tenants;
		_buildings = buildings;

		// System.Console.WriteLine(_buildings.ToString());

		string[] codes = [
			 "q1",
			 "q2",
			 "q3",
			 "q4",
			 "q5",
			 "q6",
			 "q7",
			 "q8",
			 "q9",
			 "q10",
			 "q11",
			 "q12",
			 "q13",
			 "q14",
			 "q15"
		];

		string[] descs = [
			"вивести наймачів згрупованих по кількості членів сім'ї",
			"порахувати кількість наймачів",
			"порахувати загальну кількіть квартир",
			"вивести наймачів з боргом",
			"вивести всіх наймачів, що живуть на вулиці Соборна",
			"вивести всіх наймачів з приватним будинком",
			"приватні будинки, відсортовані по еффективній площі",
			"адреси двоповерхових квартир",
			"квартири, згруповані по номеру під'їзда",
			"наймачі без реєстрацій",
			"наймач + приватний будинок",
			"наймач + квартира",
			"кожне житло",
			"наймачі, що мають реєстрацію з 2020 по 2021 роки",
			"наймачі, відсортовані по площі житла, що вони знімають",
		];

		Action[] actions = [
			Query1,
			Query2,
			Query3,
			Query4,
			Query5,
			Query6,
			Query7,
			Query8,
			Query9,
			Query10,
			Query11,
			Query12,
			Query13,
			Query14,
			Query15,
		];

		InitN(codes, descs, actions);
	}

	//вивести наймачів згрупованих по кількості членів сім'ї
	void Query1()
	{

		var res = from tenant in _tenants.Root?.Elements("Tenant") ?? []
				  group tenant by tenant.Element("FamilyMembersCount")?.Value;

		XmlSerializer serializer = new(typeof(Tenant));

		foreach (var item in res)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			System.Console.WriteLine($"\nGroup: {item.Key}");
			Console.ForegroundColor = ConsoleColor.Gray;

			foreach (var groupItem in item)
			{
				Console.WriteLine();
				System.Console.WriteLine(serializer.Deserialize(groupItem.CreateReader()));
			}
		}

		// PrintXElementsAsObjects<Tenant>(res);
	}

	// порахувати кількість наймачів
	void Query2()
	{
		var res = _tenants.Root?.Elements("Tenant").Count();
		LogSucces($"{res}");
	}

	//порахувати загальну кількіть квартир
	void Query3()
	{
		var res = _buildings.Root?.Elements("Building")?.Elements("Apartments")?.Elements("Apartment")?.Count();
		LogSucces($"{res}");
	}

	//вивести наймачів з боргом
	void Query4()
	{
		var res = from tenant in _tenants.Root?.Elements("Tenant") ?? []
				  where tenant?.Element("Debt")?.Value == "true"
				  select tenant;

		PrintXElementsAsObjects<Tenant>(res);
	}

	//вивести всіх наймачів, що живуть на вулиці Соборна
	void Query5()
	{
		var res = from tenant in _tenants.Root?.Elements("Tenant") ?? []
				  from registration in tenant.Element("Registrations")?.Elements("Registration") ?? []
				  where registration.Element("Address")?.Element("Street")?.Value == "Соборна"
				  select tenant;

		PrintXElementsAsObjects<Tenant>(res);
	}

	//вивести всіх наймачів з приватним будинком
	void Query6()
	{
		var res = from building in _buildings.Root?.Elements("Building") ?? []
				  where building.Attribute("Type")?.Value == "PrivateHouse"
				  from tenant in _tenants.Root?.Elements("Tenant") ?? []
				  where tenant.Element("Registrations")?.Elements("Registration").Any(reg => reg.Element("Address")?.Value.ToString() == building.Element("Address")?.Value.ToString()) ?? false
				  select tenant;

		PrintXElementsAsObjects<Tenant>(res);
	}

	//приватні будинки, відсортовані по еффективній площі
	void Query7()
	{
		var res = _buildings.Root?.Elements("Building")
						.Where(building => building.Attribute("Type")?.Value == "PrivateHouse")
						.OrderBy(building => (float?)building?.Element("EffectiveArea") ?? 0)
						// .Select(building => new XElement("PrivateHouse", building.Value));
						.Select(building => new XElement(building) { Name = "PrivateHouse" });


		PrintXElementsAsObjects<PrivateHouse>(res);
	}

	//адреси двоповерхових квартир
	void Query8()
	{
		var res = _buildings.Root?.Elements("Building")
						.Where(building => building.Attribute("Type")?.Value == "ApartmentHouse")
						.SelectMany(building => (building.Element("Apartments")?.Elements("Apartment") ?? [])
							.Where(apartment => apartment.Element("FloorsCount")?.Value == "2")
							// .Select(apartment => new XElement(apartment.Element("Address") ?? new XElement("Address")) { Name = "ApartmentAddress" }));
							.Select(apartment => apartment.Element("Address") ?? new XElement("Address")));

		PrintXElementsAsObjects<ApartmentAddress>(res);
	}

	//квартири, згруповані по номеру під'їзда
	void Query9()
	{
		var res = from building in _buildings.Root?.Elements("Building") ?? []
				  where building.Attribute("Type")?.Value == "ApartmentHouse"
				  from apartment in building.Element("Apartments")?.Elements("Apartment") ?? []
				  group apartment by apartment.Element("Address")?.Element("EntranceNumber")?.Value ?? "-1";

		XmlSerializer serializer = new(typeof(Apartment));

		foreach (var item in res)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			System.Console.WriteLine($"\nGroup: {item.Key}");
			Console.ForegroundColor = ConsoleColor.Gray;

			foreach (var groupItem in item)
			{
				Console.WriteLine();
				System.Console.WriteLine(serializer.Deserialize(groupItem.CreateReader()));
			}
		}
		Console.ForegroundColor = ConsoleColor.White;

	}

	//наймачі без реєстрацій
	void Query10()
	{
		var res = from tenant in _tenants.Root?.Elements("Tenant") ?? []
				  where tenant.Element("Registrations") is null
				  select tenant;

		PrintXElementsAsObjects<Tenant>(res);

		// PrintXElements(res);
	}

	//наймач + приватний будинoк
	void Query11()
	{
		var res = from tenant in _tenants.Root?.Elements("Tenant") ?? []
				  from registration in tenant.Element("Registrations")?.Elements("Registration") ?? []
				  join privateHouse in
					(from building in _buildings.Root?.Elements("Building") ?? []
					 where building.Element("BuildingType")?.Value == "PrivateHouse"
					 select building)
				  on registration.Element("Address")?.Value equals privateHouse.Element("Address")?.Value
				  select new XElementPair(tenant, privateHouse);

		List<Pair<Tenant?, PrivateHouse?>> pairs = [];

		foreach (var item in res)
			if (item.TryDeserialize(out Pair<Tenant?, PrivateHouse?>? pair, true) && pair is not null)
				pairs.Add(pair);

		PrintSequence(pairs);
	}

	//наймач + квартира
	void Query12()
	{
		var res = from tenant in _tenants.Root?.Elements("Tenant") ?? []
				  from registration in tenant.Element("Registrations")?.Elements("Registration") ?? []
				  join apartment in
					(from building in _buildings.Root?.Elements("Building") ?? []
					 where building.Element("BuildingType")?.Value == "ApartmentHouse"
					 from apartment in building.Element("Apartments")?.Elements("Apartment") ?? []
					 select apartment)
				  on registration.Element("Address")?.Value equals apartment.Element("Address")?.Value
				  select new XElementPair(tenant, apartment);

		List<Pair<Tenant?, Apartment?>> pairs = [];

		foreach (var item in res)
			if (item.TryDeserialize(out Pair<Tenant?, Apartment?>? pair, true) && pair is not null)
				pairs.Add(pair);

		PrintSequence(pairs);
	}

	//кожне житло
	void Query13()
	{
		var privateHouses = _buildings.Root?.Elements("Building")
							.Where(b => b.Element("BuildingType")?.Value == "PrivateHouse") ?? [];
							// .Select(b => new XElement(b) { Name = "PrivateHouse" }) ?? [];

		var apartments = _buildings.Root?.Elements("Building")
								   .Where(b => b.Element("BuildingType")?.Value == "ApartmentHouse")
								   .SelectMany(b => b.Element("Apartments")?.Elements("Apartment") ?? []) ?? [];

		PrintXElementsAsObjects<IResidential>(apartments.Concat(privateHouses));
	}

	//наймачі, що мають реєстрацію з t1 по t2
	void Query14()
	{
		DateTime start = new(2015, 1, 1);
		DateTime end = new(2020, 1, 1);

		var res = from tenant in _tenants.Root?.Elements("Tenant")
				  from registration in tenant.Element("Registrations")?.Elements("Registration") ?? []
				  where end > ((DateTime?)registration.Element("Date")) && ((DateTime?)registration.Element("Date")) > start
				  select tenant;

		PrintXElementsAsObjects<Tenant>(res);
	}

	//наймачі, відсортовані по площі житла
	void Query15()
	{
		IEnumerable<XElement> privateHouses = _buildings.Root?.Elements("Building")
							.Where(b => b.Element("BuildingType")?.Value == "PrivateHouse") ?? [];
							// .Select(b => new XElement(b) { Name = "PrivateHouse" }) ?? [];

		IEnumerable<XElement> apartments = _buildings.Root?.Elements("Building")
								   .Where(b => b.Element("BuildingType")?.Value == "ApartmentHouse")
								   .SelectMany(b => b.Element("Apartments")?.Elements("Apartment") ?? []) ?? [];

		IEnumerable<XElement> residentials = apartments.Concat(privateHouses);

		var res = from tenant in _tenants.Root?.Elements("Tenant")
				  from registration in tenant.Element("Registrations")?.Elements("Registration") ?? []
				  join residential in residentials on registration.Element("Address")?.Value equals residential.Element("Address")?.Value
				  orderby ((float?)residential.Element("TotalArea")) ?? 0 descending
				  select tenant;

		PrintXElementsAsObjects<Tenant>(res);
	}

	private void PrintXElementsAsObjects<T>(IEnumerable<XElement>? elements) where T : IXmlSerializable
	{
		if (elements is null)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			System.Console.WriteLine("Null");
			Console.ForegroundColor = ConsoleColor.White;
			return;
		}

		var resDoc = new XDocument(new XElement($"SerializableListOf{typeof(T).Name}", elements));

		XmlSerializer serializer = new(typeof(SerializableList<T>));

		var res = serializer.Deserialize(resDoc.CreateReader());
		SerializableList<T> objects = res is null ? [] : (SerializableList<T>)res;

		if (objects.Count == 0)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			System.Console.WriteLine("Empty");
			Console.ForegroundColor = ConsoleColor.White;
			return;
		}

		int i = 0;
		foreach (var item in objects)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			System.Console.WriteLine($"\nElement: {i}");
			Console.ForegroundColor = ConsoleColor.Gray;
			System.Console.WriteLine(item);
			i++;
		}

		Console.ForegroundColor = ConsoleColor.White;
	}

	private void PrintSequence<T>(IEnumerable<T>? values)
	{
		if (values is null)
		{
			System.Console.WriteLine("Sequence is Null");
			return;
		}

		int i = 0;
		foreach (var item in values)
		{
			System.Console.WriteLine();
			System.Console.WriteLine($"#{i}:".Blue().Italic());
			System.Console.WriteLine(item);
			i++;
		}
	}
}
