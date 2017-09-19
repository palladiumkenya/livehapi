using System;
using System.Collections.Generic;
using LiveHAPI.Core.Interfaces.Events;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Events
{
    public class EncounterSaved:IhEvent
    {
        public List<Guid> EncounterIds { get;}

        public EncounterSaved(List<Guid> encounterIds)
        {
            EncounterIds = encounterIds;
        }
    }
}