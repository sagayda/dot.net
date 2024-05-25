namespace Civilization;

public class Forest : Territory, IResourceFactory
{
	public override string Name => "Forest";

	public ResourceRequest ProductionDescription { get; }
	public ResourceStack Storage { get; }

	public UnitUnion AssignedUnits { get; }

	public Forest()
	{
		AssignedUnits = new(this);
		ProductionDescription = new(new Wood(), 2);
		Storage = new(new Wood());

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
		int producingAmount = ProductionDescription.Amount * (AssignedUnits.GetGroupStats().WorkEfficiency / 100);

		Storage.Add(producingAmount);
	}
}
