using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskManagerApi.Dtos
{
    public record UserRegisterDto(string Username, string Password, string FullName)
    {
        
    }
}