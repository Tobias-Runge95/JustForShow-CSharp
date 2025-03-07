﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShowCase.AuthDatabase.Models;
using ShowCase.Core.Request.Authentication;

namespace ShowCase.Core.Authentication;

public class UserManager : UserManager<User>
{
    public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
        
    }

    public async Task<bool> RegisterUser(Register register)
    {
        var registerResult = await CreateAsync(new User { UserName = register.Name, Email = register.Email, PhoneNumber = register.PhoneNumber }, register.Password);

        if (registerResult.Errors.Count() > 0)
        {
            
        }
        
        return registerResult.Succeeded;
    }
}