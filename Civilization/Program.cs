using System.Drawing;
using ANSIConsole;
using Civilization;

internal class Program
{
	private static void Main(string[] args)
	{
		MapBuilder mapBuilder = new();
		mapBuilder.SetSize(20).SetDeadlandsVolume(2).SetForestVolume(5).SetPlainsVolume(3);

		Singleton<WorldMap>.Instance = mapBuilder.Build();

		var map = Singleton<WorldMap>.Instance;

		HumanCivilization civilization = map.CreateHumans();

		LogTime(map.CurrentTick);
		LogResources(civilization.Wallet);
		LogUnitEnumerable(civilization.UnassignedUnits);
		LogUnions(civilization.UnitsUnions);
		LogBuilding(civilization.Buildings);

		var spawn = civilization.Base;
		var forest = (Forest)map.Territories.First(t => t is Forest);
		var plains = (Plains)map.Territories.First(t => t is Plains);

		forest.Capture(civilization);

		var allUnits = civilization.UnassignedUnits;

		var warriors = civilization.CreateUnion(allUnits.Select(UnitType.Warrior).AsPart().Crop(), "Swordsman 1");
		warriors.MoveTo(forest);

		var workerUnion1 = civilization.CreateUnion(allUnits.Select(UnitType.Civilian).Select(5).AsPart(), "Worker 1");
		var workerUnion2 = civilization.CreateUnion(allUnits.Select(UnitType.Civilian), "Worker 2");

		forest.AssignedUnits = workerUnion2;

		civilization.TryBuildAccademy(plains, out var academy);

		LogTime(map.CurrentTick);
		LogResources(civilization.Wallet);
		LogUnitEnumerable(civilization.UnassignedUnits);
		LogUnions(civilization.UnitsUnions);
		LogBuilding(civilization.Buildings);

		map.Tick();
		academy.Create(civilization.Wallet, civilization);
		map.Tick();
		academy.Create(civilization.Wallet, civilization);
		map.Tick();
		academy.Create(civilization.Wallet, civilization);
		map.Tick();

		LogTime(map.CurrentTick);
		LogResources(civilization.Wallet);
		LogUnitEnumerable(civilization.UnassignedUnits);
		LogUnions(civilization.UnitsUnions);
		LogBuilding(civilization.Buildings);

		civilization.CollectResources();
		civilization.CollectTrainedUnits();

		LogTime(map.CurrentTick);
		LogResources(civilization.Wallet);
		LogUnitEnumerable(civilization.UnassignedUnits);
		LogUnions(civilization.UnitsUnions);
		LogBuilding(civilization.Buildings);

	}

	private static void LogResources(ResourceWallet wallet)
	{
		System.Console.WriteLine($"Resources:".Faint().Italic());

		System.Console.WriteLine($"\tFood = ".Faint() + $"{wallet[Resource.Food].Amount}".Color(Color.Yellow).ToString());
		System.Console.WriteLine($"\tWood = ".Faint() + $"{wallet[Resource.Wood].Amount}".Color(Color.Yellow).ToString());
		System.Console.WriteLine($"\tIron = ".Faint() + $"{wallet[Resource.Iron].Amount}".Color(Color.Yellow).ToString());
		System.Console.WriteLine($"\tFuel = ".Faint() + $"{wallet[Resource.Fuel].Amount}".Color(Color.Yellow).ToString());
		System.Console.WriteLine($"\tGold = ".Faint() + $"{wallet[Resource.Gold].Amount}".Color(Color.Yellow).ToString());
		System.Console.WriteLine($"\tPopulation = ".Faint() + $"{wallet[Resource.Civilians].Amount}".Color(Color.Yellow).ToString());
		System.Console.WriteLine();
	}

	private static void LogTime(int tick)
	{
		System.Console.WriteLine("\n\n============\n");
		System.Console.Write($"Currnent time = ".Italic().Bold());
		System.Console.Write($"{tick}".Color(Color.Green));
		System.Console.WriteLine();
		System.Console.WriteLine();
	}

	private static void LogUnions(IEnumerable<UnitUnion> unions)
	{
		foreach (var union in unions)
		{
			System.Console.WriteLine($"Union '{union.Name}':".Faint().Italic());

			foreach (var unit in union)
			{
				System.Console.WriteLine($"\t{unit.Race.Name} {unit.Name}, {unit.Location.Name}");
			}
			System.Console.WriteLine();
		}
	}

	private static void LogBuilding(IEnumerable<Building> buildings)
	{
		if (buildings.Any() == false)
			return;

		System.Console.WriteLine($"Buildings:".Faint().Bold());
		foreach (var building in buildings)
		{
			System.Console.Write($"\t{building.Name}, {building.Location.Name}");

			if (building is IUnitFactory factory)
			{
				System.Console.Write($", trained units = {factory.TrainedUnits.Count()}");
			}
		}
	}

	private static void LogUnitEnumerable(IUnitEnumerable units)
	{
		System.Console.WriteLine($"Unassigned units:".Faint().Italic());

		foreach (var unit in units)
		{
			System.Console.WriteLine($"\t{unit.Race.Name} {unit.Name}, {unit.Location.Name}");
		}
		System.Console.WriteLine();
	}
}