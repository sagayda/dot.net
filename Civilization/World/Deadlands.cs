namespace Civilization;

public class Deadlands : Territory, IResourceFactory
{
	public override string Name => "Deadlands";

	public ResourceRequest ProductionDescription { get; }
	public ResourceStack Storage { get; private set; }

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

	public Deadlands()
	{
		ProductionDescription = new(Resource.Fuel, 5);
		Storage = new(Resource.Fuel);

		Stats = new()
		{
			Passability = 0.1f,
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
