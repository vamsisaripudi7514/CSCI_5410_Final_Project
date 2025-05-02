using System.Security.Claims;

namespace AttendanceSystem.Utility
{
    public interface IgetTokenData
    {
        string getTokenDataByContext(string paramClaimName);
    }
    public class getTokenDataFromJWT : IgetTokenData
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public getTokenDataFromJWT(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string getTokenDataByContext(string paramClaimName)
        {
            if (_httpContextAccessor.HttpContext is not null)
            {
                string _strTokenClaim = _httpContextAccessor.HttpContext.User.FindFirstValue(paramClaimName);
                return _strTokenClaim;
            }
            return "";
        }
    }
}
