namespace SoftwareCatalog.Api.Vendors;

public class Vendor2Entity
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Link { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
}