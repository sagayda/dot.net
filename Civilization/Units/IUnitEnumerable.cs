using System.Collections;

namespace Civilization;

public interface IUnitEnumerable : IEnumerable<Unit>
{
	public UnitStats GetGroupStats();

	public IUnitEnumerable Select(UnitType type);

	public IUnitEnumerable Select(int count);
	
	public IUnitEnumerable Select(Territory location);

	public IUnitEnumerable Select(Predicate<Unit> predicate);
	
	public UnitList ToList();
	
	public UnitUnion ToUnion(Civilization owner);
	
	public UnitListPart AsPart();
}
