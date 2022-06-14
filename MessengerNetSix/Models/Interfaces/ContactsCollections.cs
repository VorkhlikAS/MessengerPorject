using System.Collections.Generic;
using System.Linq;

namespace MessengerNetSix.Models.Interfaces
{
    public class ContactsCollections : IContacts
    {
        private readonly ApplicationContext _context;
        public ContactsCollections(ApplicationContext context)
        {
            _context = context;
         
        }

        public IEnumerable<Contact> GetNonConfirmedContacts(string id) => _context.Contacts.Where(c => (!c.IsConfirmed && !c.Forbiden) && (c.SenderId == id ||c.RecieverId == id)).ToList();

        public IEnumerable<Contact> GetUserContacts(string id) => _context.Contacts.Where(p => (p.Forbiden == false) && (p.SenderId == id || p.RecieverId == id)).ToList();

    }
}
