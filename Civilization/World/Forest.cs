namespace Civilization;

public class Forest : Territory, IResourceFactory
{
	public override string Name => "Forest";

	public ResourceRequest ProductionDescription { get; }
	public ResourceStack Storage { get; }

	private UnitUnion? _assignedUnits = null;
	public UnitUnion? AssignedUnits
	{
		get
		{
			return _assignedUnits;
		}

		set
		{
			if (value == null)
			{
				_assignedUnits = value;
				return;
			}

			if (value.Owner != Owner)
				return;

			value.MoveTo(this);
			_assignedUnits = value;
		}
	}

	public Forest()
	{
		ProductionDescription = new(Resource.Wood, 2);
		Storage = new(Resource.Wood);

		Stats = new()
		{
			Passability = 0.5f,
			Viewability = 0.1f,
		};
	}

	public override void Tick()
	{
		ProduceResource();
		base.Tick();
	}

	public void ProduceResource()
	{
		int producingAmount = ProductionDescription.Amount * (AssignedUnits?.GetGroupStats().WorkEfficiency / 100) ?? 0;

		Storage.Add(producingAmount);
	}
}
