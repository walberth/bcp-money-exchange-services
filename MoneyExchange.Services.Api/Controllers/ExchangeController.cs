using System.Threading.Tasks;

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
        public async Task<IActionResult> GetExchangeTypes()
        {
            var exchangeTypes = await _exchangeApplication.GetExchangeTypes();
            return Ok(exchangeTypes);
        }

        ///<Summary>
        /// Realize the money exchange from one type to another
        ///</Summary>
        [HttpPost]
        [Route("perform")]
        public ActionResult PerformMoneyExchange([FromBody] ReceiveExchangeDto receiveExchange)
        {
            return Ok(_exchangeApplication?.PerformMoneyExchange(receiveExchange));
        }

        ///<Summary>
        /// Realize the change of the value of the type exchange
        ///</Summary>
        [HttpPut]
        public ActionResult ChangeMoneyExchangeType([FromBody] ExchangeTypeDto exchangeType)
        {
            return Ok(_exchangeApplication?.ChangeMoneyExchangeType(exchangeType));
        }

        ///<Summary>
        /// Register a new type of change
        ///</Summary>
        [HttpPost]
        public ActionResult RegisterMoneyExchangeType([FromBody] ExchangeTypeDto exchangeType)
        {
            return Ok(_exchangeApplication?.RegisterMoneyExchangeType(exchangeType));
        }

        ///<Summary>
        /// Delete type of change
        ///</Summary>
        [HttpDelete]
        [Route("{originCurrency:=originCurrency}/{destinationCurrency:=destinationCurrency}")]
        public ActionResult DeleteMoneyExchangeType([FromRoute] string originCurrency, [FromRoute] string destinationCurrency)
        {
            return Ok(_exchangeApplication?.DeleteMoneyExchangeType(originCurrency, destinationCurrency));
        }
    }
}
