using Azure;
using Azure.Data.Tables;

namespace LensUp.Common.AzureTableStorage;

public class AzureTableEntityBase : ITableEntity
{
    public string PartitionKey { get; set; } = string.Empty;
    public string RowKey { get; set; } = string.Empty;
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}
