using Newtonsoft.Json.Linq;
using Sparc.Blossom.Authentication;

namespace WhatsUpDocx.Users;

public class User : BlossomUser
{
    public string? Name { get; set; }
}
