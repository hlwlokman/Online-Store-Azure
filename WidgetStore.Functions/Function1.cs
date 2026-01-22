using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace WidgetStore.Functions
{
    public static class OrderProcessingFunction
    {
        [FunctionName("OrderProcessingFunction")]
        public static async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Order Processing Function executed at: {DateTime.Now}");
            log.LogInformation("Created by: Lokman");

            try
            {
                log.LogInformation("Checking for pending orders...");

                // Process orders one by one
                for (int i = 1; i <= 3; i++)
                {
                    log.LogInformation($"Processing order #{i}");
                    await Task.Delay(100);
                }

                log.LogInformation($"Successfully processed 3 orders");
            }
            catch (Exception ex)
            {
                log.LogError($"Error processing orders: {ex.Message}");
            }
        }
    }
}