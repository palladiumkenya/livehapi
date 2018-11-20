using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Interfaces.Builders;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Model.Builder
{
    public class ClientNetworkBuilder:IClientNetworkBuilder
    {
        private List<ClientNetwork> _clientNetworks=new List<ClientNetwork>();
        private Contact _primaryContact;
        private List<Contact> _secondaryContacts=new List<Contact>();
        
        public void CreatePrimary(Contact primaryContact)
        {
            _primaryContact = primaryContact;
        }

        public void AddSecondaryContact(Contact secondaryContact)
        {
            if (!_secondaryContacts.Any(x => x.Id == secondaryContact.Id && x.Relation == secondaryContact.Relation))
                _secondaryContacts.Add(secondaryContact);
        }

        public void AddSecondaryContacts(IEnumerable<Contact> secondaryContacts)
        {
            secondaryContacts.ToList().ForEach(AddSecondaryContact);
        }

        public IEnumerable<ClientNetwork> Build()
        {
            foreach (var secondaryContact in _secondaryContacts)
            {
                _clientNetworks.Add(new ClientNetwork(_primaryContact, secondaryContact));
            }

            return _clientNetworks;
        }
    }
}