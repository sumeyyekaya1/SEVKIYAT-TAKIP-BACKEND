using GlobalTradeSevkiyatTakip.Api.Filters;
using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using GlobalTradeSevkiyatTakip.Application.DTOs.AuthDTOs;
using GlobalTradeSevkiyatTakip.Application.DTOs.KullaniciDTOs;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.RequestParameters;
using GlobalTradeSevkiyatTakip.Application.Utilities.Security;
using Microsoft.AspNetCore.Mvc;

namespace GlobalTradeSevkiyatTakip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : BaseController
    {
        private readonly IKullaniciServis manager;
        public KullaniciController(IKullaniciServis manager)
        {
            this.manager = manager;
        }

        [HttpGet("Get/{id}"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await manager.GetByIdAsync(id);
            return ListResult(new ApiResponse<KullaniciListDto>(true, entity));
        }

        [HttpGet("List"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> List()
        {
            var entity = await manager.GetAllAsync();
            return ListResult(new ApiResponse<List<KullaniciListDto>>(true, entity));
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
        public async Task<IActionResult> Add(KullaniciInsertDto dto)
        {
            var entity = await manager.AddAsync(dto);
            return AddResult(new ApiResponse<KullaniciListDto>(true, entity));
        }

        [HttpPut("Update"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Update(KullaniciUpdateDto dto)
        {
            var entity = await manager.UpdateAsync(dto);
            return UpdateResult(new ApiResponse<KullaniciListDto>(true, entity));
        }

        [HttpDelete("Delete/{id}"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await manager.RemoveAsync(id);
            return DeleteResult(new ApiResponse<KullaniciListDto>(true, entity));
        }

        [HttpPut("UpdatePassword"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> UpdatePassword(KullaniciParolaUpdateDto model)
        {
            int personelId = TokenUtility.getUserId(HttpContext.Request.Headers.Authorization.ToString());
            if (personelId != null)
            {
                var resDto = await manager.UpdatePassword(personelId, model);
                var response = new ApiResponse<KullaniciListDto>(true, resDto, "Şifre güncelleme işlemi başarılı");
                return Ok(response);
            }
            else
            {
                return StatusCode(
                     StatusCodes.Status500InternalServerError,
                     new ExceptionApiResponse<KullaniciListDto>("TOKEN BULUNAMADI..", "")
                     );
            }

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginReqDto user)
        {
            try
            {
                var loginResult = await manager.LoginAsyn(user);
                var response = new ApiResponse<UserLoginResDto<KullaniciListDto>>(true, loginResult, "Giriş başarılı");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(
                     StatusCodes.Status500InternalServerError,
                     new ExceptionApiResponse<KullaniciListDto>("Email Yada Parola Hatalı.", ex.Message)
                      );
            }
        }

        [HttpGet("UserBilgi"), Auth(AccessUser = Domain.Enums.AccessUserTypeEnum.Personel, Rol = Domain.Enums.KullaniciRolEnum.Personel)]
        public async Task<IActionResult> UserBilgi()
        {
            int UserId = TokenUtility.getUserId(HttpContext.Request.Headers.Authorization.ToString());
            if (UserId != null)
            {
                var entity = await manager.GetByIdAsync(UserId);
                return GetResult(new ApiResponse<KullaniciListDto>(true, entity));
            }
            else
            {
                return StatusCode(
               StatusCodes.Status500InternalServerError,
               new ExceptionApiResponse<KullaniciListDto>("TOKEN BULUNAMADI..", "")
               );
            }
        }

    }
}
