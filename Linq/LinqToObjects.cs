using System.Text;

namespace LINQ_to_objects;

public class LinqToObjects
{
	private IEnumerable<Building> _buildings;
	private IEnumerable<Tenant> _tenants;

	public LinqToObjects()
	{
		(_buildings, _tenants) = TestData.GetTestData(1);
	}

	public void Main()
	{
		while(ShowMenu());
	}

	private bool ShowMenu()
	{
		Console.WriteLine("\n\n\n\n");
		string menu = """
		0 - Вихід
		1 - Вивести всіх наймачів
		2 - Вивести адреси всіх будівель
		3 - Вивести всі квартири
		4 - Вивести всіх наймачів з боргом
		5 - Вивести всіх наймачів, що живуть в 2 під'їзді
		6 - Вивести всіх наймачів, що живуть на вулиці Молодіжна
		7 - Вивести кожне житло (квартири + приватні будинки)
		8 - Вивести всіх наймачів, що мають 3 кімнатну квартиру / будинок
		9 - Вивести кожне житло, що зареєстроване за наймачем піжніше ніж 01.01.2019
		10 - Вивести наймачів, що мають приватний будинок
		11 - Вивести житло, відсортоване по площі
		12 - Вивести наймачів з унікальною фамілією
		13 - Вивести відсортоване житло за кількістю поверхів, кімнат та площею
		14 - Вивести житло, згруповане по кількості кімнат 
		15 - Вивести наймач + житло
		16 - Вивести наймач + квартира з будинку за адресою Рівне, Лісова 76
		17 - Вивести всіх наймачів, згрупованих по типу квартир, що вони знімають
		18 - Вивести житло, згруповане по наймачу
		19 - Вивести житло, наймач якого має тільки одне житло
		20 - Вивести наймачів, що реєстрували нове житло з 01.01.2015 до 01.01.2017

		Вибір: 
		""";
		Console.Write(menu);

		string? choise = Console.ReadLine();

		if (choise is null)
			return true;

		if (int.TryParse(choise, out int variant) == false)
			return true;

		switch (variant)
		{
			case 0:
				return false;
			case 1:
				Console.WriteLine(IEnumerableToString(GetAllTenants()));
				break;
			case 2:
				Console.WriteLine(IEnumerableToString(GetAllBuldingAddresses()));
				break;
			case 3:
				Console.WriteLine(IEnumerableToString(GetAllApartments()));
				break;
			case 4:
				Console.WriteLine(IEnumerableToString(GetTenantsWithDebt()));
				break;
			case 5:
				Console.WriteLine(IEnumerableToString(GetTenantsRegisteredAtEntrance(2)));
				break;
			case 6:
				Console.WriteLine(IEnumerableToString(GetTenantsRegisteredAtStreet("Молодіжна")));
				break;
			case 7:
				Console.WriteLine(IEnumerableToString(GetAllResidentials()));
				break;
			case 8:
				Console.WriteLine(IEnumerableToString(GetTenantsWithRooms(3)));
				break;
			case 9:
				Console.WriteLine(IEnumerableToString(GetResidentialsRegisteredLaterThen(new(2019, 01, 01))));
				break;
			case 10:
				Console.WriteLine(IEnumerableToString(GetTenantsRegisteredAtPrivateHouse()));
				break;
			case 11:
				Console.WriteLine(IEnumerableToString(GetResidentialSortedByArea()));
				break;
			case 12:
				Console.WriteLine(IEnumerableToString(GetTenantsWithUniqLastname()));
				break;
			case 13:
				Console.WriteLine(IEnumerableToString(GetResidentialsSortedByFloorsRoomsArea()));
				break;
			case 14:
				Console.WriteLine(IGroupingToString(GetResidentialsGrouppedByRoomsCount()));
				break;
			case 15:
				Console.WriteLine(IEnumerableToString(GetTenantsWithResidential()));
				break;
			case 16:
				Console.WriteLine(IEnumerableToString(GetTenantWithApartmentFromBuildingWithAddress(new BuildingAddress("Ужгород", "Соборна", "36"))));
				break;
			case 17:
				Console.WriteLine(IGroupingToString(GetTenantsGroupByApartmentType()));
				break;
			case 18:
				Console.WriteLine(IGroupingToString(GetResidentialGroupByTenant()));
				break;
			case 19:
				Console.WriteLine(IEnumerableToString(GetResidentialsWithTenantWithOneRegistration()));
				break;
			case 20:
				Console.WriteLine(IEnumerableToString(GetTenantsWhoHasRegistration(new(2015, 1, 1), new(2020, 1, 1))));
				break;
			default:
				return true;
		}
		
		return true;
	}

