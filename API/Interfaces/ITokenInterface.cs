using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    //An Interface to create tokens for users
    public interface ITokenInterface
    {
        string CreateToken(AppUser appUser);
    }
}