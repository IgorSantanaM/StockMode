using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Domain.Enums
{
    public enum PaymentMethod
    {
        Pix = 1,
        CreditCard = 2,
        DebitCard = 3,
        Cash = 4,
        /// <summary>
        /// "Fiado" or "On Credit" - a common term in some regions where the customer buys on credit and pays later.
        /// </summary>
        StoreCredit = 5 
    }
}
