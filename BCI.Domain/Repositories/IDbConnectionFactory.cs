using System.Data;

namespace BCI.Domain.Repositories
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
