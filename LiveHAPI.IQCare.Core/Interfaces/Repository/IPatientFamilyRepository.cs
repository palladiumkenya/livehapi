using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.IQCare.Core.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.IQCare.Core.Interfaces.Repository
{
    public interface IPatientFamilyRepository
    {
        IEnumerable<PatientFamily> GetMembers(int patientPk);
    }
}