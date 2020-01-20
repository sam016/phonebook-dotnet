namespace Sam016.Phonebook.API.Models.Queries
{
    public abstract class BaseUserQuery : BaseQuery
    {
        public uint UserId { get; set; }
    }
}
