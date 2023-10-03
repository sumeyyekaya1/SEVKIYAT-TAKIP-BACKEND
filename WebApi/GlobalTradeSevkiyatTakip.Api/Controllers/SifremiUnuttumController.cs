using GlobalTradeSevkiyatTakip.Api.Filters;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Runtime.InteropServices;
using GlobalTradeSevkiyatTakip.Application.Utilities.Security;
using Newtonsoft.Json;
using GlobalTradeSevkiyatTakip.Application.DTOs.SistemDTOs;

namespace GlobalTradeSevkiyatTakip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SifremiUnuttumController : BaseController
    {
        private readonly ISifremiUnuttumServis manager;
        public SifremiUnuttumController(ISifremiUnuttumServis manager)
        {
            this.manager = manager;
        }

        [HttpPost("SifremiUnuttum")] //1.ADIM
        public async Task<IActionResult> SifremiUnuttum(string eposta)
        {
            try
            {
                var Token = await manager.GenerateForgotPasswordToken(eposta, DateTime.Now.AddHours(1));
                string url = $"http://78.189.194.8:4500/SifremiUnuttum/{Token.Token}";
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("noreply@tumteknoloji.com.tr", "Tüm Teknoloji");
                mail.To.Add(eposta);
                mail.Subject = "Şifrenizi Değiştirin";
                mail.IsBodyHtml = true;
                mail.Body = "<h1>Şifrenizi Sıfırlayın</h1>" +
                    "<p>Şifrenizi sıfırlamak için aşağıdaki butonu kullanabilirsiniz." +
                    "Eğer butonu görmüyorsanız aşağıdaki bağlantıya tıklayarak işleminize devam edebilirsiniz. Bağlantının geçerlilik süresi 1 saattir. " +
                    $"{url}<br /><br /><a style=\"background-color:#176bfc;color:white;padding:1rem 2rem;text-decoration:none;\" href=\"{url}\" target=\"_blank\">ŞİFREMİ SIFIRLA</a>";

                System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient();
                sc.Port = 587;
                sc.EnableSsl = true;
                sc.Host = "smtp.yandex.com";
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential("noreply@tumteknoloji.com.tr", "akwfkbrhinqnzqsa");
                sc.Send(mail);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseApiResponse(false, ex.Message));
            }

        }

        [HttpPost("SifreGuncelleTokenKontrol")] //2.ADIM
        public async Task<IActionResult> SifreGuncelleTokenKontrol(string token)
        {
            try
            {
                var Token = EnDeCode.Decrypt(token, SecurityParameters.SifrelemeParametresi);
                var data = JsonConvert.DeserializeObject<UserData>(Token);

                if (!(Convert.ToDateTime(data[SecurityParameters.Expires]) > DateTime.Now))
                {
                    var response = new ApiResponse<string>(false, null, "Token Geçersiz..");
                    return BadRequest(response);
                }
                var userTip = data[SecurityParameters.UserType];
                return Ok(new ApiResponse<string>(true, userTip, "Token Geçerli.."));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseApiResponse(false, ex.Message));
            }

        }

        [HttpPost("SifreGuncelle")] //3.ADIM
        public async Task<IActionResult> SifreGuncelle(SifremiUnuttumDto model, string token)
        {
            try
            {
                string tokenGecerlilik = TokenUtility.getTokenExpirationDate(token);
                if (Convert.ToDateTime(tokenGecerlilik) > DateTime.Now)
                {
                    if (model.YeniParola != model.YeniParolaTekrar)
                        return BadRequest("Parola ile tekrarı aynı değil.");
                    int kullaniciId = TokenUtility.getUserId(token);
                    var resDto = await manager.UpdatePassword(kullaniciId, model);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseApiResponse(false, ex.Message));
            }

        }
    }
}
