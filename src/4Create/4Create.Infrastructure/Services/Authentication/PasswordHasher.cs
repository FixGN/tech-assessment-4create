﻿using Isopoh.Cryptography.Argon2;

namespace _4Create.Infrastructure.Services.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return Argon2.Hash(password);
        }

        public bool Verify(string hashedPassword, string password)
        {
            return Argon2.Verify(hashedPassword, password);
        }
    }
}
