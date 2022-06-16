namespace MessengerNetSix.Models.Interfaces
{
    public interface IMessages
    {

        IEnumerable<Message> GetChatMessages(string id);
    }
}
