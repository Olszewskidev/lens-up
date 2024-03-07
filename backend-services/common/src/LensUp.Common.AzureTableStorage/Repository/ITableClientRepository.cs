namespace LensUp.Common.AzureTableStorage.Repository
{
    public interface ITableClientRepository<TEntity> where TEntity : AzureTableEntityBase
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task<TEntity?> GetAsync(string partitionKey, string rowKey, CancellationToken cancellationToken);
    }
}
