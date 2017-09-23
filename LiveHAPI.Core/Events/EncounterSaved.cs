using System;
using System.Collections.Generic;
using LiveHAPI.Core.Interfaces.Events;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Events
{
    public class EncounterSaved:IhEvent
    {
        public List<EncounterInfo> Encounters { get;}

        public EncounterSaved(List<EncounterInfo> encounters)
        {
            Encounters = encounters;
        }
    }
}