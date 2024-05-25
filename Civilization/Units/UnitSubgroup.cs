namespace Civilization;

public class UnitSubgroup : UnitEnumerable
{
	private readonly UnitEnumerable _parent;

	public UnitSubgroup(IEnumerable<Unit> units, UnitEnumerable parent) : base(units)
	{
		_parent = parent;
	}

	public UnitSubgroup(IEnumerable<Unit> units, UnitSubgroup parent) : base(units)
	{
		_parent = parent._parent;
	}

	//no OnUnitRemove
	public UnitGroup SeperateFromParent()
	{
		_parent.Remove(Units);

		return new(Units);
	}
}
