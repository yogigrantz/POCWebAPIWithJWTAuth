using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace POCWebAPIJWT.Auth
{
    public class CustomAuth : ActionFilterAttribute
    {
        private readonly IAuth _auth;

        public CustomAuth(IAuth auth)
        {
            this._auth = auth;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                List<string> x = new List<string>();
                foreach (var r in context.HttpContext.Request.Headers)
                {
                    x.Add($"{r.Key}: {r.Value}");
                }
                if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                    context.Result = new RedirectResult("/api/UnAuthorized");
                else
                {
                    string token = context.HttpContext.Request.Headers.Authorization;
                    Tuple<bool, string?> auth = _auth.Authorize(token);
                    if (!auth.Item1)
                        context.Result = new RedirectResult($"/api/UnAuthorized?err={auth.Item2}");
                    else
                    {
                        context.HttpContext.Items.TryAdd("username", auth.Item2);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                context.Result = new RedirectResult($"/api/Error?errMsg={ex.Message}");
            }
        }
    }
}
