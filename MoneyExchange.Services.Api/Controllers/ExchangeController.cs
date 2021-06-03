namespace MoneyExchange.Controllers
{
    using Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using MoneyExchange.Service.Api.Controllers;

    ///<Summary>
    /// Exchange controller
    ///</Summary>
    [Route("api/exchange")]
    public class ExchangeController : BaseController
    {
        private readonly IExchangeApplication _exchangeApplication;

        ///<Summary>
        /// Constructor for Course
        ///</Summary>
        public ExchangeController(IExchangeApplication exchangeApplication)
        {
            _exchangeApplication = exchangeApplication;
        }

        ///<Summary>
        /// Get the psychology appointment inscriptions
        ///</Summary>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetExchangeTypes()
        {
            return Ok(_exchangeApplication.GetExchangeTypes());
        }
    }
}
