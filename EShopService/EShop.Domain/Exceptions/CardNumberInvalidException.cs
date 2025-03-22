using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Exceptions;

public class CardNumberInvalidException : Exception
{
    public CardNumberInvalidException() { }
    public CardNumberInvalidException(string message = "Card number invalid")
    {
        message = "Card number invalid";
    }

    public CardNumberInvalidException(string message, Exception innerException) : base(message, innerException) { }
}
