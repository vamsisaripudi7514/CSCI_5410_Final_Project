using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace AttendanceSystem.Utility
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JWTAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        //private readonly Enums.UserRole[] allowedroles;
        private readonly string[] allowedroles;
        private static readonly string HEADER_NAME = "Authorization";
        //public JWTAuthorizeAttribute(params Enums.UserRole[] roles)
        public JWTAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                bool _blHasHeader = context.HttpContext.Request.Headers.ContainsKey(HEADER_NAME);
                if (_blHasHeader == false)
                {
                    var problemDetails = new ResponseModel<string>(Hepler.StatusCode.Unauthorized, "Token Required!");
                    context.Result = new ContentResult()
                    {
                        StatusCode = problemDetails.StatusCode,
                        ContentType = "application/json",
                        Content = JsonSerializer.Serialize(problemDetails)
                    };
                }
                else
                {
                    bool _blIsValid = Convert.ToBoolean(context.HttpContext.Items["IsValid"]);
                    if (_blIsValid == false)
                    {
                        var problemDetails = new ResponseModel<string>(Hepler.StatusCode.Unauthorized, "Failed to validate Token");
                        context.Result = new ContentResult()
                        {
                            StatusCode = problemDetails.StatusCode,
                            ContentType = "application/json",
                            Content = JsonSerializer.Serialize(problemDetails)
                        };
                    }
                    else
                    {
                        //string? _strRles = context.HttpContext.User.FindFirstValue(Helper.JWTHelperKeys.strUserRole);
                        //if (string.IsNullOrEmpty(_strRles) == false)
                        //{
                        //    List<string>? _objUserRolelst = JsonSerializer.Deserialize<List<string>>(_strRles);
                        //    if (allowedroles != null && allowedroles.Length > 0 && _objUserRolelst != null && _objUserRolelst.Count > 0)
                        //    {
                        //        if (_objUserRolelst.Any(x => allowedroles.Any(y => y.ToString() == x.ToString())) == false)
                        //        {
                        //            var problemDetails = new ResponseModel<string>((int)Enums.StatusCode.Unauthorized, "User has not permission to access this action");
                        //            context.Result = new ContentResult()
                        //            {
                        //                StatusCode = problemDetails.StatusCode,
                        //                ContentType = "application/json",
                        //                Content = JsonSerializer.Serialize(problemDetails)
                        //            };
                        //        }
                        //    }
                        //}
                    }
                }
            }
            catch (Exception)
            {
                //var problemDetails = new ResponseModel<string>((int)Enums.StatusCode.Unauthorized, exception.Message);
                var problemDetails = new ResponseModel<string>(Hepler.StatusCode.Unauthorized, "Failed to parse token");
                context.Result = new ContentResult()
                {
                    StatusCode = problemDetails.StatusCode,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(problemDetails)
                };
            }
        }
    }
}
