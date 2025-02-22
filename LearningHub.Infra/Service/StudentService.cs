using LearningHub.Core.Data;
using LearningHub.Core.Repository;
using LearningHub.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Infra.Service
{
    public class StudentService: IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        
        public StudentService(IStudentRepository studentRepository) 
        {
            _studentRepository = studentRepository;
        }

        public Task<List<Student>> GetAllStudentsAsync()
        {
            return _studentRepository.GetAllStudentsAsync();
        }

        public Task<bool> CreateStudentAsync(Student student)
        {
            return _studentRepository.CreateStudentAsync(student);
        }

        public Task<bool> UpdateStudentAsync(Student student)
        {
            return _studentRepository.UpdateStudentAsync(student);
        }

        public Task<bool> DeleteStudentAsync(int id)
        {
            return _studentRepository.DeleteStudentAsync(id);
        }

        public Task<Student?> GetStudentByIdAsync(int id)
        {
            return _studentRepository.GetStudentByIdAsync(id);
        }

        public Task<List<Student>> GetStudentNamesAsync()
        {
            return _studentRepository.GetStudentNamesAsync();
        }

        public Task<List<Student>> GetStudentsByFirstNameAsync(string firstName)
        {
            return _studentRepository.GetStudentsByFirstNameAsync(firstName);
        }

        public Task<List<Student>> GetStudentsByDOBAsync(DateTime dateOfBirth)
        {
            return _studentRepository.GetStudentsByDOBAsync(dateOfBirth);
        }

        public Task<List<Student>> GetStudentsByDOBIntervalAsync(DateTime startDateOfBirth, DateTime endDateOfBirth)
        {
            return _studentRepository.GetStudentsByDOBIntervalAsync(startDateOfBirth, endDateOfBirth);
        }

        public Task<List<Student>> GetTopNStudentsByMarksAsync(int numberOfTopStudents)
        {
            return _studentRepository.GetTopNStudentsByMarksAsync(numberOfTopStudents);
        }
    }
}
