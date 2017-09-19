using System;
using System.Collections.Generic;
using LiveHAPI.Core.Interfaces.Events;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Core.Events
{
    public class EncounterSaved:IhEvent
    {
        public List<Guid> EncounterIds { get; }
        public List<Encounter> Encounters { get;}

        public EncounterSaved(List<Guid> encounterIds)
        {
            EncounterIds = encounterIds;
        }
        public EncounterSaved(List<Encounter> encounters)
        {
            Encounters = encounters;
        }
    }
}