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
    public class IrsaliyeController : BaseController
    {
        private readonly IIrsaliyeServis manager;
        public IrsaliyeController(IIrsaliyeServis manager)
        {
            this.manager = manager;
        }

        [HttpGet("Get/{id}"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await manager.GetByIdAsync(id);
            return ListResult(new ApiResponse<IrsaliyeListDto>(true, entity));
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

            response.Message = "irsaliyeler başarılı şekilde listelendi.";
            response.IsSuccess = true;

            return Ok(response);
        }

        [HttpGet("FaturalananPagedList"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> FaturalananPagedList([FromQuery] PagedRequestParam pagedParam)
        {
            var response = await manager.GetPagedListFaturalananAsync(pagedParam);

            if (pagedParam.CurrentPage != 1 && pagedParam.CurrentPage > response.PageCount)
            {
                response.Message = "Sayfa aralık dışında...";
                response.Error = new OverflowException("İstenilen sayfa toplam sayfa sınırları dışına çıktı.");
                response.IsSuccess = false;
                return BadRequest(response);
            }

            response.Message = "irsaliyeler başarılı şekilde listelendi.";
            response.IsSuccess = true;

            return Ok(response);
        }

        [HttpGet("List"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> List()
        {
            var entity = await manager.GetAllAsync();
            return ListResult(new ApiResponse<List<IrsaliyeListDto>>(true, entity));
        }

        [HttpGet("ToplamIrsaliyeAdet"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> ToplamIrsaliyeAdet()
        {
            var entity = await manager.ToplamIrsaliyeAdet();
            return ListResult(new ApiResponse<int>(true, entity));
        }

        [HttpPost("Add"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Add(IrsaliyeViewModelDto dto)
        {
            var entity = await manager.AddAsync(dto);
            return AddResult(new ApiResponse<IrsaliyeListViewModelDto>(true, entity));
        }

        [HttpPut("Update"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Update(IrsaliyeInsertDto dto)
        {
            var entity = await manager.UpdateAsync(dto);
            return UpdateResult(new ApiResponse<IrsaliyeListViewModelDto>(true, entity));
        }


        [HttpDelete("Delete/{id}"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await manager.RemoveAsync(id);
            return DeleteResult(new ApiResponse<IrsaliyeListDto>(true, entity));
        }

    }
}
