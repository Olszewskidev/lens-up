using Azure.Data.Tables;
using LensUp.Common.AzureTableStorage.TableConfiguration;

namespace LensUp.Common.AzureTableStorage.Repository
{
    public sealed class TableClientRepository<TEntity> : ITableClientRepository<TEntity> where TEntity : AzureTableEntityBase
    {
        private readonly TableClient tableClient;

        public TableClientRepository(TableServiceClient tableServiceClient, ITableConfiguration<TEntity> tableConfiguration)
        {
            this.tableClient = tableServiceClient.GetTableClient(tableConfiguration.TableName);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
            => await this.tableClient.AddEntityAsync(entity, cancellationToken);

        public async Task<TEntity?> GetAsync(string partitionKey, string rowKey, CancellationToken cancellationToken)
        {
            var result = await this.tableClient.GetEntityAsync<TEntity>(partitionKey, rowKey, cancellationToken: cancellationToken);

            return result is null ? null : result.Value;
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
            => await this.tableClient.UpdateEntityAsync(entity, entity.ETag, TableUpdateMode.Replace, cancellationToken);
    }
}
