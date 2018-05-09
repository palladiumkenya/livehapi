using System;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.Sync.Core.Exchange.Encounters
{
    public class NewLinkage
    {
        public string FACILITY { get; set; }
        public string HEALTH_WORKER { get; set; }
        public string CARDE { get; set; }
        public string DATE_ENROLLED { get; set; }
        public string CCC_NUMBER { get; set; }
        public string REMARKS { get; set; }

        public NewLinkage()
        {
        }

        private NewLinkage(string facility, string healthWorker, string carde, string dateEnrolled, string cccNumber, string remarks)
        {
            FACILITY = facility;
            HEALTH_WORKER = healthWorker;
            CARDE = carde;
            DATE_ENROLLED = dateEnrolled;
            CCC_NUMBER = cccNumber;
            REMARKS = remarks;
        }

        public static NewLinkage Create(string facility, string healthWorker, string carde, DateTime? dateEnrolled, string cccNumber, string remarks)
        {
            return new NewLinkage(facility,healthWorker,carde,dateEnrolled.ToIqDateOnly(),cccNumber,remarks);
        }

        public static NewLinkage Create(ClientLinkageStage stage1)
        {
           return new NewLinkage(
               stage1.Facility,stage1.HealthWorker,stage1.Carde,stage1.DateEnrolled,stage1.CccNumber,stage1.Remarks
               );
        }
    }
}