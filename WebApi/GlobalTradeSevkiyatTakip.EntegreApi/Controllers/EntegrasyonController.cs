using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.AuthDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Utilities.Security;
using GlobalTradeSevkiyatTakip.EntegreApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GlobalTradeSevkiyatTakip.EntegreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntegrasyonController : BaseController
    {
        private readonly IIrsaliyeServis IrsaliyeManager;
        private readonly ICariServis CariManager;
        private readonly IStokDetayServis StokDetayManager;
        private readonly IStokServis StokManager;
        private readonly IEntegreServis EntegreKullaniciManager;
        private readonly IDovizServis DovizManager;
        private readonly IHizmetServis HizmetManager;
        private readonly IFaturaServis FaturaManager;
        public EntegrasyonController(IIrsaliyeServis ırsaliyeManager, 
            ICariServis cariManager, IStokDetayServis stokDetayManager,
            IStokServis stokManager, IEntegreServis entegreKullaniciServis,
            IDovizServis dovizManager, IHizmetServis hizmetManager, 
            IFaturaServis faturaManager)
        {
            IrsaliyeManager = ırsaliyeManager;
            CariManager = cariManager;
            StokDetayManager = stokDetayManager;
            StokManager = stokManager;
            EntegreKullaniciManager = entegreKullaniciServis;
            DovizManager = dovizManager;
            HizmetManager = hizmetManager;
            FaturaManager = faturaManager;
        }

        /// <summary>
        /// Entegreden gelen dataları işlerken sistem timeouta düştüğünde, proje dataları eklemeye devam ediyor ama entegre bir dakika sonra
        /// tekrar çalışıp verileri gönderdiği için, üst üste yığılan datalar olup, tutarsızlık meydana gelir.
        /// Bunu enggellemk için bu endpoint yazıldı, entegre dataları göndermeden önce buradan meşgul oluıp olmadığının bilgisini çekiyor.
        /// İstek geri döndüğünde meşgul olmadığı anlaşılıp o an için entegreden o datalar projeye gönderilmiyor
        /// </summary>
        /// <returns></returns>
        [HttpPost("IsBusy")]
        public IActionResult IsBusy()
        {
            return Ok();
        }

        [HttpGet("SendYeniIrsaliye"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Entegre)]
        public async Task<IActionResult> SendYeniIrsaliye()
        {
            var entity = await IrsaliyeManager.SendWolvoxYeniIrsaliye();
            return ListResult(new ApiResponse<List<IrsaliyeEntegreViewModelDto>>(true, entity));
        }

        [HttpGet("FaturalanmamisIrsaliye"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Entegre)]
        public async Task<IActionResult> FaturalanmamisIrsaliye()
        {
            var entity = await IrsaliyeManager.FaturalanmayanIrsaliyeListAsync();
            return ListResult(new ApiResponse<List<EntegreResDto>>(true, entity));
        }

        [HttpPost("UpdateBlKoduIrsaliye"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Entegre)]
        public async Task<IActionResult> UpdateBlKoduIrsaliye(List<BlKoduUpdateDto> dto)
        {
            await IrsaliyeManager.UpdateWolvoxBlKodu(dto);
            return Ok();
        }

        [HttpPost("AddWolvoxCari"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Entegre)]
        public async Task<IActionResult> AddWolvoxCari(List<CariEntegreDto> dto)
        {
            try
            {
                var entity = await CariManager.AddWolvoxAsync(dto);
                return AddResult(new ApiResponse<List<EntegreResDto>>(true, entity));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseApiResponse(false, "Cari Eklerken Sorun Oluştu"));
            }
        }

        [HttpPost("AddWolvoxHizmet"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Entegre)]
        public async Task<IActionResult> AddWolvoxHizmet(List<HizmetEntegreDto> dto)
        {
            try
            {
                var entity = await HizmetManager.AddWolvoxAsync(dto);
                return AddResult(new ApiResponse<List<EntegreResDto>>(true, entity));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseApiResponse(false, "Hizmet Eklerken Sorun Oluştu"));
            }
        }

        [HttpPost("AddWolvoxFatura"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Entegre)]
        public async Task<IActionResult> AddWolvoxFatura(List<FaturaEntegreViewModelDto> dto)
        {
            try
            {
                var entity = await FaturaManager.AddWolvoxAsync(dto);
                return AddResult(new ApiResponse<List<EntegreResDto>>(true, entity));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseApiResponse(false, "Fatura Eklerken Sorun Oluştu"));
            }
        }

        [HttpPost("AddWolvoxDoviz"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Entegre)]
        public async Task<IActionResult> AddWolvoxDoviz(List<DovizEntegreDto> dto)
        {
            try
            {
                var entity = await DovizManager.AddWolvoxAsync(dto);
                return AddResult(new ApiResponse<List<EntegreResDto>>(true, entity));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseApiResponse(false, "Döviz Eklerken Sorun Oluştu"));
            }
        }

        [HttpPost("AddWolvoxStokHareket"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Entegre)]
        public async Task<IActionResult> AddWolvoxStokHareket(List<StokDetayEntegreDto> dto)
        {
            try
            {
                var entity = await StokDetayManager.AddWolvoxAsync(dto);
                return AddResult(new ApiResponse<List<EntegreResDto>>(true, entity));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseApiResponse(false, "Stok Hareket Eklerken Sorun Oluştu"));
            }
        }

        [HttpPost("AddWolvoxStok"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Entegre)]
        public async Task<IActionResult> AddWolvoxStok(List<StokEntegreDto> dto)
        {

            try
            {
                var entity = await StokManager.AddWolvoxAsync(dto);
                return AddResult(new ApiResponse<List<EntegreResDto>>(true, entity));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseApiResponse(false, "Stok Eklerken Sorun Oluştu"));
            }
        }

        [HttpPost("AddWolvoxIrsaliye"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Entegre)]
        public async Task<IActionResult> AddWolvoxIrsaliye(List<IrsaliyeEntegreViewModelDto> dto)
        {
            try
            {
                var entity = await IrsaliyeManager.AddWolvoxAsync(dto);
                return AddResult(new ApiResponse<List<EntegreResDto>>(true, entity));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseApiResponse(false, "İrsaliye Eklerken Sorun Oluştu"));
            }
        }

        [HttpPost("TokenControl"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Entegre)]
        public async Task<IActionResult> TokenControl()
        {
            try
            {
                string token = HttpContext.Request.Headers.Authorization.ToString();
                int userId = TokenUtility.getUserId(token);
                string userType = TokenUtility.getUserType(token);
                if (userType == SecurityParameters.EntegreType)
                {
                    var loginResult = await EntegreKullaniciManager.TokenExpiredControl(token);
                    var response = new ApiResponse<UserLoginResDto<EntegreKullaniciResDto>>(true, loginResult, "Giriş başarılı");
                    return Ok(response);
                }
                throw new Exception("Geçersiz Kullanıcı Tipi..");
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseApiResponse(false, ex.Message));
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(EntegreLoginDto user)
        {
            try
            {
                var loginResult = await EntegreKullaniciManager.LoginAsyn(user);
                var response = new ApiResponse<UserLoginResDto<EntegreKullaniciResDto>>(true, loginResult, "Giriş başarılı");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseApiResponse(false, ex.Message));
            }

        }
    }
}
