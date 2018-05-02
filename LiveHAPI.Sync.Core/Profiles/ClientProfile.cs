using AutoMapper;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Sync.Core.Model;

namespace LiveHAPI.Sync.Core.Profiles
{
    public class ClientProfile:Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientUser, User>()
                .ForMember(x => x.Source, o => o.MapFrom(s => s.UserFirstName))
                .ForMember(x => x.SourceSys, o => o.MapFrom(s => s.UserLastName))
                .ForMember(x => x.SourceRef, o => o.MapFrom(s => s.UserId));

            CreateMap<ClientFacility, Practice>()
                .ForMember(x => x.Code, o => o.MapFrom(s => s.PosID))
                .ForMember(x => x.IsDefault, o => o.MapFrom(s => s.Preferred.HasValue && s.Preferred == 1))
                .ForMember(x => x.Name, o => o.MapFrom(s => s.FacilityName));
        }
    }
}