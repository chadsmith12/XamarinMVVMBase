using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleProject.Base;
using SampleProject.Interfaces;
using SampleProject.Models;

namespace SampleProject.Services
{
    public class DataService : BaseHttpService, IDataService
    {
        public async Task<Response<UserResponse>> GetAuthTokenAsync(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}
