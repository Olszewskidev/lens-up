namespace LensUp.Common.AzureTableStorage.TableConfiguration;

public interface ITableConfiguration<TEntity> where TEntity : AzureTableEntityBase
{
    public string TableName { get; }
}
