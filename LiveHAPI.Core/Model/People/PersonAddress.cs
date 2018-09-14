using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.People
{
    public class PersonAddress : Entity<Guid>, IAddress, ISourceIdentity
    {
        [MaxLength(200)]
        public string Landmark { get; set; }
        public int? CountyId { get; set; }
        public int? SubCountyId { get; set; }
        public int? WardId { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        [MaxLength(50)]
        public string Source { get; set; }
        [MaxLength(50)]
        public string SourceRef { get; set; }
        [MaxLength(50)]
        public string SourceSys { get; set; }

        public bool Preferred { get; set; }
        public Guid PersonId { get; set; }

        public PersonAddress()
        {
            Id = LiveGuid.NewGuid();
        }

        private PersonAddress(string landmark, int? countyId, decimal? lat, decimal? lng, int? subCountyId, int? wardId) :this()
        {
            Landmark = landmark;
            CountyId = countyId;
            Lat = lat;
            Lng = lng;
            SubCountyId = subCountyId;
            WardId = wardId;
        }

        public void ChangeTo(PersonAddress address)
        {
            Landmark = address.Landmark;
            CountyId = address.CountyId;
            Lat = address.Lat;
            Lng = address.Lng;
            SubCountyId = address.SubCountyId;
            WardId = address.WardId;
        }

        public static PersonAddress Create(AddressInfo address)
        {
            return new PersonAddress(address.Landmark, address.CountyId, address.Lat, address.Lng, address.SubCountyId,
                address.WardId);
        }

        public static List<PersonAddress> Create(PersonInfo personInfo)
        {
            var list = new List<PersonAddress>();

            foreach (var address in personInfo.Addresses)
            { 
                list.Add(Create(address));
            }
            return list;
        }

        public override string ToString()
        {
            return $"{Landmark},{CountyId} [{Lng},{Lat}]   ({PersonId})";
        }

        public static List<AddressInfo> GetAddressInfos(List<PersonAddress> addresses)
        {
            var list=new List<AddressInfo>();
            foreach (var personAddress in addresses)
            {
                    list.Add(personAddress.GetAddressInfo());
            }

            return list;
        }

        private AddressInfo GetAddressInfo()
        {
            return new AddressInfo(Id, Landmark, CountyId, Lat, Lng, PersonId, SubCountyId, WardId);
        }
    }
}