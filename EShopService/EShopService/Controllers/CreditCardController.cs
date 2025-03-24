using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EShop.Application;

namespace EShopService.Controllers;

using EShop.Domain.Exceptions;
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

        try 
        {
            cardService.ValidateCardNumber(cardNumber);
            string issuer = cardService.GetCardType(cardNumber);
            string[] supported_issuers = { "Visa", "American Express", "Mastercard" };

            if (issuer == null || !supported_issuers.Contains(issuer))
            {
                return StatusCode(406, "Error 406: Unsupported card issuer");
            }
            return Ok($"Valid card. Issuer: {issuer}");
            
        }
        catch (CardNumberTooLongException)
        {
            return StatusCode(414, "Error 414: Carn number too long");
        }
        catch (CardNumberInvalidException)
        {
            return StatusCode(400, "Error 400: Invalid card number");
        }
        catch (CardNumberTooShortException)
        {
            return StatusCode(400, "Error 400: Invalid card number");
        }
        
    }
}