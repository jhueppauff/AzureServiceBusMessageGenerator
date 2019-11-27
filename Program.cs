using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace AzureServiceBusMessageGenerator
{
    public class Program
    {
        private static IConfiguration configuration;

        static async Task Main(string[] args)
        {            
            configuration = GetConfiguration();

            bool continueMessage = true;

            while (continueMessage)
            {
                Console.WriteLine("Enter your message content:");
                string inputText = Console.ReadLine();

                await SendMessage(inputText);

                Console.WriteLine("Send next message? y/n");
                string input = Console.ReadLine();

                if(input.ToLowerInvariant() == "n")
                {
                    continueMessage = false;
                }
            }
        }

        private static async Task SendMessage(string messageText)
        {
            // Send Message
            Message message = new Message { Body = Encoding.UTF8.GetBytes(messageText) }; 

            AzureQueueSender sender = new AzureQueueSender(configuration.GetSection("queueName").Value, configuration.GetSection("serviceBusConnectionString").Value);
            await sender.SendAsync(message, null);
        }

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false).Build();
        }
    }
}