	//
	// 1 вивести всіх наймачів
	public IEnumerable<Tenant> GetAllTenants()
	{
		return _tenants.Where(tenant => tenant.Any());
	}

	// 2 вивести адреси всіх будівель
	public IEnumerable<Address> GetAllBuldingAddresses()
	{
		return from building in _buildings
			   select building.Address;
	}

	//
	// 3 вивести всі квартири
	public IEnumerable<Apartment> GetAllApartments()
	{
		return from building in _buildings
			   where building is ApartmentHouse
			   from apartment in (ApartmentHouse)building
			   select apartment;
	}

	// 4 вивести всіх наймачів з боргом
	public IEnumerable<Tenant> GetTenantsWithDebt()
	{
		return from tenant in GetAllTenants()
			   where tenant.Debt
			   select tenant;
	}

	//
	// 5 вивести всіх наймачів, що живуть в {n} під'їзді
	public IEnumerable<Tenant> GetTenantsRegisteredAtEntrance(int entranceNumber)
	{
		return GetAllTenants().Where(tenant => tenant.Any(registration => registration.Address is ApartmentAddress address && address.EntranceNumber == entranceNumber));
	}

	//
	// 6 вивести всіх наймачів, що живуть на вулиці {s}
	public IEnumerable<Tenant> GetTenantsRegisteredAtStreet(string streetName)
	{
		return GetAllTenants()
		.Where(tenant => tenant
		.Any(registration => registration.Address.Street == streetName));
	}

	// 7 вивести кожне житло (квартири + приватні будинки)
	public IEnumerable<IResidentialWithAddress> GetAllResidentials()
	{
		var privateResidentials = _buildings.OfType<PrivateHouse>().Select(house => new IResidentialWithAddress(house, house.Address));
		var apartmentResidentials = _buildings.OfType<ApartmentHouse>().SelectMany(apartmentHouse => apartmentHouse).Select(apartment => new IResidentialWithAddress(apartment, apartment.Address)).ToList();

		return privateResidentials.Concat(apartmentResidentials);
	}

	// 8 вивести всіх наймачів, що мають {n} кімнатну квартиру / будинок
	public IEnumerable<Tenant> GetTenantsWithRooms(int roomsCount)
	{
		return from tenant in GetAllTenants()
			   from registration in tenant
			   from residential in GetAllResidentials()
			   where residential.Residential.RoomsCount == roomsCount
			   where residential.Address.Equals(registration.Address)
			   select tenant;
	}

	// 9 вивести кожне житло, що зареєстроване за наймачем піжніше ніж {t}
	public IEnumerable<IResidential> GetResidentialsRegisteredLaterThen(DateTime date)
	{
		return from tenant in GetAllTenants()
			   from registration in tenant
			   from residential in GetAllResidentials()
			   where registration.Date > date
			   where residential.Address.Equals(registration.Address)
			   select residential.Residential;
	}

	// 10 вивести наймачів, що мають приватний будинок
	public IEnumerable<Tenant> GetTenantsRegisteredAtPrivateHouse()
	{
		return from building in _buildings
			   where building is PrivateHouse
			   select building.Address into selectedAddress
			   from tenant in _tenants
			   where tenant.Any(registration => registration.Address == selectedAddress)
			   select tenant;
	}

	// 11 житло, відсортоване по площі
	public IEnumerable<IResidential> GetResidentialSortedByArea()
	{
		return GetAllResidentials().OrderByDescending(record => record.Residential.TotalArea).Select(record => record.Residential);
	}

