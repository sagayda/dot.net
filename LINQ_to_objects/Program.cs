using System.Text;
using LINQ_to_objects;

(var buildings, var tenants) = TestData.GetTestData(1);

string IEnumerableToString<T>(IEnumerable<T> values)
{
	StringBuilder builder = new();

	foreach (var item in values)
	{
		builder.AppendLine(item?.ToString());
	}

	return builder.ToString();
}

string IGroupingToString<K, V>(IEnumerable<IGrouping<K, V>> values)
{
	StringBuilder builder = new();

	foreach (var item in values)
	{
		builder.AppendLine($"===================\nGroup: {item.Key}\n");
		builder.Append(IEnumerableToString(item));
	}

	return builder.ToString();
}

void ShowMenu()
{
	System.Console.WriteLine("\n\n\n\n");
	string menu = """
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
	System.Console.Write(menu);

	string? choise = Console.ReadLine();

	if (choise is null)
		return;


	if (int.TryParse(choise, out int variant) == false)
		return;

	switch (variant)
	{
		case 1:
			System.Console.WriteLine(IEnumerableToString(GetAllTenants()));
			break;
		case 2:
			System.Console.WriteLine(IEnumerableToString(GetAllBuldingAddresses()));
			break;
		case 3:
			System.Console.WriteLine(IEnumerableToString(GetAllApartments()));
			break;
		case 4:
			System.Console.WriteLine(IEnumerableToString(GetTenantsWithDebt()));
			break;
		case 5:
			System.Console.WriteLine(IEnumerableToString(GetTenantsRegisteredAtEntrance(2)));
			break;
		case 6:
			System.Console.WriteLine(IEnumerableToString(GetTenantsRegisteredAtStreet("Молодіжна")));
			break;
		case 7:
			System.Console.WriteLine(IEnumerableToString(GetAllResidentials()));
			break;
		case 8:
			System.Console.WriteLine(IEnumerableToString(GetTenantsWithRooms(3)));
			break;
		case 9:
			System.Console.WriteLine(IEnumerableToString(GetResidentialsRegisteredLaterThen(new(2019,01,01))));
			break;
		case 10:
			System.Console.WriteLine(IEnumerableToString(GetTenantsRegisteredAtPrivateHouse()));
			break;
		case 11:
			System.Console.WriteLine(IEnumerableToString(GetResidentialSortedByArea()));
			break;
		case 12:
			System.Console.WriteLine(IEnumerableToString(GetTenantsWithUniqLastname()));
			break;
		case 13:
			System.Console.WriteLine(IEnumerableToString(GetResidentialsSortedByFloorsRoomsArea()));
			break;
		case 14:
			System.Console.WriteLine(IGroupingToString(GetResidentialsGrouppedByRoomsCount()));
			break;
		case 15:
			System.Console.WriteLine(IEnumerableToString(GetTenantsWithResidential()));
			break;
		case 16:
			System.Console.WriteLine(IEnumerableToString(GetTenantWithApartmentFromBuildingWithAddress(new BuildingAddress("Рівне","Лісова","76"))));
			break;
		case 17:
			System.Console.WriteLine(IGroupingToString(GetTenantsGroupByApartmentType()));
			break;
		case 18:
			System.Console.WriteLine(IGroupingToString(GetResidentialGroupByTenant()));
			break;
		case 19:
			System.Console.WriteLine(IEnumerableToString(GetResidentialsWithTenantWithOneRegistration()));
			break;
		case 20:
			System.Console.WriteLine(IEnumerableToString(GetTenantsWhoHasRegistration(new(2015,1,1),new(2020,1,1))));
			break;
		default:
			return;
	}
}

while(true)
{
	ShowMenu();
}


//
// 1 вивести всіх наймачів
IEnumerable<Tenant> GetAllTenants()
{
	return tenants.Where(tenant => tenant.Any());
}

// 2 вивести адреси всіх будівель
IEnumerable<Address> GetAllBuldingAddresses()
{
	return from building in buildings
		   select building.Address;
}

//
// 3 вивести всі квартири
IEnumerable<Apartment> GetAllApartments()
{
	return from building in buildings
		   where building is ApartmentHouse
		   from apartment in (ApartmentHouse)building
		   select apartment;
}

// 4 вивести всіх наймачів з боргом
IEnumerable<Tenant> GetTenantsWithDebt()
{
	return from tenant in GetAllTenants()
		   where tenant.Debt
		   select tenant;
}

//
// 5 вивести всіх наймачів, що живуть в {n} під'їзді
IEnumerable<Tenant> GetTenantsRegisteredAtEntrance(int entranceNumber)
{
	return GetAllTenants().Where(tenant => tenant.Any(registration => registration.Address is ApartmentAddress address && address.EntranceNumber == entranceNumber));
}

//
// 6 вивести всіх наймачів, що живуть на вулиці {s}
IEnumerable<Tenant> GetTenantsRegisteredAtStreet(string streetName)
{
	return GetAllTenants()
	.Where(tenant => tenant
	.Any(registration => registration.Address.Street == streetName));
}

// 7 вивести кожне житло (квартири + приватні будинки)
IEnumerable<IResidentialWithAddress> GetAllResidentials()
{
	var privateResidentials = buildings.OfType<PrivateHouse>().Select(house => new IResidentialWithAddress(house, house.Address));
	var apartmentResidentials = buildings.OfType<ApartmentHouse>().SelectMany(apartmentHouse => apartmentHouse).Select(apartment => new IResidentialWithAddress(apartment, apartment.Address)).ToList();

	return privateResidentials.Concat(apartmentResidentials);
}

// 8 вивести всіх наймачів, що мають {n} кімнатну квартиру / будинок
IEnumerable<Tenant> GetTenantsWithRooms(int roomsCount)
{
	return from tenant in GetAllTenants()
		   from registration in tenant
		   from residential in GetAllResidentials()
		   where residential.Residential.RoomsCount == roomsCount
		   where residential.Address.Equals(registration.Address)
		   select tenant;
}

// 9 вивести кожне житло, що зареєстроване за наймачем піжніше ніж {t}
IEnumerable<IResidential> GetResidentialsRegisteredLaterThen(DateOnly date)
{
	return from tenant in GetAllTenants()
		   from registration in tenant
		   from residential in GetAllResidentials()
		   where registration.Date > date
		   where residential.Address.Equals(registration.Address)
		   select residential.Residential;
}

// 10 вивести наймачів, що мають приватний будинок
IEnumerable<Tenant> GetTenantsRegisteredAtPrivateHouse()
{
	return from building in buildings
		   where building is PrivateHouse
		   select building.Address into selectedAddress
		   from tenant in tenants
		   where tenant.Any(registration => registration.Address == selectedAddress)
		   select tenant;
}

// 11 житло, відсортоване по площі
IEnumerable<IResidential> GetResidentialSortedByArea()
{
	return GetAllResidentials().OrderByDescending(record => record.Residential.TotalArea).Select(record => record.Residential);
}

// 12 вивести наймачів з унікальною фамілією
IEnumerable<Tenant> GetTenantsWithUniqLastname()
{
	return GetAllTenants().DistinctBy(tenant => tenant.Lastname);
}

// 13 відсортувати житло за кількістю поверхів, кімнат та площею
IEnumerable<IResidential> GetResidentialsSortedByFloorsRoomsArea()
{
	return GetAllResidentials()
			.Select(record => record.Residential)
			.OrderByDescending(apartment => apartment.FloorsCount)
			.ThenByDescending(apartment => apartment.RoomsCount)
			.ThenByDescending(apartment => apartment.EffectiveArea);
}

// 14 вивести житло, згруповане по кількості кімнат
IEnumerable<IGrouping<int, IResidentialWithAddress>> GetResidentialsGrouppedByRoomsCount()
{
	return from record in GetAllResidentials()
		   orderby record.Residential.RoomsCount
		   group record by record.Residential.RoomsCount;
}

// 15 наймач + житло
IEnumerable<TenantWithResidential> GetTenantsWithResidential()
{
	return from tenant in GetAllTenants()
		   from registration in tenant
		   join record in GetAllResidentials() on registration.Address equals record.Address
		   select new TenantWithResidential(tenant, record.Residential);
}

// 16 вивести наймач + квартира з будинку {b}
IEnumerable<TenantWithResidential> GetTenantWithApartmentFromBuildingWithAddress(Address address)
{
	if (buildings.First(building => building.Address.Equals(address) && building is ApartmentHouse) is not ApartmentHouse apartmentHouse)
		return [];

	return from tenant in GetAllTenants()
		   from registration in tenant
		   join apartment in GetAllApartments() on registration.Address equals apartment.Address
		   select new TenantWithResidential(tenant, apartment);
}

// 17 вивести всіх наймачів, згрупованих по типу квартир, що вони знімають
IEnumerable<IGrouping<ApartmentType, Tenant>> GetTenantsGroupByApartmentType()
{
	return from tenant in GetAllTenants()
		   from registration in tenant
		   join apartment in GetAllApartments() on registration.Address equals apartment.Address
		   group tenant by apartment.Type;
}

// 18 житло, згруповане по наймачу
IEnumerable<IGrouping<Tenant, IResidential>> GetResidentialGroupByTenant()
{
	return from tenant in GetAllTenants()
		   from registration in tenant
		   join record in GetAllResidentials() on registration.Address equals record.Address
		   group record.Residential by tenant;

}

// 19 житло, наймач якого має тільки одне житло 
IEnumerable<IResidential> GetResidentialsWithTenantWithOneRegistration()
{
	return from tenant in GetAllTenants()
		   where tenant.Count() == 1
		   from registration in tenant
		   join record in GetAllResidentials() on registration.Address equals record.Address
		   select record.Residential;
}

// 20 наймачі, що реєстрували нове житло з {t1} до {t2}
IEnumerable<Tenant> GetTenantsWhoHasRegistration(DateOnly fromDate, DateOnly toDate)
{
	return GetAllTenants()
		   .Where(tenant => tenant.Any(registration => fromDate < registration.Date && toDate > registration.Date));
}