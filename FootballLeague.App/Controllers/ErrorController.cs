using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FootballLeague.App.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger _logger;

        public ErrorController(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<ErrorController>();
        }

        [Route("Error/404")]
        public IActionResult HandlePageNotFound()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                this._logger.LogError(exceptionFeature.Error.Message);
                this._logger.LogError(exceptionFeature.Path);
            }

            ViewBag.Title = "Page Not Found";
            ViewBag.ErrorMessage = "Page wasn't found.";
            return this.View("ErrorPage");
        }

        [Route("Error/500")]
        public IActionResult HandleInternalServerError()
        {
            
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                this._logger.LogError(exceptionFeature.Error.Message);
                this._logger.LogError(exceptionFeature.Path);
            }

            ViewBag.Title = "Internal Server Error";
            ViewBag.ErrorMessage = "Something went wrong on the Server, please try again or contact admin.";
            return this.View("ErrorPage");
        }

        [Route("Error/400")]
        public IActionResult HandleBadRequest()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                this._logger.LogError(exceptionFeature.Error.Message);
                this._logger.LogError(exceptionFeature.Path);
            }

            ViewBag.Title = "Bad Request";
            ViewBag.ErrorMessage = "Something went wrong, please try again or contact admin.";
            return this.View("ErrorPage");
        }
    }
}