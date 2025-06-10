using Microsoft.AspNetCore.Mvc;
using School.Models;

namespace School.Services
{
    public interface ITokenService
    {
        public string CreateToken(ApplicationUser user);

    }
}
