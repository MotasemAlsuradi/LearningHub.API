using LearningHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Repository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<bool> CreateStudentAsync(Student student);
        Task<bool> UpdateStudentAsync(Student student);
        Task<bool> DeleteStudentAsync(int id);
        Task<Student?> GetStudentByIdAsync(int id);


        Task<List<Student>> GetStudentNamesAsync();
        Task<List<Student>> GetStudentsByFirstNameAsync(string firstName);
        Task<List<Student>> GetStudentsByDOBAsync(DateTime dateOfBirth);
        Task<List<Student>> GetStudentsByDOBIntervalAsync(DateTime startDateOfBirth, DateTime endDateOfBirth);
        Task<List<Student>> GetTopNStudentsByMarksAsync(int numberOfTopStudents);
    }
}
