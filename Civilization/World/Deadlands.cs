namespace Civilization;

public class Deadlands : Territory, IResourceFactory
{
	public override string Name => "Deadlands";

	public ResourceRequest ProductionDescription { get; }
	public ResourceStack Storage { get; private set; }

	public UnitUnion AssignedUnits { get; }

	public Deadlands()
	{
		AssignedUnits = new(this);
		ProductionDescription = new(new Fuel(), 5);
		Storage = new(new Fuel());

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
		int producingAmount = ProductionDescription.Amount * (AssignedUnits.GetGroupStats().WorkEfficiency / 100);

		Storage.Add(producingAmount);
	}
}
