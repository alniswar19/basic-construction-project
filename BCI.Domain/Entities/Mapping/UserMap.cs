using Dapper.FluentMap.Mapping;

namespace BCI.Domain.Entities.Mapping
{
    public class UserMap : EntityMap<User>
    {
        public UserMap()
        {
            Map(i => i.Id).ToColumn("CreatorId");
        }
    }
}
