using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace AzureServiceBusMessageGenerator
{
    public class AzureQueueSender  
  {    
      public async Task SendAsync(Message item, Dictionary<string, object> properties)  
      {  
          var json = JsonConvert.SerializeObject(item);  
          var message = new Message(Encoding.UTF8.GetBytes(json));  
  
          if (properties != null)  
          {  
              foreach (var prop in properties)  
              {  
                  message.UserProperties.Add(prop.Key, prop.Value);  
              }  
          }  
  
          await client.SendAsync(message);  
      }  
  
      private AzureQueueSettings settings;  
      private QueueClient client;  
  
      public AzureQueueSender(string queueName, string queueConnectionString)  
      {  
          client = new QueueClient(queueName, queueConnectionString);  
      }  
  }  
}