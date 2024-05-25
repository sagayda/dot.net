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
		
		var forest = (Forest)map.Territories.First(t => t is Forest);
		var plains = (Plains)map.Territories.First(t => t is Plains);
		
		forest.Capture(civilization);
		
		var allUnits = civilization.GroupUnassignedUnits();
		
		var warriors = allUnits.Select(UnitType.Warrior).SeperateFromParent().ToUnion();
		warriors.MoveTo(forest);
		
		var workerUnion1 = allUnits.Select(UnitType.Civilian).Select(5).SeperateFromParent();
		var workerUnion2 = allUnits.Select(UnitType.Civilian).SeperateFromParent();
		
		forest.AssignedUnits.Add(workerUnion1);
		
		civilization.TryBuildAccademy(plains, out var academy);
		
		map.Tick();
		map.Tick();
		map.Tick();
		map.Tick();
		
		
		civilization.CollectResources();
	}
}