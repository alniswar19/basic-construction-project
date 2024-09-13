using BCI.Domain.Entities;
using BCI.Domain.Exceptions;
using BCI.Domain.QueryFilters;
using BCI.Domain.Repositories;
using Dapper;
using System.Data;
using System.Linq.Expressions;

namespace BCI.Infrastructure.Persistances
{
    public class ConstructionProjectRepository : IRepository<ConstructionProject, int>, IRepositoryExtended<ConstructionProject, ConstructionProjectFilter>
    {
        private readonly IDbConnectionFactory _dbFactory;

        public ConstructionProjectRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<ConstructionProject> Add(ConstructionProject entity)
        {
            using (var connection = _dbFactory.GetConnection())
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("Name", entity.Name);
                p.Add("Location", entity.Location);
                p.Add("Stage", entity.Stage);
                p.Add("Category", entity.Category);
                p.Add("StartDate", entity.StartDate);
                p.Add("Description", entity.Description);
                p.Add("CreatorId", entity.Creator.Id);

                var recordId = await connection.QuerySingleAsync<int>("[dbo].[stp_ConstructionProject_Create]", param: p, commandType: CommandType.StoredProcedure);
                return entity with
                {
                    Id = recordId
                };
            }
        }

        public Task<IEnumerable<ConstructionProject>> Find(Expression<Func<ConstructionProject, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<IEnumerable<ConstructionProject>, int>> FindAll(ConstructionProjectFilter filters)
        {
            using (var connection = _dbFactory.GetConnection())
            {
                connection.Open();
                DynamicParameters p = new DynamicParameters();
                p.Add("ProjectStage", filters.ProjectStage);
                p.Add("PageSize", filters.PageSize);
                p.Add("PageNumber", filters.PageNumber);
                if (filters.OrderBy != null)
                {
                    var sortBy = filters.SortOrder == Domain.Enums.SortOrder.Descending ? "-" : "+";
                    p.Add("OrderBy", $"{sortBy}{filters.OrderBy}");
                }

                using (var conn = connection.QueryMultiple("[dbo].[stp_ConstructionProject_FindAll]", param: p, commandType: CommandType.StoredProcedure))
                {
                    var user = conn.Read<ConstructionProject, User, ConstructionProject>((constructionProject, user) =>
                    {
                        constructionProject.Creator = user;
                        return constructionProject;
                    }, splitOn: "CreatorId").AsEnumerable();

                    var totalCount = conn.Read<int>().FirstOrDefault();

                    if (user is null)
                    {
                        throw new RecordNotFoundException("User not found.");
                    }

                    return Task.FromResult(Tuple.Create(user, totalCount));
                }
            }
        }

        public async Task<ConstructionProject> Get(int id)
        {
            using (var connection = _dbFactory.GetConnection())
            {
                connection.Open();
                DynamicParameters p = new DynamicParameters();
                p.Add("ProjectId", id);

                var resuit = await connection.QueryAsync<ConstructionProject, User, ConstructionProject>("[dbo].[stp_ConstructionProject_View]",
                    (project, user) =>
                    {
                        project.Creator = user;
                        return project;
                    }, splitOn: "CreatorId", param: p);

                return resuit.SingleOrDefault();
            }
        }

        public async Task<ConstructionProject> Update(ConstructionProject entity)
        {
            using (var connection = _dbFactory.GetConnection())
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("Id", entity.Id);
                p.Add("Name", entity.Name);
                p.Add("Location", entity.Location);
                p.Add("Stage", entity.Stage);
                p.Add("Category", entity.Category);
                p.Add("StartDate", entity.StartDate);
                p.Add("Description", entity.Description);

                return await connection.QuerySingleAsync<ConstructionProject>("[dbo].[stp_ConstructionProject_Update]", param: p, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
