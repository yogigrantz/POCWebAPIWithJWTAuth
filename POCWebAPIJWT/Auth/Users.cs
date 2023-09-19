namespace POCWebAPIJWT.Auth;

public class Users
{
    public static Dictionary<string, string> SampleUsers = new Dictionary<string, string>()
    {
        {"yogi", BCrypt.Net.BCrypt.HashPassword("Password1")},
        {"doggies", BCrypt.Net.BCrypt.HashPassword("Password2") }
    };
}
