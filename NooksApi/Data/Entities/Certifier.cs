namespace NooksApi.Data.Entities;

public class Certifier
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string? Website { get; set; }
    public bool IsActive { get; set; } = true;
}
