using GlobalTradeSevkiyatTakip.Application.ApiResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalTradeSevkiyatTakip.Api.Controllers
{
  
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult ListResult<T>(ApiResponse<T> response)
        {
            try
            {
                var result2 = new ApiResponse<T>(response.IsSuccess, response.Value, "Listeleme Başarılı");
                return Ok(result2);
            }
            catch (Exception ex)
            {
                return StatusCode(
                StatusCodes.Status500InternalServerError,
                new ExceptionApiResponse<T>("Beklenmeyen bir hata oluştu.", ex.Message)
                );
            }

        }

        [NonAction]
        public IActionResult GetResult<T>(ApiResponse<T> response)
        {
            try
            {
                var result2 = new ApiResponse<T>(response.IsSuccess, response.Value, "Get Başarılı");
                return Ok(result2);
            }
            catch (Exception ex)
            {
                return StatusCode(
                 StatusCodes.Status500InternalServerError,
                 new ExceptionApiResponse<T>("Beklenmeyen bir hata oluştu.", ex.Message)
                 );
            }

        }

        [NonAction]
        public IActionResult AddResult<T>(ApiResponse<T> response)
        {
            try
            {
                var result2 = new ApiResponse<T>(response.IsSuccess, response.Value, "Ekleme Başarılı");
                return Ok(result2);
            }
            catch (Exception ex)
            {
                return StatusCode(
                 StatusCodes.Status500InternalServerError,
                 new ExceptionApiResponse<T>("Beklenmeyen bir hata oluştu.", ex.Message)
                 );

            }

        }

        [NonAction]
        public IActionResult UpdateResult<T>(ApiResponse<T> response)
        {
            try
            {
                var result2 = new ApiResponse<T>(response.IsSuccess, response.Value, "Güncelleme Başarılı");
                return Ok(result2);
            }
            catch (Exception ex)
            {
                return StatusCode(
                   StatusCodes.Status500InternalServerError,
                   new ExceptionApiResponse<T>("Beklenmeyen bir hata oluştu.", ex.Message)
                   );
            }

        }

        [NonAction]
        public IActionResult DeleteResult<T>(ApiResponse<T> response)
        {
            try
            {
                var result2 = new ApiResponse<T>(response.IsSuccess, response.Value, "Silme Başarılı");
                return Ok(result2);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ExceptionApiResponse<T>("Beklenmeyen bir hata oluştu.", ex.Message)
                    );
            }

        }

        [NonAction]
        public IActionResult LoginResult<T>(ApiResponse<T> response)
        {
            try
            {
                var result2 = new ApiResponse<T>(response.IsSuccess, response.Value, "Giriş Başarılı");
                return Ok(result2);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ExceptionApiResponse<T>("Beklenmeyen bir hata oluştu.", ex.Message)
                    );
            }

        }
    }
}
