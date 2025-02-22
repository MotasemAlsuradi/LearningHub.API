using Dapper;
using LearningHub.Core.Common;
using LearningHub.Core.Data;
using LearningHub.Core.Repositry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Infra.Repository
{
    public class CourseRepository: ICourseRepository
    {
        private readonly IDbContext _dbContext;

        public CourseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public List<Course> GetAllCourses()
        {
            IEnumerable<Course> records = _dbContext.Connection.Query<Course>
                ("Course_pkg.GetAllCourses", commandType: CommandType.StoredProcedure);

            return records.ToList();
        }

        public void CreateCourse(Course course)
        {
            var parameter = new DynamicParameters();
            parameter.Add("p_course_name", course.Coursename, dbType: DbType.String, direction: ParameterDirection.Input);
            parameter.Add("p_catId", course.Categoryid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameter.Add("p_image", course.Imagename, dbType: DbType.String, direction: ParameterDirection.Input);

            var record = _dbContext.Connection.Execute
                ("Course_pkg.CreateCourse", parameter, commandType: CommandType.StoredProcedure);
        }

        public void UpdateCourse(Course course)
        {
            var parameter = new DynamicParameters();
            parameter.Add("p_ID", course.Courseid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameter.Add("p_course_name", course.Coursename, dbType: DbType.String, direction: ParameterDirection.Input);
            parameter.Add("p_catId", course.Categoryid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameter.Add("p_image", course.Imagename, dbType: DbType.String, direction: ParameterDirection.Input);

            var record = _dbContext.Connection.Execute
                ("Course_pkg.UpdateCourse", parameter, commandType: CommandType.StoredProcedure);
        }

        public void DeleteCourse(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("p_ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            var record = _dbContext.Connection.Execute
                ("Course_pkg.DeleteCourse", parameter, commandType: CommandType.StoredProcedure);
        }

        public Course? GetCourseById(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("p_Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            IEnumerable<Course> records = _dbContext.Connection.Query<Course>
                ("Course_pkg.GetCourseById", parameter, commandType: CommandType.StoredProcedure);

            return records.FirstOrDefault();
        }
    }
}
