using Dapper;
using LearningHub.Core.Common;
using LearningHub.Core.Data;
using LearningHub.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Infra.Repository
{
    public class LoginRepository: ILoginRepository
    {
        private readonly IDbContext _dbContext;

        public LoginRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Login> GetAllLogins()
        {
            IEnumerable<Login> records = _dbContext.Connection.Query<Login>
                ("Login_pkg.GetAllStudents", commandType: CommandType.StoredProcedure);

            return records.ToList();
        }

        public void CreateLogin(Login login)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_username", login.Username, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("p_password", login.Password, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("p_roleId", login.Roleid, dbType: DbType.UInt32, direction: ParameterDirection.Input);
            parameters.Add("p_studentId", login.Studentid, dbType: DbType.String, direction: ParameterDirection.Input);

            var record = _dbContext.Connection.Execute
                ("Login_pkg.CreateStudent", parameters, commandType: CommandType.StoredProcedure);
        }

        public void UpdateLogin(Login login)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_ID", login.Studentid, dbType: DbType.UInt32, direction: ParameterDirection.Input);
            parameters.Add("p_username", login.Username, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("p_password", login.Password, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("p_roleId", login.Roleid, dbType: DbType.UInt32, direction: ParameterDirection.Input);
            parameters.Add("p_studentId", login.Studentid, dbType: DbType.UInt32, direction: ParameterDirection.Input);

            var record = _dbContext.Connection.Execute
                ("Login_pkg.UpdateStudent", parameters, commandType: CommandType.StoredProcedure);
        }

        public void DeleteLogin(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("p_ID", id, dbType: DbType.UInt32, direction: ParameterDirection.Input);

            var record = _dbContext.Connection.Execute
                ("Login_pkg.DeleteStudent", parameter, commandType: CommandType.StoredProcedure);
        }

        public Login? GetLoginById(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("p_Id", id, dbType: DbType.UInt32, direction: ParameterDirection.Input);

            IEnumerable<Login> records = _dbContext.Connection.Query<Login>
                ("Login_pkg.GetStudentById", parameter, commandType: CommandType.StoredProcedure);

            return records.FirstOrDefault();
        }
    }
}
