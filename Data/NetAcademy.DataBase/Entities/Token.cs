namespace NetAcademy.DataBase.Entities;

public class Token
{
    public Guid TokenId { get; set; }
    
    public string? DeviceName { get; set; }
    
    public DateTime ExpireDate { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}