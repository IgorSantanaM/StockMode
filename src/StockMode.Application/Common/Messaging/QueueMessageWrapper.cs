using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StockMode.Application.Common.Messaging
{
    /// <summary>
    /// Legacy wrapper for channel-based queue messages
    /// </summary>
    public record QueueMessageWrapper(string MessageType, string Payload);

    /// <summary>
    /// Generic wrapper for any email message to be sent via RabbitMQ
    /// This allows flexible email sending for any template type
    /// </summary>
    public class GenericEmailMessage
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string TemplateName { get; set; } = string.Empty;
        public string ModelJson { get; set; } = string.Empty;
        public string ModelType { get; set; } = string.Empty;
        
        public GenericEmailMessage() { }

        public GenericEmailMessage(string to, string subject, string templateName, object model)
        {
            To = to;
            Subject = subject;
            TemplateName = templateName;
            ModelJson = JsonSerializer.Serialize(model);
            ModelType = model.GetType().AssemblyQualifiedName ?? string.Empty;
        }

        public T? GetModel<T>()
        {
            return JsonSerializer.Deserialize<T>(ModelJson);
        }

        public object? GetModel(Type type)
        {
            return JsonSerializer.Deserialize(ModelJson, type);
        }
    }
}
