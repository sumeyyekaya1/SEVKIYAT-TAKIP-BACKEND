using GlobalTradeSevkiyatTakip.Api.Filters;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.IrsaliyeDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using Microsoft.AspNetCore.Mvc;

namespace GlobalTradeSevkiyatTakip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IrsaliyeDetayController : BaseController
    {
        private readonly IIrsaliyeDetayServis manager;
        public IrsaliyeDetayController(IIrsaliyeDetayServis manager)
        {
            this.manager = manager;
        }

        [HttpGet("Get/{id}"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await manager.GetByIdAsync(id);
            return ListResult(new ApiResponse<IrsaliyeDetayListDto>(true, entity));
        }


        [HttpGet("PagedList"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> PagedList([FromQuery] PagedRequestParam pagedParam, int irsaliyeId)
        {
            var response = await manager.GetPagedListAsync(pagedParam, irsaliyeId);

            if (pagedParam.CurrentPage != 1 && pagedParam.CurrentPage > response.PageCount)
            {
                response.Message = "Sayfa aralık dışında...";
                response.Error = new OverflowException("İstenilen sayfa toplam sayfa sınırları dışına çıktı.");
                response.IsSuccess = false;
                return BadRequest(response);
            }

            response.Message = "irsaliye detaylar başarılı şekilde listelendi.";
            response.IsSuccess = true;

            return Ok(response);
        }


        [HttpGet("List"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> List()
        {
            var entity = await manager.GetAllAsync();
            return ListResult(new ApiResponse<List<IrsaliyeDetayListDto>>(true, entity));
        }

        [HttpPost("Add"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Add(IrsaliyeDetayInsertDto dto)
        {
            var entity = await manager.AddAsync(dto);
            return AddResult(new ApiResponse<IrsaliyeDetayListDto>(true, entity));
        }

        [HttpPut("Update"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Update(IrsaliyeDetayInsertDto dto)
        {
            var entity = await manager.UpdateAsync(dto);
            return UpdateResult(new ApiResponse<IrsaliyeDetayListDto>(true, entity));
        }
        [HttpDelete("Delete/{id}"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await manager.RemoveAsync(id);
            return DeleteResult(new ApiResponse<IrsaliyeDetayListDto>(true, entity));
        }
    }
}
