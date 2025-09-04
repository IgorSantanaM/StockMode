﻿namespace StockMode.Application.Common.Messaging;

public record EmailMessage<TModel>(string To, string Subject, string TemplateName, TModel Model);
