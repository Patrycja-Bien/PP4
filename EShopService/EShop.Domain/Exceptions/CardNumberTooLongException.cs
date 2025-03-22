using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Exceptions;

public class CardNumberTooLongException : Exception
{
    public CardNumberTooLongException()
    {
        
    }
    public CardNumberTooLongException(string message = "Card number too long") 
    {
        message = "Card number too long";
    }

    public CardNumberTooLongException(string message, Exception innerException) { }
}
