namespace Civilization;

public class Plains : Territory, IResourceFactory
{
	public override string Name => "Plains";

	public ResourceRequest ProductionDescription { get; }
	public ResourceStack Storage { get; }

    public UnitUnion AssignedUnits {get;}

    public Plains()
	{
		AssignedUnits = new(this);
		ProductionDescription = new(new Food(), 2);
		Storage = new(new Food());

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
		int producingAmount = ProductionDescription.Amount * (AssignedUnits.GetGroupStats().WorkEfficiency / 100);
		
		Storage.Add(producingAmount);
	}
}
