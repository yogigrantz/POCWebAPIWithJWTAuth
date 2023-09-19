namespace POCWebAPIJWT.Auth;

public class Auth : IAuth
{
    private readonly JWToken _jwt;
    private readonly int _expirationMinutes;

    public Auth(JWToken jwt, int expirationMinutes)
    {
        this._jwt = jwt;
        this._expirationMinutes = expirationMinutes;
    }

    public string? Authenticate(UserDTO userDTO)
    {
        string? rtnToken = null;
        if (Users.SampleUsers.TryGetValue(userDTO.Username, out string? encryptedPwd))
        {
            if (BCrypt.Net.BCrypt.Verify(userDTO.Password, encryptedPwd))
            {
                rtnToken = _jwt.IssueJwtToken(userDTO.Username, _expirationMinutes);
            }
        }
        return rtnToken;
    }

    public Tuple<bool, string?> Authorize(string token)
    {
        var rslt = _jwt.ReadJwtToken(token.Replace("bearer ", "").Replace("Bearer ", ""));
        if (rslt == null)
            return new Tuple<bool, string?>(false, "Token is expired");
        else if (rslt.Item2 != null)
            return new Tuple<bool, string?>(false, rslt.Item2.Message);
        else 
            return new Tuple<bool, string?>(true, rslt.Item1);
    }

}
