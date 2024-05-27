namespace Civilization;

public interface IUnitList : IUnitEnumerable
{
	public void Add(Unit unit);
	public void Remove(Unit unit);
}
