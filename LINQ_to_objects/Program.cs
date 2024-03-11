using System.Reflection;
using System.Security.Cryptography;
using LINQ_to_objects;


(var apartmentHouses, var privateHouses, var tenants) = TestData.GetTestData(1);

void PrintIEnumerable(IEnumerable<object> objects)
{
	foreach (var item in objects)
	{
		System.Console.WriteLine(item);
	}
}

void PrintIEnumerableFormatted<T>(IEnumerable<T> values, Func<T, string> stringFormatter)
{
	foreach (var item in values)
	{
		System.Console.WriteLine(stringFormatter(item));
	}
}

// 1 вивести всіх наймачів
IEnumerable<Tenant> GetAllTenants()
{
	return tenants.Where(tenant => tenant.Address is not null);
}

// 2 вивести всі будівлі
IEnumerable<Building> GetAllBuildings()
{
	IEnumerable<Building> buildings = [];
	return buildings.Concat(privateHouses).Concat(apartmentHouses);
}

// 3 вивести адреси всіх будівель
IEnumerable<Address> GetAllBuldingAddresses()
{
	return from building in GetAllBuildings()
		   select building.Address;
}

// 4 вивести всі квартири
IEnumerable<Apartment> GetAllApartments()
{
	return apartmentHouses.SelectMany(apartmentHouse => apartmentHouse);
}

// 5 вивести всіх наймачів з боргом
IEnumerable<Tenant> GetAllTenantsWithDebt()
{
	return from tenant in GetAllTenants()
		   where tenant.Debt
		   select tenant;
}

// 6 вивести всіх наймачів, що живуть в {n} під'їзді
IEnumerable<Tenant> GetAllTenantsRegisteredAtEntrance(int entranceNumber)
{
	return GetAllTenants().Where(tenant => tenant.Address?.EntranceNumber == entranceNumber);
}

// 7 вивести всіх наймачів, що живуть на вулиці {s}
IEnumerable<Tenant> GetAllTenantsRegisteredAtStreet(string streetName)
{
	return from tenant in GetAllTenants()
		   where tenant.Address?.StreetName == streetName
		   select tenant;
}

// 8 вивести кожне житло (квартири + приватні будинки)
IEnumerable<IResidential> GetAllResidentials()
{
	IEnumerable<IResidential> residentials = [];
	return residentials.Concat(privateHouses).Concat(GetAllApartments());
}

// 9 вивести всіх наймачів, що мають {n} кімнатну квартиру / будинок
IEnumerable<Tenant> GetAllTenantsWithRooms(int roomsCount)
{
	return from threeRoomResidential in
				(from residential in GetAllResidentials()
				 where residential.RoomsCount == roomsCount
				 select residential)
		   join tenant in GetAllTenants() on threeRoomResidential.Address equals tenant.Address
		   select tenant;
}

// 10 вивести кожне житло, наймач якого зареєстрований пізніше між {t}
IEnumerable<IResidential> GetAllResidentialsRegisteredLaterThen(DateOnly date)
{
	var selectedTenants = GetAllTenants().Where(tenant => tenant.RegistrationDate > date);
	
	return from apartment in GetAllApartments()
		   where selectedTenants.Any(tenant => tenant.Address is not null && tenant.Address.Equals(apartment.Address))
		   select apartment;
}

// 11 вивести наймачів, що мають приватний будинок
IEnumerable<Tenant> GetAllTenantsRegisteredAtPrivateHouse()
{
	return from tenant in tenants
		   where privateHouses.Any(house => house.Address.Equals(tenant.Address))
		   select tenant;
}

// 12 вивести наймача, що має {n} поверхове житло
IEnumerable<Tenant> GetAllTenantsWithFloors(int floorsCount)
{
	return GetAllTenants()
	.Join(GetAllResidentials(),
	   tenant => tenant.Address,
	   residential => residential.Address,
	   (tenant, residential) => new { Tenant = tenant, Residential = residential })
	.Where(pair => pair.Residential.FloorsCount == floorsCount)
	.Select(pair => pair.Tenant);
}

// 13 житло, відсортоване по площі
IEnumerable<IResidential> GetResidentialSortedByArea()
{
	return from residential in GetAllResidentials()
		   orderby residential.TotalArea
		   select residential;
}

// 14 вивести наймачів з унікальною фамілією
IEnumerable<Tenant> GetAllTenantsWithUniqLastname()
{
	return GetAllTenants().DistinctBy(tenant => tenant.Lastname);
}

// 15 відсортувати квартири за кількістю поверхів, кімнат та площею
IEnumerable<Apartment> GetApartmentsSortedByFloorsRoomsArea()
{
	return GetAllApartments()
	.OrderByDescending(apartment => apartment.FloorsCount)
	.ThenByDescending(apartment => apartment.RoomsCount)
	.ThenByDescending(apartment => apartment.EffectiveArea);
}

// 16 вивести адреси квартир, згрупованих по кількості кімнат
void PrintApartmentAddressesGrouppedByRoomsCount()
{
	var res = GetAllApartments().GroupBy(apartment => apartment.RoomsCount);

	foreach (var item in res)
	{
		System.Console.WriteLine($"\n\n====================\n\n\t{item.Key} rooms:");
		foreach (var apartment in item)
		{
			System.Console.WriteLine(apartment);
		}
	}
}

// 17 вивести наймач + квартира з будинку {b}
void PrintTenantWithApartmentFromBuildingWithAddress(Address address)
{
	var house = apartmentHouses.First(house => house.Address.Equals(address));

	if (house == null)
		return;

	var res = from apartment in house
			  join tenant in tenants on apartment.Address equals tenant.Address
			  select new { Tenant = tenant, Apartment = apartment };

	foreach (var item in res)
	{
		System.Console.WriteLine("-----------------");
		System.Console.WriteLine(item.Tenant);
		System.Console.WriteLine(item.Apartment);
	}
}

