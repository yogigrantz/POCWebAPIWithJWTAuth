namespace POCWebAPIJWT.Auth
{
    public interface IAuth
    {
        string? Authenticate(UserDTO userDTO);
        Tuple<bool, string?> Authorize(string token);
    }
}