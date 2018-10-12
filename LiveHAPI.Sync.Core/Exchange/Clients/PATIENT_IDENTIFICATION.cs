using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.Sync.Core.Exchange.Clients
{
    public class PATIENT_IDENTIFICATION
    {
        public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; } = new List<INTERNAL_PATIENT_ID>();
        public PATIENT_NAME PATIENT_NAME { get; set; }
        public string DATE_OF_BIRTH { get; set; }
        public string DATE_OF_BIRTH_PRECISION { get; set; }
        public int SEX { get; set; }
        public List<int> KEY_POP { get; set; } = new List<int>();
        public PATIENT_ADDRESS PATIENT_ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public int MARITAL_STATUS { get; set; }
        public string REGISTRATION_DATE { get; set; }
        
        //TODO add user_id
        public int USER_ID { get; set; } = 1;

        public PATIENT_IDENTIFICATION()
        {
        }

        protected PATIENT_IDENTIFICATION(ClientStage clientStage)
        {
            INTERNAL_PATIENT_ID = Clients.INTERNAL_PATIENT_ID.Create(clientStage.ClientId, clientStage.Serial);
            PATIENT_NAME = PATIENT_NAME.Create(clientStage.FirstName, clientStage.MiddleName, clientStage.LastName, clientStage.NickName);
            DATE_OF_BIRTH = clientStage.DateOfBirth.ToIqDateOnly();
            DATE_OF_BIRTH_PRECISION = clientStage.DateOfBirthPrecision;
            SEX = clientStage.Sex;
            KEY_POP = new List<int> { clientStage.KeyPop };
            PATIENT_ADDRESS = PATIENT_ADDRESS.Create(clientStage.Landmark);
            PHONE_NUMBER = clientStage.Phone;
            MARITAL_STATUS = clientStage.MaritalStatus;
            REGISTRATION_DATE = clientStage.RegistrationDate.ToIqDateOnly();
        }
        protected PATIENT_IDENTIFICATION(ClientStage clientStage, Guid indexClientId) : this(clientStage)
        {
            INTERNAL_PATIENT_ID = Clients.INTERNAL_PATIENT_ID.CreateContact(clientStage.ClientId, indexClientId);
        }

        public static PATIENT_IDENTIFICATION Create(ClientStage clientStage)
        {
            return new PATIENT_IDENTIFICATION(clientStage);
        }    
    }
}