using GlobalTradeSevkiyatTakip.Api.Filters;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.DepoDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.RaporDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalTradeSevkiyatTakip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepoDetayController : BaseController
    {
        private readonly IDepoDetayServis manager;
        public DepoDetayController(IDepoDetayServis manager)
        {
            this.manager = manager;
        }

        [HttpGet("Get/{id}"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await manager.GetByIdAsync(id);
            return ListResult(new ApiResponse<DepoDetayListDto>(true, entity));
        }

        [HttpGet("PagedList"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> PagedList([FromQuery] PagedRequestParam pagedParam,int depoId)
        {
            var response = await manager.GetPagedListAsync(pagedParam, depoId);

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
            return ListResult(new ApiResponse<List<DepoDetayListDto>>(true, entity));
        }

        [HttpPost("Add"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Add(DepoDetayInsertDto dto)
        {
            var entity = await manager.AddAsync(dto);
            return AddResult(new ApiResponse<DepoDetayListDto>(true, entity));
        }

      
    }
}
