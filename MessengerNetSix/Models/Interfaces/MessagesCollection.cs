namespace MessengerNetSix.Models.Interfaces
{
    public class MessagesCollection : IMessages
    {
        private readonly ApplicationContext _context;
        public MessagesCollection(ApplicationContext context)
        {
            _context = context;

        }
        public IEnumerable<Message> GetChatMessages(string id) => _context.Messages.Where(m => m.ChatId == id).ToList();
    }
}
