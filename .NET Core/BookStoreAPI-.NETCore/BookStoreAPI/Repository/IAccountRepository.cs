﻿using BookStoreAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStoreAPI.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel model);
        Task<string> LoginAsync(SignInModel model);
    }
}
