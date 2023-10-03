using GlobalTradeSevkiyatTakip.Api.Filters;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalTradeSevkiyatTakip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SevkiyatDetayController : BaseController
    {
        private readonly ISevkiyatDetayServis manager;
        public SevkiyatDetayController(ISevkiyatDetayServis manager)
        {
            this.manager = manager;
        }

        [HttpGet("Get/{id}"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await manager.GetByIdAsync(id);
            return ListResult(new ApiResponse<SevkiyatDetayListDto>(true, entity));
        }

        [HttpGet("List"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> List()
        {
            var entity = await manager.GetAllAsync();
            return ListResult(new ApiResponse<List<SevkiyatDetayListDto>>(true, entity));
        }

        [HttpGet("PagedListForIrsaliye"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> PagedListForIrsaliye([FromQuery] PagedRequestParam pagedParam, int sevkiyatId)
        {
            var response = await manager.GetPagedListForIrsaliyeAsync(pagedParam, sevkiyatId);

            if (pagedParam.CurrentPage != 1 && pagedParam.CurrentPage > response.PageCount)
            {
                response.Message = "Sayfa aralık dışında...";
                response.Error = new OverflowException("İstenilen sayfa toplam sayfa sınırları dışına çıktı.");
                response.IsSuccess = false;
                return BadRequest(response);
            }

            response.Message = "sevkiyat detaylar başarılı şekilde listelendi.";
            response.IsSuccess = true;

            return Ok(response);
        }

        [HttpGet("PagedListForFatura"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> PagedListForFatura([FromQuery] PagedRequestParam pagedParam, int sevkiyatId)
        {
            var response = await manager.GetPagedListForFaturaAsync(pagedParam, sevkiyatId);

            if (pagedParam.CurrentPage != 1 && pagedParam.CurrentPage > response.PageCount)
            {
                response.Message = "Sayfa aralık dışında...";
                response.Error = new OverflowException("İstenilen sayfa toplam sayfa sınırları dışına çıktı.");
                response.IsSuccess = false;
                return BadRequest(response);
            }

            response.Message = "sevkiyat detaylar başarılı şekilde listelendi.";
            response.IsSuccess = true;

            return Ok(response);
        }

        [HttpPost("Add"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Add(SevkiyatDetayInsertDto dto)
        {
            var entity = await manager.AddAsync(dto);
            return AddResult(new ApiResponse<SevkiyatDetayListDto>(true, entity));
        }

        [HttpPut("Update"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Update(SevkiyatDetayInsertDto dto)
        {
            var entity = await manager.UpdateAsync(dto);
            return UpdateResult(new ApiResponse<SevkiyatDetayListDto>(true, entity));
        }

        [HttpDelete("Delete/{id}"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await manager.RemoveAsync(id);
            return DeleteResult(new ApiResponse<SevkiyatDetayListDto>(true, entity));
        }
    }
}
