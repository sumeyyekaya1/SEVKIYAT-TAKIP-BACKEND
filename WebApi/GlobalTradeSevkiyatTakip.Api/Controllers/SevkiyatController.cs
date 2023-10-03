using GlobalTradeSevkiyatTakip.Api.Filters;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.RaporDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalTradeSevkiyatTakip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SevkiyatController : BaseController
    {
        private readonly ISevkiyatServis manager;
        public SevkiyatController(ISevkiyatServis manager)
        {
            this.manager = manager;
        }

        [HttpGet("Get/{id}"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await manager.GetByIdAsync(id);
            return ListResult(new ApiResponse<SevkiyatListDto>(true, entity));
        }

        [HttpGet("List"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> List()
        {
            var entity = await manager.GetAllAsync();
            return ListResult(new ApiResponse<List<SevkiyatListDto>>(true, entity));
        }


        [HttpGet("AylikSevkiyatAdet"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> AylikSevkiyatAdet()
        {
            var entity = await manager.AylikSevkiyatAdet();
            return ListResult(new ApiResponse<List<int>>(true, entity));
        }


        [HttpGet("ToplamSevkiyatAdet"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> ToplamSevkiyatAdet()
        {
            var entity = await manager.ToplamSevkiyatAdet();
            return ListResult(new ApiResponse<int>(true, entity));
        }

        [HttpGet("PagedList"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> PagedList([FromQuery] PagedRequestParam pagedParam)
        {
            var response = await manager.GetPagedListAsync(pagedParam);

            if (pagedParam.CurrentPage != 1 && pagedParam.CurrentPage > response.PageCount)
            {
                response.Message = "Sayfa aralık dışında...";
                response.Error = new OverflowException("İstenilen sayfa toplam sayfa sınırları dışına çıktı.");
                response.IsSuccess = false;
                return BadRequest(response);
            }

            response.Message = "sevkiyatlar başarılı şekilde listelendi.";
            response.IsSuccess = true;

            return Ok(response);
        }

        [HttpPost("Add"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Add(SevkiyatInsertDto dto)
        {
            var entity = await manager.AddAsync(dto);
            return AddResult(new ApiResponse<SevkiyatListDto>(true, entity));
        }

        [HttpGet("DovizKurDonusum"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> DovizKurDonusum(int sevkiyatId, int paraBirimId)
        {
            var entity = await manager.DovizBirimDonusum(sevkiyatId, paraBirimId);
            return AddResult(new ApiResponse<SevkiyatKurDonusumDto>(true, entity));
        }

        [HttpPut("Update"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Update(SevkiyatInsertDto dto)
        {
            var entity = await manager.UpdateAsync(dto);
            return UpdateResult(new ApiResponse<SevkiyatListDto>(true, entity));
        }

        [HttpDelete("Delete/{id}"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await manager.RemoveAsync(id);
            return DeleteResult(new ApiResponse<SevkiyatListDto>(true, entity));
        }

        [HttpGet("CreateReport/{sevkiyatId}")]
        public async Task<IActionResult> CreateReport(int sevkiyatId)
        {
            try
            {
                var response = new ApiResponse<List<SevkiyatCekiListeRaporDto>>(
                    true,
                    await manager.GetAllReportAsync(sevkiyatId),
                    "RAPOR OLUŞTURULDU.");

                var deneme = System.Reflection.Assembly.GetAssembly(typeof(SevkiyatCekiListeRaporDto)).GetManifestResourceNames();
                var pdfByteArray = await manager.CreateReport(response.Value);
                return File(pdfByteArray, "application/pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(
                  StatusCodes.Status500InternalServerError,
                  new ExceptionApiResponse<SevkiyatInsertDto>("Beklenmeyen bir hata oluştu.", "")
                  );
            }
        }

        [HttpGet("CreateReportForDonem/{baslangicTarih}/{bitisTarih}")]
        public async Task<IActionResult> CreateReportForDonem(DateTime baslangicTarih, DateTime bitisTarih)
        {
            try
            {
                var response = new ApiResponse<List<SevkiyatMaliyetRaporDto>>(
                    true,
                    await manager.DonemSevkiyatReportAsync(baslangicTarih, bitisTarih),
                    "RAPOR OLUŞTURULDU.");

                var deneme = System.Reflection.Assembly.GetAssembly(typeof(SevkiyatMaliyetRaporDto)).GetManifestResourceNames();
                var pdfByteArray = await manager.CreateReportForMaliyet(response.Value);
                return File(pdfByteArray, "application/pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(
                  StatusCodes.Status500InternalServerError,
                  new ExceptionApiResponse<SevkiyatInsertDto>("Beklenmeyen bir hata oluştu.", "")
                  );
            }
        }

        [HttpGet("CreateReportForMaliyet/{sevkiyatId}")]
        public async Task<IActionResult> CreateReportForMaliyet(int sevkiyatId)
        {
            try
            {
                var response = new ApiResponse<List<SevkiyatMaliyetRaporDto>>(
                    true,
                    await manager.ReportForMaliyetAsync(sevkiyatId),
                    "RAPOR OLUŞTURULDU.");

                var deneme = System.Reflection.Assembly.GetAssembly(typeof(SevkiyatMaliyetRaporDto)).GetManifestResourceNames();
                var pdfByteArray = await manager.CreateReportForMaliyet(response.Value);
                return File(pdfByteArray, "application/pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(
                  StatusCodes.Status500InternalServerError,
                  new ExceptionApiResponse<SevkiyatInsertDto>("Beklenmeyen bir hata oluştu.", "")
                  );
            }
        }
    }
}
