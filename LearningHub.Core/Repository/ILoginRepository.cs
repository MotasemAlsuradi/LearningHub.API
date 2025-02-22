using LearningHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Repository
{
    public interface ILoginRepository
    {
        List<Login> GetAllLogins();
        void CreateLogin(Login login);
        void UpdateLogin(Login login);
        void DeleteLogin(int id);
        Login? GetLoginById(int id);
    }
}

