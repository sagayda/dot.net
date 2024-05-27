namespace Civilization;

public class Plains : Territory, IResourceFactory
{
	public override string Name => "Plains";

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

    public Plains()
	{
		ProductionDescription = new(Resource.Food, 2);
		Storage = new(Resource.Food);

		Stats = new()
		{
			Passability = 0.9f,
			Viewability = 1f,
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
