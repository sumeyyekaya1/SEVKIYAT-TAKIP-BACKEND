using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Application.Utilities.Security;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GlobalTradeSevkiyatTakip.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace GlobalTradeSevkiyatTakip.Api.Filters
{
    public class AuthAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public KullaniciRolEnum Rol { get; set; } = KullaniciRolEnum.Admin;
        public AccessUserTypeEnum AccessUser { get; set; } = AccessUserTypeEnum.Personel;

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

                        if (AccessUser == AccessUserTypeEnum.Personel && data[SecurityParameters.UserType].Equals(SecurityParameters.PersonelType))
                        {
                            var personel = await unit.KullaniciRepo.GetAllAsync().FirstOrDefaultAsync(x => x.Email.Equals(userEmail) && x.ID == userId);
                            if (personel != null)//personeldir
                            {
                                if (Rol == KullaniciRolEnum.Admin || Rol == KullaniciRolEnum.Personel)
                                {
                                    return;
                                }
                                else if (Rol == personel.Rol) //GEREK KALMAYABİLİR TÜM KONTROL BİR ÜSTTE YAPILDI..?
                                    return;
                                else
                                {
                                    context.Result = new UnauthorizedObjectResult(new
                                    {
                                        message = "Bu işlem için yetkiniz yok, yöneticiniz ile iletişime geçiniz..."
                                    });
                                    return;
                                }

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
