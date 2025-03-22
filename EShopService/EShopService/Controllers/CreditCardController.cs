using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EShop.Application;

namespace EShopService.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[ApiController]
[Route("api/credit-card")]
public class CreditCardController : ControllerBase
{
    [HttpPost("validate")]
    public IActionResult ValidateCard([FromBody] string cardNumber)
    {
        var cardService = new CreditCardService();

        if (!cardService.ValidateCardNumber(cardNumber))
        {
            return StatusCode(400, "Error 400: Invalid card number");
            //Niepoprawny, za długi numer za zwrócić błąd 414
            //Niepoprawny, za krótki lub niezgodny z walidacja sumy kontrolnej za zwrócić błąd 400
        }

        string issuer = cardService.GetCardType(cardNumber);
        string[] supported_issuers = { "Visa", "American Express", "Mastercard" };

        if (issuer == null || !supported_issuers.Contains(issuer))
        {
            return StatusCode(406, "Error 406: Unsupported card issuer");
        }

        return Ok($"Valid card. Issuer: {issuer}");
    }
}