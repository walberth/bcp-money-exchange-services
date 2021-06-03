namespace MoneyExchange.Controllers
{
    using Application.DTO;
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
        /// Constructor for Exchange
        ///</Summary>
        public ExchangeController(IExchangeApplication exchangeApplication)
        {
            _exchangeApplication = exchangeApplication;
        }

        ///<Summary>
        /// Get the exchange types registered in the sqlite database
        ///</Summary>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetExchangeTypes()
        {
            return Ok(_exchangeApplication.GetExchangeTypes());
        }

        ///<Summary>
        /// Realize the money exchange from one type to another
        ///</Summary>
        [HttpPost]
        public ActionResult RealizeMoneyExchange([FromBody] ReceiveExchangeDto receiveExchange)
        {
            return Ok(_exchangeApplication?.RealizeMoneyExchange(receiveExchange));
        }

        ///<Summary>
        /// Realize the change of the value of the type exchange
        ///</Summary>
        [HttpPut]
        public ActionResult ChangeMoneyExchangeType([FromBody] ExchangeTypeDto exchangeType)
        {
            return Ok(_exchangeApplication?.ChangeMoneyExchangeType(exchangeType));
        }
    }
}
