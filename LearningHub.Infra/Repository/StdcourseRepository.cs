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
    public class StdcourseRepository: IStdcourseRepository
    {
        private readonly IDbContext _dbContext;

        public StdcourseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public List<Stdcourse> GetAllStdcourses()
        {
            IEnumerable<Stdcourse> records = _dbContext.Connection.Query<Stdcourse>
                ("StdCourse_pkg.GetAllStudentCourses", commandType: CommandType.StoredProcedure);

            return records.ToList();
        }

        public void CreateStdcourse(Stdcourse stdcourse)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_stid", stdcourse.Stid, dbType: DbType.UInt32, direction: ParameterDirection.Input);
            parameters.Add("p_courseid", stdcourse.Courseid, dbType: DbType.UInt32, direction: ParameterDirection.Input);
            parameters.Add("p_markofstd", stdcourse.Markofstd, dbType: DbType.UInt32, direction: ParameterDirection.Input);
            parameters.Add("p_dateofregister", stdcourse.Dateofregister, dbType: DbType.DateTime, direction: ParameterDirection.Input);

            var record = _dbContext.Connection.Execute
                ("StdCourse_pkg.CreateStudentCourse", parameters, commandType: CommandType.StoredProcedure);
        }

        public void UpdateStdcourse(Stdcourse stdcourse)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_id", stdcourse.Id, dbType: DbType.UInt32, direction: ParameterDirection.Input);
            parameters.Add("p_stid", stdcourse.Stid, dbType: DbType.UInt32, direction: ParameterDirection.Input);
            parameters.Add("p_courseid", stdcourse.Courseid, dbType: DbType.UInt32, direction: ParameterDirection.Input);
            parameters.Add("p_markofstd", stdcourse.Markofstd, dbType: DbType.UInt32, direction: ParameterDirection.Input);
            parameters.Add("p_dateofregister", stdcourse.Dateofregister, dbType: DbType.DateTime, direction: ParameterDirection.Input);

            var record = _dbContext.Connection.Execute
                ("StdCourse_pkg.DeleteStudentCourse", parameters, commandType: CommandType.StoredProcedure);
        }

        public void DeleteStdcourse(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("p_id", id, dbType: DbType.UInt32, direction: ParameterDirection.Input);

            var record = _dbContext.Connection.Execute
                ("StdCourse_pkg.UpdateStudentCourse", parameter, commandType: CommandType.StoredProcedure);
        }

        public Stdcourse? GetStdcourseById(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("p_id", id, dbType: DbType.UInt32, direction: ParameterDirection.Input);

            IEnumerable<Stdcourse> records = _dbContext.Connection.Query<Stdcourse>
               ("StdCourse_pkg.GetStudentCourseById", parameter, commandType: CommandType.StoredProcedure);

            return records.FirstOrDefault();
        }
    }
}
