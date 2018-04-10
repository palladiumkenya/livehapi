using System;
using System.Collections.Generic;
using LiveHAPI.Core.Interfaces.Events;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Events
{
    public class PSmartStoreSaved : IhEvent
    {
        public List<PSmartStoreInfo> Encounters { get;}

        public PSmartStoreSaved(List<PSmartStoreInfo> encounters)
        {
            Encounters = encounters;
        }
    }
}