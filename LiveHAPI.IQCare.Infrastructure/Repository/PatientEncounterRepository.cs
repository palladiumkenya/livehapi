using LiveHAPI.IQCare.Core.Interfaces.Repository;

namespace LiveHAPI.IQCare.Infrastructure.Repository
{
    public class PatientEncounterRepository : BaseRepository, IPatientEncounterRepository
    {
        public PatientEncounterRepository(EMRContext context) : base(context)
        {
        }
    }
}
