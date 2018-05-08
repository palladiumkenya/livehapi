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

        public PATIENT_IDENTIFICATION()
        {
        }

        private PATIENT_IDENTIFICATION(List<INTERNAL_PATIENT_ID> internalPatientId, PATIENT_NAME patientName,
            string dateOfBirth, string dateOfBirthPrecision, int sex, List<int> keyPop, PATIENT_ADDRESS patientAddress,
            string phoneNumber, int maritalStatus, string registrationDate)
        {
            INTERNAL_PATIENT_ID = internalPatientId;
            PATIENT_NAME = patientName;
            DATE_OF_BIRTH = dateOfBirth;
            DATE_OF_BIRTH_PRECISION = dateOfBirthPrecision;
            SEX = sex;
            KEY_POP = keyPop;
            PATIENT_ADDRESS = patientAddress;
            PHONE_NUMBER = phoneNumber;
            MARITAL_STATUS = maritalStatus;
            REGISTRATION_DATE = registrationDate;
        }

        public static PATIENT_IDENTIFICATION Create(ClientStage clientStage)
        {
            return new PATIENT_IDENTIFICATION(

                Clients.INTERNAL_PATIENT_ID.Create(clientStage.ClientId,clientStage.Serial),
                PATIENT_NAME.Create(clientStage.FirstName, clientStage.MiddleName, clientStage.LastName),
                clientStage.DateOfBirth.ToIqDateOnly(),
                clientStage.DateOfBirthPrecision,
                clientStage.Sex,
                new List<int> { clientStage.KeyPop },
                PATIENT_ADDRESS.Create(clientStage.Landmark),
                clientStage.Phone,
                clientStage.MaritalStatus,
                clientStage.RegistrationDate.ToIqDateOnly());
        }
    }
}