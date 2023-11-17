
namespace ErpSystem.TruckData.Contracts;

public class TruckDto
{
    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Status { get; private set; }
    public string Description { get; private set; }

    public TruckDto(Guid id, string code, string name, string status,string description)
    {
        Id = id;
        Code = code;
        Name = name;
        Status = status;
        Description = description;
    }

    public TruckDto(string code, string name, string status, string description)
    {
        Code = code;
        Name = name;
        Status = status;
        Description = description;
    }

    public void SetId(Guid id)
    {
        Id = id;
    }
}