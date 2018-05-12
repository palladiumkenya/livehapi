using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;
using Encounter = LiveHAPI.Core.Model.Encounters.Encounter;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Core.Model.People
{
    public class Client : Entity<Guid>, IClient
    {
        [MaxLength(50)]
        public string MaritalStatus { get; set; }

        [MaxLength(50)]
        public string KeyPop { get; set; }

        [MaxLength(100)]
        public string OtherKeyPop { get; set; }
        public bool? IsFamilyMember { get; set; }
        public bool? IsPartner { get; set; }
        public bool? PreventEnroll { get; set; }
        public bool? AlreadyTestedPos { get; set; }
        public Guid PracticeId { get; set; }
        public Guid PersonId { get; set; }
        public Guid UserId { get; set; }
        public ICollection<ClientIdentifier> Identifiers { get; set; } = new List<ClientIdentifier>();
        public ICollection<ClientRelationship> Relationships { get; set; } = new List<ClientRelationship>();
        public ICollection<ClientAttribute> Attributes { get; set; } = new List<ClientAttribute>();
        public ICollection<Encounter> Encounters { get; set; } = new List<Encounter>();
        public ICollection<ClientState> ClientStates { get; set; }=new List<ClientState>();
        private ICollection<ClientSummary> ClientSummaries { get; set; }=new List<ClientSummary>();

        [NotMapped]
        public ClientIdentifier HtsEnrollment
        {
            get
            {
                if (Identifiers.Any())
                {
                    return Identifiers.FirstOrDefault(x => x.IdentifierTypeId.IsSameAs("Serial"));
                }
                return null;
            }

        }

        public Client()
        {
            Id = LiveGuid.NewGuid();
        }

      
        private Client(Guid id, string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Guid personId, bool? isFamilyMember, bool? isPartner, bool? preventEnroll, bool? alreadyTestedPos) :base(id)
        {
            MaritalStatus = maritalStatus;
            KeyPop = keyPop;
            OtherKeyPop = otherKeyPop;
            PracticeId = practiceId;
            PersonId = personId;
            IsFamilyMember = isFamilyMember;
            IsPartner = isPartner;
            PreventEnroll = preventEnroll;
            AlreadyTestedPos = alreadyTestedPos;
        }

        private Client(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Guid personId, bool? isFamilyMember, bool? isPartner, bool? preventEnroll, bool? alreadyTestedPos) :this(LiveGuid.NewGuid(),maritalStatus,keyPop,otherKeyPop,practiceId, personId, isFamilyMember, isPartner,preventEnroll, alreadyTestedPos)
        {
        }
        public static Client Create(ClientInfo clientInfo, Guid practiceId, Guid personId)
        {
            var client = new Client(clientInfo.Id, clientInfo.MaritalStatus, clientInfo.KeyPop, clientInfo.OtherKeyPop, practiceId,
                personId, clientInfo.IsFamilyMember, clientInfo.IsPartner,clientInfo.PreventEnroll,clientInfo.AlreadyTestedPos);

            var identifiers = ClientIdentifier.Create(clientInfo);
            client.AddIdentifiers(identifiers);

            var relationships = ClientRelationship.Create(clientInfo);
            client.AddRelationships(relationships);

            var states = ClientState.Create(clientInfo);
            client.AddClientStates(states);
            client.UserId = clientInfo.UserId;
            return client;
        }
        public void Update(ClientInfo clientInfo)
        {
            IsFamilyMember = clientInfo.IsFamilyMember;
            IsPartner = clientInfo.IsPartner;
            MaritalStatus = clientInfo.MaritalStatus;
            KeyPop = clientInfo.KeyPop;
            OtherKeyPop = clientInfo.OtherKeyPop;
            PreventEnroll = clientInfo.PreventEnroll;
            AlreadyTestedPos = clientInfo.AlreadyTestedPos;
            UserId = clientInfo.UserId;

            Identifiers.Clear();
            var identifiers = ClientIdentifier.Create(clientInfo);
            AddIdentifiers(identifiers);

            Relationships.Clear();
            var relationships = ClientRelationship.Create(clientInfo);
            AddRelationships(relationships);

            ClientStates.Clear();
            var stats = ClientState.Create(clientInfo);
            AddClientStates(stats);
        }

        public void AddIdentifier(ClientIdentifier personName)
        {
            personName.ClientId = Id;
            Identifiers.Add(personName);
        }
        public void AddIdentifiers(List<ClientIdentifier> personNames)
        {
            foreach (var personName in personNames)
            {
                AddIdentifier(personName);
            }
        }

        public void AddRelationship(ClientRelationship personName)
        {
            personName.ClientId = Id;
            Relationships.Add(personName);
        }
        public void AddRelationships(List<ClientRelationship> personNames)
        {
            foreach (var personName in personNames)
            {
                AddRelationship(personName);
            }
        }

        public void AddClientState(ClientState state)
        {
            state.ClientId = Id;
            ClientStates.Add(state);
        }
        public void AddClientStates(List<ClientState> states)
        {
            foreach (var state in states)
            {
                AddClientState(state);
            }
        }

        public bool IsPos()
        {
            var finalResultEnocunters = Encounters.Where(x => x.EncounterTypeId == new Guid("b262f4ee-852f-11e7-bb31-be2e44b06b34")).ToList();
            var obs = new List<ObsFinalTestResult>();
            foreach (var f in finalResultEnocunters)
            {
                obs.AddRange(f.ObsFinalTestResults);
            }

            if (obs.Count > 0)
            {
                return obs.Any(x => x.FinalResult == new Guid("B25EFD8A-852F-11E7-BB31-BE2E44B06B34"));
            }

            return false;
        }

        public bool IsFamilyContact()
        {
            if (null != PreventEnroll && PreventEnroll.Value)
            {
                var famEncounters = Encounters
                    .Where(x => x.EncounterTypeId == new Guid("B262FDA4-877F-11E7-BB31-BE2E44B66B34") ||
                                x.EncounterTypeId == new Guid("B262FDA4-877F-11E7-BB31-BE2E44B67B34"))
                    .ToList();
                return famEncounters.Count > 0;
            }
            return false;
        }
        public bool IsPartnerContact()
        {
            if (null != PreventEnroll && PreventEnroll.Value)
            {
                var partnerEncounters = Encounters
                    .Where(x => x.EncounterTypeId == new Guid("B262FDA4-877F-11E7-BB31-BE2E44B68B34") ||
                                x.EncounterTypeId == new Guid("B262FDA4-877F-11E7-BB31-BE2E44B69B34"))
                    .ToList();
                return partnerEncounters.Count > 0;
            }
            return false;
        }

        public bool IsInState(params LiveState[] states)
        {
            if (null != ClientStates && ClientStates.Any() && states.Length > 0)
            {
                var found = ClientStates.Where(x => states.Contains(x.Status)).ToList();
                return found.Count == states.Length;
            }
            return false;
        }

        public bool IsInAnyState(params LiveState[] states)
        {
            if (null != ClientStates && ClientStates.Any() && states.Length > 0)
            {
                var found = ClientStates.Where(x => states.Contains(x.Status)).ToList();
                return found.Count > 0;
            }

            return false;
        }

        public List<ClientState> GetStates(params LiveState[] states)
        {
            if (null != ClientStates && ClientStates.Any() && states.Length > 0)
            {
                return ClientStates.Where(x => states.Contains(x.Status)).ToList();
            }

            return new List<ClientState>();
        }
        public bool IsInState(Guid indexId, params LiveState[] states)
        {
            if (null != ClientStates && ClientStates.Any(x => null != x.IndexClientId && x.IndexClientId == indexId) &&
                states.Length > 0)
            {
                var found = ClientStates.Where(x => states.Contains(x.Status) && x.IndexClientId == indexId).ToList();
                return found.Count == states.Length;
            }

            return false;
        }
        public bool IsInAnyState(Guid indexId, params LiveState[] states)
        {
            if (null != ClientStates && ClientStates.Any(x => null != x.IndexClientId && x.IndexClientId == indexId) &&
                states.Length > 0)
            {
                var found = ClientStates.Where(x => states.Contains(x.Status) && x.IndexClientId == indexId).ToList();
                return found.Count > 0;
            }

            return false;
        }
        public override string ToString()
        {
            var info = $"{Id}({PersonId}) {MaritalStatus}|{KeyPop}";
            var ids = Identifiers.Count > 0 ? Identifiers.First().ToString(): "";
            return $"{info} {ids}";
        }
    }
}