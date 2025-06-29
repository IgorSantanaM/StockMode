using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Domain.Enums
{
    public enum StockMovementType
    {
        PurchaseEntry = 1,
        SaleExit = 2,
        PositiveAdjustment = 3,
        LossAdjustment = 4,
        CustomerReturn = 5,
        SupplierReturnExit = 6
    }
}
