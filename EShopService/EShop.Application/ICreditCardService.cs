using EShop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EShop.Application;

public interface ICreditCardService
{
    public Boolean ValidateCardNumber(string cardNumber);

    public string GetCardType(string cardNumber);
}
