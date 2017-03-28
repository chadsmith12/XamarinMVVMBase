using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleProject.Models;

namespace SampleProject.Interfaces
{
    public interface IDataService
    {
        Task<Response<UserResponse>> GetAuthTokenAsync(LoginModel loginModel);
    }
}
