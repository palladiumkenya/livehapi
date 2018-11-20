using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Interfaces.Builders;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Model.Builder
{
    public class ClientContactNetworkBuilder:IClientContactNetworkBuilder
    {
        private List<ClientContactNetwork> _clientNetworks=new List<ClientContactNetwork>();
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

        public IEnumerable<ClientContactNetwork> Build()
        {
            var primaryNetwork = ClientContactNetwork.CreatePrimary(_primaryContact);
          
            _clientNetworks.Add(primaryNetwork);

            foreach (var secondaryContact in _secondaryContacts)
                _clientNetworks.Add(ClientContactNetwork.CreateSecondary(secondaryContact, primaryNetwork.Id));

            return _clientNetworks;
        }
    }
}