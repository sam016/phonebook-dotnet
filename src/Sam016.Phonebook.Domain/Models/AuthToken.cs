using System;
using System.Collections.Generic;

namespace Sam016.Phonebook.Domain.Models
{
    public class AuthToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
