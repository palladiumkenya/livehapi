using LiveHAPI.IQCare.Core.Interfaces.Repository;

namespace LiveHAPI.IQCare.Infrastructure.Repository
{
    public class PatientRepository : BaseRepository, IPatientRepository
    {
        public PatientRepository(EMRContext context) : base(context)
        {
        }
        
    }
}