// 18 вивести всіх наймачів, згрупованих по типу квартир, що вони знімають
void PrintTenantsGroupByApartmentType()
{
	var res = GetAllTenants()
	.Join(GetAllApartments(),
	   tenant => tenant.Address,
	   apartment => apartment.Address,
	   (tenant, apartment) => new { Tenant = tenant, ApartmentType = apartment.Type })
	.GroupBy(pair => pair.ApartmentType);

	foreach (var item in res)
	{
		System.Console.WriteLine($"\n\n\tType: {item.Key}\n\n");

		foreach (var tenant in item)
		{
			System.Console.WriteLine(tenant.Tenant);
		}
	}
}

// 19 ПІ наймача + площа житла (включаючи null) + сортування
void PrintTenantAndArea()
{
	var res = from tenant in tenants
			  join residential in GetAllResidentials() on tenant.Address equals residential.Address into temp
			  from t in temp.DefaultIfEmpty()
			  orderby t is null ? 0 : t.TotalArea descending
			  select new { TenantName = tenant.Name, TenantLastName = tenant.Lastname, Area = (t is null) ? "null" : t.TotalArea.ToString() };

	foreach (var item in res)
	{
		System.Console.WriteLine($"{item.TenantName} {item.TenantLastName}, Area: {item.Area}");
	}
}

// 20 вивести ПІ наймача, площу житла та адресу
void PrintTenantAndAreaAndAdrress()
{
	var res = from tenant in tenants
			  join residential in GetAllResidentials() on tenant.Address equals residential.Address
			  select new { TenentName = tenant.Name, TenantLastName = tenant.Lastname, Area = residential.TotalArea, Address = residential.Address };

	foreach (var item in res)
	{
		System.Console.WriteLine($"{item.TenentName} {item.TenantLastName}, Area: {item.Area}, Address: {item.Address}");
	}
}

void ShowMenu()
{
	System.Console.WriteLine("\n\n\n\n");
	string menu = """
	1 - Вивести всіх наймачів
	2 - Вивести всі будівлі
	3 - Вивести адреси всіх будівель
	4 - Вивести всі квартири
	5 - Вивести всіх наймачів з боргом
	6 - Вивести всіх наймачів, що живуть в {2} під'їзді
	7 - Вивести всіх наймачів, що живуть на вулиці {Лісова}
	8 - Вивести кожне житло (квартири + приватні будинки)
	9 - Вивести всіх наймачів, що мають {3} кімнатну квартиру / будинок
	10 - Вивести кожне житло, наймач якого зареєстрований пізніше між 01.01.2019
	11 - Вивести наймачів, що мають приватний будинок
	12 - Вивести наймача, що має {2} поверхове житло
	13 - Вивести житло, відсортоване по площі
	14 - Вивести наймачів з унікальною фамілією
	15 - Відсортувати квартири за кількістю поверхів, кімнат та площею
	16 - Вивести адреси квартир, згрупованих по кількості кімнат
	17 - Вивести наймач + квартира з будинку за адресою Луцьк, Спортивна 57
	18 - Вивести всіх наймачів, згрупованих по типу квартир, що вони знімають
	19 - ПІ наймача + площа житла (включаючи null) + сортування
	20 - ПІ наймача + площа житла + адреса
	
	Вибір: 
	""";	
	System.Console.Write(menu);
	
	string? choise = Console.ReadLine();
	
	if(choise is null)
		return;


	if (int.TryParse(choise, out int variant) == false)
		return;

	switch (variant)
	{
		case 1:
			PrintIEnumerable(GetAllTenants());
			break;
		case 2:
			PrintIEnumerable(GetAllBuildings());
			break;
		case 3:
			PrintIEnumerable(GetAllBuldingAddresses());
			break;
		case 4:
			PrintIEnumerable(GetAllApartments());
			break;
		case 5:
			PrintIEnumerable(GetAllTenantsWithDebt());
			break;
		case 6:
			PrintIEnumerable(GetAllTenantsRegisteredAtEntrance(2));
			break;
		case 7:
			PrintIEnumerable(GetAllTenantsRegisteredAtStreet("Лісова"));
			break;
		case 8:
			PrintIEnumerable(GetAllResidentials());
			break;
		case 9:
			PrintIEnumerable(GetAllTenantsWithRooms(3));
			break;
		case 10:
			PrintIEnumerable(GetAllResidentialsRegisteredLaterThen(new(2019,1,1)));
			break;
		case 11:
			PrintIEnumerable(GetAllTenantsRegisteredAtPrivateHouse());
			break;
		case 12:
			PrintIEnumerable(GetAllTenantsWithFloors(2));
			break;
		case 13:
			PrintIEnumerable(GetResidentialSortedByArea());
			break;
		case 14:
			PrintIEnumerable(GetAllTenantsWithUniqLastname());
			break;
		case 15:
			PrintIEnumerable(GetApartmentsSortedByFloorsRoomsArea());
			break;
		case 16:
			PrintApartmentAddressesGrouppedByRoomsCount();
			break;
		case 17:
			PrintTenantWithApartmentFromBuildingWithAddress(new("Луцьк","Спортивна","57"));
			break;
		case 18:
			PrintTenantsGroupByApartmentType();
			break;
		case 19:
			PrintTenantAndArea();
			break;
		case 20:
			PrintTenantAndAreaAndAdrress();
			break;
		default:
			return;
	}
}

while(true)
	ShowMenu();