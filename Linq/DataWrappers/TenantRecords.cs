namespace LINQ_to_objects;

public record TenantWithResidential(Tenant Tenant, IResidential Residential)
{
	public override string ToString()
	{
		return $"Tenant: {Tenant}\n{Residential}";
	}
}