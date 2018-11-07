using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IClient
    {
        
        string MaritalStatus { get; set; }
        string KeyPop { get; set; }
        string OtherKeyPop { get; set; }
        bool? IsFamilyMember { get; set; }
        bool? IsPartner { get; set; }
        Guid? Education { get; set; }
        Guid? Completion { get; set; }
        Guid? Occupation { get; set; }
    }
}