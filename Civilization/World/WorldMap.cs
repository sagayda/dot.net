using System.Collections.ObjectModel;

namespace Civilization;

public partial class WorldMap : ITickable
{
	private readonly Territory[] _territories;
	private readonly List<Civilization> _civilizations;
	
	public ReadOnlyCollection<Territory> Territories => _territories.AsReadOnly();
	
	public WorldMap(Territory[] territories)
	{
		_territories = territories;
		_civilizations = [];
	}
	
	public Territory[] GetTerritoriesFor(Civilization civilization)
	{
		return _territories.Where(t => t.Owner == civilization).ToArray();
		
		// return _territories.Where(t => t.Units.Any(u => u.Owner == civilization)).ToArray();
	}
	
	public HumanCivilization CreateHumans()
	{
		var plains = _territories.First(t => t is Plains);
		
		HumanCivilization civilization = new(plains);
		_civilizations.Add(civilization);
		
		return civilization;
	}

	public void Tick()
	{
		foreach (var territory in _territories)
			territory.Tick();

		foreach (var civilization in _civilizations)
			civilization.Tick();
	}
}
