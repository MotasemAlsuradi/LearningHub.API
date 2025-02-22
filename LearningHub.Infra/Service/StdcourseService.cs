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
    public class StdcourseService: IStdcourseService
    {
        private IStdcourseRepository _stdcourseRepository;

        public StdcourseService(IStdcourseRepository stdcourseRepository)
        { 
            _stdcourseRepository = stdcourseRepository; 
        }

        public List<Stdcourse> GetAllStdcourses()
        {
            return _stdcourseRepository.GetAllStdcourses();
        }

        public void CreateStdcourse(Stdcourse stdcourse)
        {
            _stdcourseRepository.CreateStdcourse(stdcourse);
        }

        public void UpdateStdcourse(Stdcourse stdcourse)
        {
            _stdcourseRepository.UpdateStdcourse(stdcourse);
        }

        public void DeleteStdcourse(int id)
        {
            _stdcourseRepository.DeleteStdcourse(id);
        }

        public Stdcourse? GetStdcourseById(int id)
        {
            return _stdcourseRepository.GetStdcourseById(id);
        }
    }
}
