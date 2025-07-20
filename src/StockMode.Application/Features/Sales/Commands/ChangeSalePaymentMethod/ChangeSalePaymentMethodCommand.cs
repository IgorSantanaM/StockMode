using MediatR;
using StockMode.Application.Features.Products.Dtos;
using StockMode.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Features.Sales.Commands.ChangeSalePaymentMethod;

public record ChangeSalePaymentMethodCommand(int SaleId, PaymentMethod NewPaymentMethod) : IRequest;

