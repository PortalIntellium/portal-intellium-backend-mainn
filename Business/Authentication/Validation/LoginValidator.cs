﻿using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Authentication.Validation
{
    public class LoginValidator:AbstractValidator<UserForLoginDto>
    {
        public LoginValidator()
        {
            
        }
    }
}
