namespace MessengerNetSix.Models.Interfaces
{
    public interface IChatMember
    {
        IEnumerable<ChatMember> GetMembers(string id);
    }
}
