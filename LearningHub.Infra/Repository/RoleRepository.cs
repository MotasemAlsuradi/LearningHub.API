using Dapper;
using LearningHub.Core.Common;
using LearningHub.Core.Data;
using LearningHub.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Infra.Repository
{
    public class RoleRepository: IRoleRepository
    {
        private readonly IDbContext _dbContext;

        public RoleRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Role> GetAllRoles()
        {
            IEnumerable<Role> records = _dbContext.Connection.Query<Role>
                ("Role_pkg.GetAllRoles", commandType: CommandType.StoredProcedure);

            return records.ToList();
        }

        public void CreateRole(Role role)
        {
            var parameter = new DynamicParameters();
            parameter.Add("p_role_name", role.Rolename, DbType.String, direction: ParameterDirection.Input);

            var record = _dbContext.Connection.Execute
                ("Role_pkg.CreateRole", parameter, commandType: CommandType.StoredProcedure);
        }

        public void UpdateRole(Role role)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_ID", role.Roleid, DbType.UInt32, direction: ParameterDirection.Input);
            parameters.Add("p_role_name", role.Rolename, DbType.String, direction: ParameterDirection.Input);

            var record = _dbContext.Connection.Execute
                ("Role_pkg.UpdateRole", parameters, commandType: CommandType.StoredProcedure);
        }

        public void DeleteRole(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("p_ID", id, DbType.UInt32, direction: ParameterDirection.Input);

            var record = _dbContext.Connection.Execute
                ("Role_pkg.DeleteRole", parameter, commandType: CommandType.StoredProcedure);
        }

        public Role? GetRoleById(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("p_ID", id, DbType.UInt32, direction: ParameterDirection.Input);

            IEnumerable<Role> records = _dbContext.Connection.Query<Role>
                ("Role_pkg.GetRoleById", parameter, commandType: CommandType.StoredProcedure);

            return records.FirstOrDefault();
        }
    }
}
