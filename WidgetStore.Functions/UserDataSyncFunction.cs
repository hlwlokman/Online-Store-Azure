using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace WidgetStore.Functions
{
    public static class UserDataSyncFunction
    {
        [FunctionName("UserDataSyncFunction")]
        public static async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"User Data Sync Function executed at: {DateTime.Now}");
            log.LogInformation("Developer: Lokman");

            try
            {
                
                log.LogInformation("Collecting user data for sync...");

                // Simulate finding users
                var userCount = 5;
                log.LogInformation($"Found {userCount} users to sync");

                await Task.Delay(200); // Simulate sync work

                log.LogInformation($"Successfully synced {userCount} users to Lokman & Co department");
                log.LogInformation("User data sync completed");
            }
            catch (Exception ex)
            {
                log.LogError($"Error syncing user data: {ex.Message}");
            }
        }
    }
}