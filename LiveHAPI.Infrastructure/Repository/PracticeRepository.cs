using System;
using System.Collections.Generic;
using System.Linq;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Shared.Custom;
using Microsoft.EntityFrameworkCore;
using Z.Dapper.Plus;

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

        public IEnumerable<Practice> GetAllDefault()
        {
            return Context.Practices.Where(x => x.PracticeTypeId == "Facility");
        }

        public Practice GetByCode(string code)
        {
            return Context.Practices.FirstOrDefault(x => x.Code.ToLower() == code.ToLower());
        }

        public Practice GetByFacilityCode(string code)
        {
            Practice practice;
            using (var con=GetDbConnection())
            {
                 practice = con.GetAll<Practice>().FirstOrDefault(x => x.Code.ToLower() == code.ToLower());
            }
            return practice;
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
        public void Sync(IEnumerable<Practice> practices)
        {
            var updateList=new List<Practice>();
            var insertList=new List<Practice>();
            var defaultList=new List<Guid>();

            foreach (var practice in practices)
            {
                var exisitngPractice = GetByFacilityCode(practice.Code);
                if (null != exisitngPractice)
                {
                    exisitngPractice.UpdateTo(practice);
                    updateList.Add(exisitngPractice);
                    if(practice.IsDefault)
                        defaultList.Add(exisitngPractice.Id);
                }
                else
                {
                    practice.MakeFacility();
                    insertList.Add(practice);
                    if (practice.IsDefault)
                        defaultList.Add(practice.Id);
                }
            }

            using (var con = GetDbConnection())
            {
                con.BulkUpdate(updateList);
                con.BulkInsert(insertList);
            }

            ResetDefault(defaultList);
        }

        public void ResetDefault(List<Guid> practiceIds)
        {
            var defaultId = practiceIds.FirstOrDefault();
            if (!defaultId.IsNullOrEmpty())
            {
                var exisitngDefaultPracs = GetDbConnection().GetAll<Practice>()
                    .Where(x => x.IsDefault).ToList();
                if (exisitngDefaultPracs.Any())
                {
                    foreach (var exisitngDefaultPrac in exisitngDefaultPracs)
                    {
                        exisitngDefaultPrac.IsDefault = false;
                    }
                    using (var con = GetDbConnection())
                    {
                        con.BulkUpdate(exisitngDefaultPracs);
                    }
                }

                var newDefaultPrac = GetDbConnection().Get<Practice>(defaultId);
                if (null != newDefaultPrac)
                {
                    newDefaultPrac.IsDefault = true;
                    using (var con = GetDbConnection())
                    {
                        con.BulkUpdate(newDefaultPrac);
                    }
                }
            }
        }
    }
}