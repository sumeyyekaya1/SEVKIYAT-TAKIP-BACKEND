using GlobalTradeSevkiyatTakip.Api.Filters;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.CariDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.SevkiyatNotDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using GlobalTradeSevkiyatTakip.Application.Utilities.Security;
using Microsoft.AspNetCore.Mvc;

namespace GlobalTradeSevkiyatTakip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SevkiyatNotController : BaseController
    {
        private readonly ISevkiyatNotServis manager;
        public SevkiyatNotController(ISevkiyatNotServis manager)
        {
            this.manager = manager;
        }

        [HttpGet("Get/{id}"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await manager.GetByIdAsync(id);
            return ListResult(new ApiResponse<SevkiyatNotInsertResDto>(true, entity));
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

            response.Message = "cariler başarılı şekilde listelendi.";
            response.IsSuccess = true;

            return Ok(response);
        }

        [HttpPost("Add"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Add(SevkiyatNotInsertReqDto dto)
        {
            int kullaniciId = TokenUtility.getUserId(HttpContext.Request.Headers.Authorization.ToString());
            var entity = await manager.AddAsync(dto,kullaniciId);
            return AddResult(new ApiResponse<SevkiyatNotInsertResDto>(true, entity));
        }

        [HttpPut("Update"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Update(SevkiyatNotInsertReqDto dto)
        {
            var entity = await manager.UpdateAsync(dto);
            return UpdateResult(new ApiResponse<SevkiyatNotInsertResDto>(true, entity));
        }

        [HttpDelete("Delete/{id}"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await manager.RemoveAsync(id);
            return DeleteResult(new ApiResponse<SevkiyatNotInsertResDto>(true, entity));
        }
    }
}
