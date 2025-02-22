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
    public class LoginService: ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public List<Login> GetAllLogins()
        {
            return _loginRepository.GetAllLogins();
        }

        public void CreateLogin(Login login)
        {
            _loginRepository.CreateLogin(login);
        }

        public void UpdateLogin(Login login)
        {
            _loginRepository.UpdateLogin(login);
        }

        public void DeleteLogin(int id)
        {
            _loginRepository.DeleteLogin(id);
        }

        public Login? GetLoginById(int id)
        {
            return _loginRepository.GetLoginById(id);
        }
    }
}