	// 12 вивести наймачів з унікальною фамілією
	public IEnumerable<Tenant> GetTenantsWithUniqLastname()
	{
		return GetAllTenants().DistinctBy(tenant => tenant.Lastname);
	}

	// 13 відсортувати житло за кількістю поверхів, кімнат та площею
	public IEnumerable<IResidential> GetResidentialsSortedByFloorsRoomsArea()
	{
		return GetAllResidentials()
				.Select(record => record.Residential)
				.OrderByDescending(apartment => apartment.FloorsCount)
				.ThenByDescending(apartment => apartment.RoomsCount)
				.ThenByDescending(apartment => apartment.EffectiveArea);
	}

	// 14 вивести житло, згруповане по кількості кімнат
	public IEnumerable<IGrouping<int, IResidentialWithAddress>> GetResidentialsGrouppedByRoomsCount()
	{
		return from record in GetAllResidentials()
			   orderby record.Residential.RoomsCount
			   group record by record.Residential.RoomsCount;
	}

	// 15 наймач + житло
	public IEnumerable<TenantWithResidential> GetTenantsWithResidential()
	{
		return from tenant in GetAllTenants()
			   from registration in tenant
			   join record in GetAllResidentials() on registration.Address equals record.Address
			   select new TenantWithResidential(tenant, record.Residential);
	}

	// 16 вивести наймач + квартира з будинку {b}
	public IEnumerable<TenantWithResidential> GetTenantWithApartmentFromBuildingWithAddress(Address address)
	{
		if (_buildings.First(building => building.Address.Equals(address) && building is ApartmentHouse) is not ApartmentHouse apartmentHouse)
			return [];

		return from tenant in GetAllTenants()
			   from registration in tenant
			//    join apartment in GetAllApartments() on registration.Address equals apartment.Address
			   join apartment in apartmentHouse on registration.Address equals apartment.Address
			   select new TenantWithResidential(tenant, apartment);
	}

	// 17 вивести всіх наймачів, згрупованих по типу квартир, що вони знімають
	public IEnumerable<IGrouping<ApartmentType, Tenant>> GetTenantsGroupByApartmentType()
	{
		return from tenant in GetAllTenants()
			   from registration in tenant
			   join apartment in GetAllApartments() on registration.Address equals apartment.Address
			   group tenant by apartment.Type;
	}

	// 18 житло, згруповане по наймачу
	public IEnumerable<IGrouping<Tenant, IResidential>> GetResidentialGroupByTenant()
	{
		return from tenant in GetAllTenants()
			   from registration in tenant
			   join record in GetAllResidentials() on registration.Address equals record.Address
			   group record.Residential by tenant;

	}

	// 19 житло, наймач якого має тільки одне житло 
	public IEnumerable<IResidential> GetResidentialsWithTenantWithOneRegistration()
	{
		return from tenant in GetAllTenants()
			   where tenant.Count() == 1
			   from registration in tenant
			   join record in GetAllResidentials() on registration.Address equals record.Address
			   select record.Residential;
	}

	// 20 наймачі, що реєстрували нове житло з {t1} до {t2}
	public IEnumerable<Tenant> GetTenantsWhoHasRegistration(DateTime fromDate, DateTime toDate)
	{
		return GetAllTenants()
			   .Where(tenant => tenant.Any(registration => fromDate < registration.Date && toDate > registration.Date));
	}

	private static string IEnumerableToString<T>(IEnumerable<T> values)
	{
		StringBuilder builder = new();

		foreach (var item in values)
		{
			builder.AppendLine(item?.ToString());
		}

		return builder.ToString();
	}

	private static string IGroupingToString<K, V>(IEnumerable<IGrouping<K, V>> values)
	{
		StringBuilder builder = new();

		foreach (var item in values)
		{
			builder.AppendLine($"===================\nGroup: {item.Key}\n");
			builder.Append(IEnumerableToString(item));
		}

		return builder.ToString();
	}
}
