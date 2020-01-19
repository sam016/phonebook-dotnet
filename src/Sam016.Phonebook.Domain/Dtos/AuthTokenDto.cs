
using System;

namespace Sam016.Phonebook.Domain.Dtos
{
    public class AuthTokenDto
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
