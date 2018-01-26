using System;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Network;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.Infrastructure.Repository
{
    public class PracticeRepository : BaseRepository<Practice, Guid>, IPracticeRepository
    {
        public PracticeRepository(LiveHAPIContext context) : base(context)
        {
        }

        public Practice GetDefault()
        {
            return Context.Practices.FirstOrDefault(x => x.IsDefault && x.PracticeTypeId == "Facility");
        }

        public Practice GetByCode(string code)
        {
            return Context.Practices.FirstOrDefault(x => x.Code.ToLower() == code.ToLower());
        }

        public void Sync(Practice practice)
        {
            var exisitngPractice = GetByCode(practice.Code);
            if (null != exisitngPractice)
            {
                exisitngPractice.UpdateTo(practice);
                Update(exisitngPractice);
                Save();
                if (practice.IsDefault)
                {
                    ResetDefault(exisitngPractice.Id);
                }
            }
            else
            {
                practice.MakeFacility();
                Insert(practice);
                Save();
                if (practice.IsDefault)
                {
                    ResetDefault(practice.Id);
                }
            }
        }

        public void ResetDefault(Guid practiceId)
        {
            var pracs = Context.Practices.ToList();
            foreach (var practice in pracs)
            {
                practice.IsDefault = practice.Id == practiceId;
            }

            Context.UpdateRange(pracs);
            Context.SaveChanges();
        }
    }
}