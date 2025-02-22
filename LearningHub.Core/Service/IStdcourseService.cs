using LearningHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Service
{
    public interface IStdcourseService
    {
        List<Stdcourse> GetAllStdcourses();
        void CreateStdcourse(Stdcourse stdcourse);
        void UpdateStdcourse(Stdcourse stdcourse);
        void DeleteStdcourse(int id);
        Stdcourse? GetStdcourseById(int id);
    }
}
