using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EShop.Application;
using EShop.Domain.Models;

namespace EShopService.Controllers;

using Eshop.Application;
using EShop.Domain.Exceptions;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[ApiController]
[Route("api/credit-card")]
public class CreditCardController : ControllerBase
{
    private readonly ICreditCardService creditCardService;
    private readonly IProductService productService;

    public CreditCardController(ICreditCardService _creditCardService, IProductService _productService)
    {
        creditCardService = _creditCardService;
        productService = _productService;
    }

    [HttpPost("validate")]
    public IActionResult ValidateCard([FromBody] string cardNumber)
    {

        try 
        {
            creditCardService.ValidateCardNumber(cardNumber);
            string issuer = creditCardService.GetCardType(cardNumber);
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
    [HttpGet("getProductByID")]
    public IActionResult GetProductByID([FromQuery] int productID)
    {
        try
        {
            var product = productService.GetProduct(productID);
            if (product == null)
            {
                return NotFound(new { Error = "product not found" });
            }
            return Ok(product);
        }
        catch (Exception)
        {
            return NotFound(new { Error = "prodcut  not found" });
        }

    }
    [HttpGet("deleteProductByID")]
    public IActionResult DeleteProductByID([FromQuery] int productID)
    {
        try
        {
            bool deleted = productService.DeleteProduct(productID);
            if (deleted)
            {
                return Ok();
            }
            return NotFound(new { Error = "product not found" });
        }
        catch (Exception)
        {
            return NotFound(new { Error = "product not found" });
        }

    }
    [HttpGet("getAllProducts")]
    public IActionResult GetAllProducts()
    {
        try
        {
            var products = productService.GetProducts();
            if (!products.Any())
            {
                return Ok(new { Message = "no products found" });

            }
            return Ok(products);
        }
        catch (Exception)
        {
            return StatusCode(500, new { Error = "an error occurred while retrieving products" });
        }
    }
    [HttpPost("addProduct")]
    public IActionResult AddProduct([FromBody] Product product)
    {
        try
        {
            if (product == null)
            {
                return BadRequest(new { Error = "product data is invalid" });
            }
            productService.AddProduct(product);


            return CreatedAtAction(nameof(GetProductByID), new { productID = product.Id }, product);
        }
        catch (Exception)
        {
            return StatusCode(500, new { Error = "an error occurred while adding the product" });
        }
    }
    [HttpPost("updateProduct")]
    public IActionResult UpdateProduct([FromBody] Product product)
    {
        try
        {
            if (product == null)
            {
                return BadRequest(new { Error = "product data is invalid" });
            }
            productService.UpdateProduct(product);
            return Ok(new { Message = "product updated" });
        }
        catch (Exception)
        {
            return StatusCode(500, new { Error = "an error occurred while updating the product" });
        }

    }
}