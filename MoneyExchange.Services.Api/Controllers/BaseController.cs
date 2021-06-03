namespace MoneyExchange.Service.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    ///<Summary>
    /// Base controller
    ///</Summary>
    [Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
        ///<Summary>
        /// Constructor
        ///</Summary>
        public BaseController()
        {
        }
    }
}
