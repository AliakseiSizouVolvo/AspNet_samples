namespace NetAcademy.DataBase.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } //topic to discuss
    
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
    public List<Order> Orders { get; set; }
}