using GlobalTradeSevkiyatTakip.Api.Filters;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.ParaBirimDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GlobalTradeSevkiyatTakip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParaBirimController : BaseController
    {
        private readonly IParaBirimServis manager;
        public ParaBirimController(IParaBirimServis manager)
        {
            this.manager = manager;
        }

        [HttpGet("List"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> List()
        {
            var entity = await manager.GetAllAsync();
            return ListResult(new ApiResponse<List<ParaBirimListDto>>(true, entity));
        }
    }
}
