namespace MessengerNetSix.Models.Interfaces
{
    public class ChatMemberCollections : IChatMember
    {
        private readonly ApplicationContext _context;
        public ChatMemberCollections(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<ChatMember> GetMembers(string id) => _context.ChatMembers.Where(p => p.ChatId.ToString() == id).ToList();

    }
}
