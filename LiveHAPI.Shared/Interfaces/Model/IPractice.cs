using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IPractice
    {
     
        string Code { get; set; }
        string Name { get; set; }
        string PracticeTypeId { get; set; }
        int? CountyId { get; set; }
    }
}