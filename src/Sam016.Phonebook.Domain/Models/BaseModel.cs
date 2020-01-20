using System;

namespace Sam016.Phonebook.Domain.Models
{
    public abstract class BaseModel
    {
        public uint Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
