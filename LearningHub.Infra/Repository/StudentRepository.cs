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
    public class StudentRepository : IStudentRepository
    {
        private readonly IDbContext _dbContext;

        public StudentRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            IEnumerable<Student> students = await _dbContext.Connection.QueryAsync<Student>(
                "Student_pkg.GetAllStudents", commandType: CommandType.StoredProcedure);

            return students.ToList();
        }

        public async Task<bool> CreateStudentAsync(Student student)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_firstname", student.Firstname, DbType.String, ParameterDirection.Input);
                parameters.Add("p_lastname", student.Lastname, DbType.String, ParameterDirection.Input);
                parameters.Add("p_dateofbirth", student.Dateofbirth, DbType.Date, ParameterDirection.Input);

                int rowsAffected = await _dbContext.Connection.ExecuteAsync(
                    "Student_pkg.CreateStudent", parameters, commandType: CommandType.StoredProcedure);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating student: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_studentid", student.Studentid, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_firstname", student.Firstname, DbType.String, ParameterDirection.Input);
                parameters.Add("p_lastname", student.Lastname, DbType.String, ParameterDirection.Input);
                parameters.Add("p_dateofbirth", student.Dateofbirth, DbType.Date, ParameterDirection.Input);

                int rowsAffected = await _dbContext.Connection.ExecuteAsync(
                    "Student_pkg.UpdateStudent", parameters, commandType: CommandType.StoredProcedure);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating student ID {student.Studentid}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_studentid", id, DbType.Int32, ParameterDirection.Input);

                int rowsAffected = await _dbContext.Connection.ExecuteAsync(
                    "Student_pkg.DeleteStudent", parameters, commandType: CommandType.StoredProcedure);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting student ID {id}: {ex.Message}");
                return false;
            }
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_studentid", id, DbType.Int32, ParameterDirection.Input);

                var student = await _dbContext.Connection.QueryFirstOrDefaultAsync<Student>(
                    "Student_pkg.GetStudentById", parameters, commandType: CommandType.StoredProcedure);

                return student; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching student by ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Student>> GetStudentsByFirstNameAsync(string firstName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_firstname", firstName, DbType.String, ParameterDirection.Input);

            IEnumerable<Student> students = await _dbContext.Connection.QueryAsync<Student>(
                "Student_pkg.GetStudentByFirstName", parameters, commandType: CommandType.StoredProcedure);

            return students.ToList();
        }

        public async Task<List<Student>> GetStudentNamesAsync()
        {
            IEnumerable<Student> students = await _dbContext.Connection.QueryAsync<Student>(
                "Student_pkg.GetStudentNames", commandType: CommandType.StoredProcedure);

            return students.ToList();
        }

        public async Task<List<Student>> GetStudentsByDOBAsync(DateTime dateOfBirth)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_dateofbirth", dateOfBirth, DbType.Date, ParameterDirection.Input);

            IEnumerable<Student> students = await _dbContext.Connection.QueryAsync<Student>(
                "Student_pkg.GetStudentsByDOB", parameters, commandType: CommandType.StoredProcedure);

            return students.ToList();
        }

        public async Task<List<Student>> GetStudentsByDOBIntervalAsync(DateTime startDateOfBirth, DateTime endDateOfBirth)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_start_date", startDateOfBirth, DbType.Date, ParameterDirection.Input);
            parameters.Add("p_end_date", endDateOfBirth, DbType.Date, ParameterDirection.Input);

            IEnumerable<Student> students = await _dbContext.Connection.QueryAsync<Student>(
                "Student_pkg.GetStudentsByDOBInterval", parameters, commandType: CommandType.StoredProcedure);

            return students.ToList();
        }

        public async Task<List<Student>> GetTopNStudentsByMarksAsync(int numberOfTopStudents)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_n", numberOfTopStudents, DbType.Int32, ParameterDirection.Input);

            IEnumerable<Student> students = await _dbContext.Connection.QueryAsync<Student>(
                "Student_pkg.GetTopNStudentsByMarks", parameters, commandType: CommandType.StoredProcedure);

            return students.ToList();
        }
    }
}

/*
    Method                      Needs try-catch?	        Reason

    GetAllStudentsAsync	            ❌ No            No write operations, error will bubble up naturally.
    GetStudentByIdAsync	            ✅ Yes           Needs to return null if error occurs.
    CreateStudentAsync	            ✅ Yes           Needs to return false if student creation fails.
    UpdateStudentAsync	            ✅ Yes           Needs to return false if update fails.
    DeleteStudentAsync	            ✅ Yes	         Needs to return false if deletion fails.
    GetStudentsByFirstNameAsync	    ❌ No	         Simple query, no critical error handling needed.
    GetStudentNamesAsync	        ❌ No	         Same as above.
    GetStudentsByDOBAsync	        ❌ No	         Same as above.
    GetStudentsByDOBIntervalAsync	❌ No	         Same as above.
    GetTopNStudentsByMarksAsync	    ❌ No	         Same as above.
 */