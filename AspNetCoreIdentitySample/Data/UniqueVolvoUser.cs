using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentitySample.Data;

public class UniqueVolvoUser : IdentityUser<Guid>
{
    public string InternalId { get; set; }
}