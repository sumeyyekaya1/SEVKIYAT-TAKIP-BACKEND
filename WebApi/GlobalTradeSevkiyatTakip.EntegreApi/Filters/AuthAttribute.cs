using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.Utilities.Security;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GlobalTradeSevkiyatTakip.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace GlobalTradeSevkiyatTakip.EntegreApi.Filters
{
    public class AuthAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public AccessUserTypeEnum AccessUser { get; set; } = AccessUserTypeEnum.Entegre;
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                IUOW unit = context.HttpContext.RequestServices.GetService<IUOW>();

                var authorization = context.HttpContext.Request.Headers.Authorization.ToString();

                if (authorization.Contains("Token") && authorization.StartsWith("Token"))
                {
                    var token = authorization.Substring("Token".Length).Trim();

                    string seriUserData = EnDeCode.Decrypt(token, SecurityParameters.SifrelemeParametresi);

                    var data = JsonConvert.DeserializeObject<UserData>(seriUserData);
                    //expiredate kontrol edilecek..

                    if (Convert.ToDateTime(data[SecurityParameters.Expires]) > DateTime.Now)
                    {

                        int userId = Convert.ToInt32(data[SecurityParameters.UserId]);
                        string userEmail = data[SecurityParameters.EPosta];

                        if (AccessUser == AccessUserTypeEnum.Entegre && data[SecurityParameters.UserType].Equals(SecurityParameters.EntegreType))
                        {
                            var entegre = await unit.EntegreKullaniciRepo.GetAllAsync().FirstOrDefaultAsync(x => x.Email.Equals(userEmail) && x.ID == userId);
                            if (entegre != null)
                            {
                                return;
                            }
                            else
                            {
                                context.Result = new UnauthorizedObjectResult(new
                                {
                                    message = "Lütfen Giriş Yapın.."
                                });
                                return;
                            }
                        }

                    }
                }

                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
            catch { context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden); }
        }
    }
}
