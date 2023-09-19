using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

namespace POCWebAPIJWT.Auth;

public class JWToken
{
    private readonly string _claimType;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly string _idType;

    public JWToken(string claimType, string issuer, string auidence, string idType)
    {
        this._claimType = claimType;
        this._issuer = issuer;
        this._audience = auidence;
        this._idType = idType;
    }

    public Tuple<string?, Exception> ReadJwtToken(string jwtToken)
    {
        try
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = (JwtSecurityToken)handler.ReadJwtToken(jwtToken);
            if (token.ValidTo < DateTime.UtcNow)
                return new Tuple<string?, Exception>("Expired", new Exception("Token is expired"));

            var claim = token.Claims.FirstOrDefault(x => x.Type == _claimType);
            if (claim != null)
                return new Tuple<string?, Exception>(claim.Value, null);
            else
                return new Tuple<string?, Exception>(null, null);
        }
        catch (Exception ex)
        {
            return new Tuple<string?, Exception>(null, ex);
        }
    }

    public string IssueJwtToken(string? plainText, int numberOfMinutes)
    {
        if (plainText == null) plainText = "";

        RsaSecurityKey key = null;
        using (RSACryptoServiceProvider keyGen = new RSACryptoServiceProvider(2048))
        {
            key = new RsaSecurityKey(keyGen.ExportParameters(true));
        }

        SigningCredentials sc = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);
        DateTime currDate = DateTime.Now;

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        ClaimsIdentity identity = new ClaimsIdentity(
            new GenericIdentity(_issuer, _idType),
            new[] {new Claim(_claimType, plainText)
            }
        );

        SecurityToken securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = sc,
            Subject = identity,
            Expires = currDate.AddMinutes(numberOfMinutes),
            NotBefore = currDate,
            IssuedAt = currDate
        });

        return handler.WriteToken(securityToken);
    }

}
