using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.Encounters
{
    public class PSmartStore: Entity<Guid>
    {
        public string Shr { get; set; }
        public DateTime? Date_Created { get; set; }
        [MaxLength(100)]
        public string Status { get; set; }
        public DateTime? Status_Date { get; set; }
        public Guid Uuid { get; set; }

        public PSmartStore()
        {
        }

        private PSmartStore(string shr, DateTime? dateCreated, string status, DateTime? statusDate, Guid uuid)
        {
            Shr = shr;
            Date_Created = dateCreated;
            Status = status;
            Status_Date = statusDate;
            Uuid = uuid;
        }

        private PSmartStore(Guid id, string shr, DateTime? dateCreated, string status, DateTime? statusDate, Guid uuid) : base(id)
        {
            Shr = shr;
            Date_Created = dateCreated;
            Status = status;
            Status_Date = statusDate;
            Uuid = uuid;
        }

        public static PSmartStore Create(PSmartStoreInfo pSmartStoreInfo)
        {
            return new PSmartStore(pSmartStoreInfo.Id, pSmartStoreInfo.Shr, pSmartStoreInfo.Date_Created, pSmartStoreInfo.Status, pSmartStoreInfo.Status_Date, pSmartStoreInfo.Uuid);
        }
        public static List<PSmartStore> Create(List<PSmartStoreInfo> pSmartStoreInfo)
        {
            var list=new List<PSmartStore>();
            foreach (var pSmart in pSmartStoreInfo)
            {
                list.Add(Create(pSmart));
            }

            return list;
        }
    }
}