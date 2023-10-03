using GlobalTradeSevkiyatTakip.Api.Filters;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.RaporDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.StokDetayDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalTradeSevkiyatTakip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StokDetayController : BaseController
    {
        private readonly IStokDetayServis manager;
        public StokDetayController(IStokDetayServis manager)
        {
            this.manager = manager;
        }

        [HttpGet("Get/{id}"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await manager.GetByIdAsync(id);
            return ListResult(new ApiResponse<StokDetayListDto>(true, entity));
        }

        [HttpGet("PagedList"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> PagedList([FromQuery] PagedRequestParam pagedParam, int depoId)
        {
            var response = await manager.GetPagedListAsync(pagedParam,depoId);

            if (pagedParam.CurrentPage != 1 && pagedParam.CurrentPage > response.PageCount)
            {
                response.Message = "Sayfa aralık dışında...";
                response.Error = new OverflowException("İstenilen sayfa toplam sayfa sınırları dışına çıktı.");
                response.IsSuccess = false;
                return BadRequest(response);
            }

            response.Message = "depolar başarılı şekilde listelendi.";
            response.IsSuccess = true;

            return Ok(response);
        }

        [HttpGet("List"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> List()
        {
            var entity = await manager.GetAllAsync();
            return ListResult(new ApiResponse<List<StokDetayListDto>>(true, entity));
        }

        [HttpGet("ToplamStokAdet"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> ToplamStokAdet()
        {
            var entity = await manager.ToplamStokAdet();
            return ListResult(new ApiResponse<decimal>(true, entity));
        }

        [HttpGet("CreateReport/{depoId}")]
        public async Task<IActionResult> CreateReport(int depoId)
        {
            try
            {
                var response = new ApiResponse<List<DepoRaporDto>>(
                    true,
                    await manager.GetAllReportAsync(depoId),
                    "RAPOR OLUŞTURULDU.");

                var deneme = System.Reflection.Assembly.GetAssembly(typeof(DepoRaporDto)).GetManifestResourceNames();
                var pdfByteArray = await manager.CreateReport(response.Value);
                return File(pdfByteArray, "application/pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(
                  StatusCodes.Status500InternalServerError,
                  new ExceptionApiResponse<StokDetayListDto>("Beklenmeyen bir hata oluştu.", "")
                  );
            }
        }
    }
}
