namespace LINQ_to_objects;

public static class TestData
{
	public static int Seed
	{
		get
		{
			return _seed;
		}
		set
		{
			_seed = value;
			_random = new(_seed);
		}
	}

	private static int _seed = 0;
	private static Random _random = new(_seed);

	private static readonly string[] _names =
	["Андрій", "Олександр", "Максим", "Іван", "Дмитро",
	"Тетяна", "Олена", "Юлія", "Катерина", "Наталія",
	"Ігор", "Сергій", "Віталій", "Олексій", "Євген",
	"Анна", "Вікторія", "Ірина", "Оксана", "Світлана"];

	private static readonly string[] _lastnames =
	["Шевченко", "Петренко", "Іващенко", "Коваленко", "Григоренко",
	"Бойко", "Левченко", "Кравченко", "Пономаренко", "Смирнов",
	"Зайченко", "Степаненко", "Гончаренко", "Онищенко", "Ткаченко",
	"Мельник", "Яковенко", "Франчук", "Осадчук", "Василенко"];

	private static readonly string[] _middlenames =
	["Миколайович", "Петрович", "Іванович", "Дмитрович",
	"Анатолійович", "Сергійович", "Віталійович", "Олександрович",
	"Євгенович", "Юрійович", "Васильович", "Олегович",
	"Ігорович", "Русланович", "Андрійович", "Богданович",
	"Володимирович", "Ярославович", "Максимович", "Едуардович"];

	private static readonly string[] _cities =
	[ "Київ", "Львів", "Харків", "Одеса", "Дніпро",
	"Донецьк", "Запоріжжя", "Кривий Ріг", "Миколаїв", "Маріуполь",
	"Вінниця", "Луцьк", "Хмельницький", "Рівне", "Житомир",
	"Чернівці", "Івано-Франківськ", "Тернопіль", "Ужгород", "Черкаси"];

	private static readonly string[] _streets =
	["Соборна", "Хрещатик", "Французька", "Шевченка", "Лесі Українки",
	"Незалежності", "Першотравнева", "Перемоги", "Миру", "Гагаріна",
	"Садова", "Паркова", "Спортивна", "Молодіжна", "Шкільна",
	"Лісова", "Набережна", "Космічна", "Промислова", "Заводська"];

	public static string GetName()
	{
		return _names[_random.Next(0, _names.Length - 1)];
	}

	public static string GetLastname()
	{
		return _lastnames[_random.Next(0, _lastnames.Length - 1)];
	}

	public static string GetMiddlename()
	{
		return _middlenames[_random.Next(0, _middlenames.Length - 1)];
	}

	public static BuildingAddress GetBuildingAddress()
	{
		return new(_cities[_random.Next(0, _cities.Length - 1)],
			 _streets[_random.Next(0, _streets.Length - 1)],
			 _random.Next(0, 100).ToString());
	}

	public static List<Apartment> GetApartments(BuildingAddress apartmentHouseAddress, int count)
	{
		int entrancesNumber = _random.Next(1, 5);
		int apartmentsInEntrance = (int)Math.Ceiling((float)count / entrancesNumber);

		List<Apartment> apartments = [];
		for (int i = 1; i <= count; i++)
		{
			ApartmentAddress apartmentAddress = new(apartmentHouseAddress.City, apartmentHouseAddress.Street, apartmentHouseAddress.Number, i, (int)Math.Ceiling(i / (float)apartmentsInEntrance));

			int area = _random.Next(20, 150);
			float effectiveArea = _random.Next(area - area / 2, area - area / 4);

			int roomsCount;
			int floorsCount = 1;

			if (area < 30)
			{
				roomsCount = 1;
			}
			else if (area < 50)
			{
				roomsCount = 2;
			}
			else if (area < 80)
			{
				roomsCount = 3;
			}
			else if (area < 120)
			{
				roomsCount = 4;
			}
			else
			{
				roomsCount = 5;
				floorsCount = 2;
			}

			apartments.Add(new Apartment(apartmentAddress,
								(ApartmentType)_random.Next(0, Enum.GetValues(typeof(ApartmentType)).Length),
								area,
								effectiveArea,
								roomsCount,
								floorsCount));
		}

		return apartments;
	}

	public static ApartmentHouse GetApartmentHouse(int apartmentsCount)
	{
		BuildingAddress address = GetBuildingAddress();
		ApartmentHouse house = new(address, (ApartmentHouseType)_random.Next(0, Enum.GetValues(typeof(ApartmentHouseType)).Length));

		house.Add(GetApartments(address, apartmentsCount));

		return house;
	}

	public static PrivateHouse GetPrivateHouse()
	{
		BuildingAddress address = GetBuildingAddress();

		int area = _random.Next(20, 300);
		float effectiveArea = _random.Next(area - area / 2, area - area / 4);

		int roomsCount;
		int floorsCount = 1;

		if (area < 30)
		{
			roomsCount = 2;
		}
		else if (area < 80)
		{
			roomsCount = 3;
		}
		else if (area < 120)
		{
			roomsCount = 3;
		}
		else if (area < 180)
		{
			floorsCount = 2;
			roomsCount = 4;
		}
		else if (area < 250)
		{
			floorsCount = 2;
			roomsCount = 5;
		}
		else
		{
			roomsCount = 6;
			floorsCount = 3;
		}

		return new PrivateHouse(address,
						  (PrivateHouseType)_random.Next(0, Enum.GetValues(typeof(PrivateHouseType)).Length),
						  area,
						  effectiveArea,
						  roomsCount,
						  floorsCount);
	}

	public static Tenant GetTenant()
	{
		return new Tenant(GetName(), GetLastname(), GetMiddlename(), _random.Next(0, 5), _random.Next(0, 3), _random.Next(0, 100) > 80);
	}

	public static (IEnumerable<Building> Buildings, IEnumerable<Tenant> Tenants) GetTestData(float volume)
	{
		int apartmentHousesCount = (int)Math.Ceiling(volume * 3);
		int privateHousesCount = (int)Math.Ceiling(volume * 5);

		List<ApartmentHouse> apartmentHouses = [];
		List<PrivateHouse> privateHouses = [];
		List<Tenant> tenants = [];

		for (int i = 0; i < apartmentHousesCount; i++)
		{
			apartmentHouses.Add(GetApartmentHouse((int)Math.Ceiling((i + 1) * 10 * volume)));
		}

		for (int i = 0; i < privateHousesCount; i++)
		{
			privateHouses.Add(GetPrivateHouse());
		}

		for (int i = 0; i < (int)Math.Ceiling(volume * 3); i++)
		{
			tenants.Add(GetTenant());
		}

		var addresses = apartmentHouses.SelectMany(apartmentHouse => apartmentHouse.Select(apartment => apartment.Address as Address)).Concat(privateHouses.Select(house => house.Address)).OrderBy(address => _random.Next(0, 1024));

		Tenant currentTenant = GetTenant();
		foreach (var address in addresses)
		{
			currentTenant.Add(new(new DateTime(_random.Next(2000, 2024), _random.Next(1, 13), _random.Next(1, 29)), address));

			if (_random.Next(0, 3) != 0)
			{
				tenants.Add(currentTenant);
				currentTenant = GetTenant();
			}
		}

		IEnumerable<Building> buildings = apartmentHouses;
		buildings = buildings.Concat(privateHouses);

		return (buildings, tenants);
	}
}
