using Azure.Data.Tables;
using LensUp.Common.Types.Constants;
using Microsoft.Extensions.Hosting;

namespace LensUp.BackOfficeService.Infrastructure.Initializers;

/// <summary>
/// Azure Tables Initializer used for dev env purposes.
/// </summary>
public sealed class AzureTablesInitializer : IHostedService
{
    private TableServiceClient tableServiceClient;

    public AzureTablesInitializer(string azureStorageAccountConnectionString)
    {
        this.tableServiceClient = new TableServiceClient(azureStorageAccountConnectionString);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var taskList = new List<Task>();
        foreach (string tableName in TableNames.All)
        {
            var tableClient = this.tableServiceClient.GetTableClient(tableName);
            taskList.Add(tableClient.CreateIfNotExistsAsync(cancellationToken));
        }

        await Task.WhenAll(taskList);
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
