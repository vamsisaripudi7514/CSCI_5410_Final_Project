namespace AttendanceSystem.Utility
{
    public class JWTMiddleWare
    {
        private static string HEADER_NAME = "Authorization";
        private readonly RequestDelegate _requestDelegate;

        public JWTMiddleWare(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            var _objEndpoint = context.GetEndpoint();
            if (_objEndpoint != null)
            {
                var _attribute = _objEndpoint.Metadata.OfType<JWTAuthorizeAttribute>();
                if (_attribute.Count() > 0)
                {
                    bool _blHasHeader = context.Request.Headers.ContainsKey(HEADER_NAME);
                    try
                    {
                        context.Items["IsValid"] = false;
                        if (_blHasHeader == true)
                        {
                            string _strToken = context.Request.Headers[HEADER_NAME].FirstOrDefault()?.Split(" ")?.Last() ?? "";
                            if (string.IsNullOrEmpty(_strToken) == false)
                            {
                                context.Items["IsValid"] = JwtHelper.ValidToken(_strToken);
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            await _requestDelegate(context);
        }
    }
}
