using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IRepositories
{
    public interface ITokenRepository
    {
         string GenerateJwtToken(ApplicationUser User, List<string> Roles);
    }
}
