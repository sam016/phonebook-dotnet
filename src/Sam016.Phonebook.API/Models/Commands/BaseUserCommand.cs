namespace Sam016.Phonebook.API.Models.Commands
{
    public abstract class BaseUserCommand : BaseCommand
    {
        public uint UserId { get; set; }
    }
}
