using System;
using System.Collections.Generic;
using System.Text;

namespace LiveHAPI.Shared.Tests.TestHelpers
{
    class Class1
    {
        public Class1()
        {
            var json = @"
            {
    ^Id^: ^f5a650d5-94e2-4aa6-a33f-a87a00a6afac^,
    ^MaritalStatus^: ^MM^,
    ^KeyPop^: ^FFx^,
    ^OtherKeyPop^: null,
    ^PracticeId^: ^9639529b-98a5-4930-be6b-a87a008c2c04^,
    ^PracticeCode^: null,
    ^Person^: {
      ^FirstName^: ^john^,
      ^MiddleName^: ^kimani^,
      ^LastName^: ^mwangi^,
      ^Gender^: ^M^,
      ^BirthDate^: ^\/Date(-912481200000+0300)\/^,
      ^BirthDateEstimated^: false,
      ^Email^: ^^,
      ^Addresses^: [
        {
          ^Landmark^: ^Kayole^,
          ^CountyId^: null,
          ^Preferred^: false,
          ^PersonId^: ^84d87628-a719-4bf8-b8c0-a87a00a6afa8^,
          ^Id^: ^e241cdaf-b99b-4e2f-9ef9-a87a00a6afab^,
          ^Voided^: false
        }
      ],
      ^Contacts^: [
        {
          ^Phone^: 755123123,
          ^Preferred^: true,
          ^PersonId^: ^84d87628-a719-4bf8-b8c0-a87a00a6afa8^,
          ^Id^: ^ec569cd8-4e32-4344-a518-a87a00a6afa9^,
          ^Voided^: false
        }
      ],
      ^Id^: ^84d87628-a719-4bf8-b8c0-a87a00a6afa8^,
      ^Voided^: false
    },
    ^IsFamilyMember^: false,
    ^IsPartner^: false,
    ^Identifiers^: [
      {
        ^IdentifierTypeId^: ^Serial^,
        ^Identifier^: ^4555^,
        ^RegistrationDate^: ^\/Date(1517432400000+0300)\/^,
        ^Preferred^: true,
        ^ClientId^: ^f5a650d5-94e2-4aa6-a33f-a87a00a6afac^,
        ^Id^: ^809904ed-fa9b-425e-826a-a87a00a6afad^,
        ^Voided^: false
      }
    ],
    ^Relationships^: [
      
    ]
  }
            ";



            var jsonn = @"
[
{
  ^Id^: ^006a1dee-1c36-475d-a44b-a87a00a8ae87^,
  ^ClientId^: ^f5a650d5-94e2-4aa6-a33f-a87a00a6afac^,
  ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
  ^EncounterTypeId^: ^7e5164a6-6b99-11e7-907b-a6006ad3dba0^,
  ^EncounterDate^: ^\/Date(1517480049088+0300)\/^,
  ^ProviderId^: ^158790da-a5c7-4a11-9d49-a7b7016df234^,
  ^DeviceId^: ^60531e20-5ec5-46c8-be4e-a87a00919f2a^,
  ^PracticeId^: ^9639529b-98a5-4930-be6b-a87a008c2c04^,
  ^Started^: ^\/Date(1517480049088+0300)\/^,
  ^Stopped^: null,
  ^KeyPop^: ^FFx^,
  ^OtherKeyPop^: null,
  ^Phone^: ^755123123^,
  ^Obses^: [
    {
      ^QuestionId^: ^b26039a1-852f-11e7-bb31-be2e44b06b34^,
      ^ObsDate^: ^\/Date(1517480109817+0300)\/^,
      ^ValueText^: null,
      ^ValueNumeric^: null,
      ^ValueCoded^: ^0e054d84-ff64-11e7-8be5-0ed5f89f718b^,
      ^ValueMultiCoded^: null,
      ^ValueDateTime^: null,
      ^EncounterId^: ^006a1dee-1c36-475d-a44b-a87a00a8ae87^,
      ^IsNull^: false,
      ^Id^: ^d4e131b0-439e-4adb-935a-a87a00a8f5b2^,
      ^Voided^: false
    },
    {
      ^QuestionId^: ^b2603772-852f-11e7-bb31-be2e44b06b34^,
      ^ObsDate^: ^\/Date(1517480109873+0300)\/^,
      ^ValueText^: null,
      ^ValueNumeric^: null,
      ^ValueCoded^: ^b25eccd4-852f-11e7-bb31-be2e44b06b34^,
      ^ValueMultiCoded^: null,
      ^ValueDateTime^: null,
      ^EncounterId^: ^006a1dee-1c36-475d-a44b-a87a00a8ae87^,
      ^IsNull^: false,
      ^Id^: ^1f6a3bb1-4398-47e6-97ee-a87a00a8f5c3^,
      ^Voided^: false
    },
    {
      ^QuestionId^: ^b26039a2-852f-11e7-bb31-be2e44b06b34^,
      ^ObsDate^: ^\/Date(1517480109894+0300)\/^,
      ^ValueText^: null,
      ^ValueNumeric^: 40.0,
      ^ValueCoded^: null,
      ^ValueMultiCoded^: null,
      ^ValueDateTime^: null,
      ^EncounterId^: ^006a1dee-1c36-475d-a44b-a87a00a8ae87^,
      ^IsNull^: false,
      ^Id^: ^e84991ef-5e9e-451f-b481-a87a00a8f5c9^,
      ^Voided^: false
    },
    {
      ^QuestionId^: ^b2603773-852f-11e7-bb31-be2e44b06b34^,
      ^ObsDate^: ^\/Date(1517480109911+0300)\/^,
      ^ValueText^: null,
      ^ValueNumeric^: null,
      ^ValueCoded^: ^b25eccd4-852f-11e7-bb31-be2e44b06b34^,
      ^ValueMultiCoded^: null,
      ^ValueDateTime^: null,
      ^EncounterId^: ^006a1dee-1c36-475d-a44b-a87a00a8ae87^,
      ^IsNull^: false,
      ^Id^: ^cd26a4ec-2a93-4f52-a92b-a87a00a8f5ce^,
      ^Voided^: false
    },
    {
      ^QuestionId^: ^b260695c-852f-11e7-bb31-be2e44b06b34^,
      ^ObsDate^: ^\/Date(1517480109930+0300)\/^,
      ^ValueText^: null,
      ^ValueNumeric^: null,
      ^ValueCoded^: ^b25eccd4-852f-11e7-bb31-be2e44b06b34^,
      ^ValueMultiCoded^: null,
      ^ValueDateTime^: null,
      ^EncounterId^: ^006a1dee-1c36-475d-a44b-a87a00a8ae87^,
      ^IsNull^: false,
      ^Id^: ^7fc7b5cb-7341-4a87-a7b1-a87a00a8f5d4^,
      ^Voided^: false
    },
    {
      ^QuestionId^: ^b2603c5e-852f-11e7-bb31-be2e44b06b34^,
      ^ObsDate^: ^\/Date(1517480109949+0300)\/^,
      ^ValueText^: null,
      ^ValueNumeric^: null,
      ^ValueCoded^: null,
      ^ValueMultiCoded^: ^b25ed332-852f-11e7-bb31-be2e44b06b34,b25ed648-852f-11e7-bb31-be2e44b06b34^,
      ^ValueDateTime^: null,
      ^EncounterId^: ^006a1dee-1c36-475d-a44b-a87a00a8ae87^,
      ^IsNull^: false,
      ^Id^: ^457cb4b1-f99f-4786-8900-a87a00a8f5d9^,
      ^Voided^: false
    },
    {
      ^QuestionId^: ^b2603dc6-852f-11e7-bb31-be2e44b06b34^,
      ^ObsDate^: ^\/Date(1517480109967+0300)\/^,
      ^ValueText^: null,
      ^ValueNumeric^: null,
      ^ValueCoded^: ^b25eccd4-852f-11e7-bb31-be2e44b06b34^,
      ^ValueMultiCoded^: null,
      ^ValueDateTime^: null,
      ^EncounterId^: ^006a1dee-1c36-475d-a44b-a87a00a8ae87^,
      ^IsNull^: false,
      ^Id^: ^9f63150d-2518-4172-8b9a-a87a00a8f5df^,
      ^Voided^: false
    },
    {
      ^QuestionId^: ^b260401e-852f-11e7-bb31-be2e44b06b34^,
      ^ObsDate^: ^\/Date(1517480109983+0300)\/^,
      ^ValueText^: null,
      ^ValueNumeric^: null,
      ^ValueCoded^: ^b25ede36-852f-11e7-bb31-be2e44b06b34^,
      ^ValueMultiCoded^: null,
      ^ValueDateTime^: null,
      ^EncounterId^: ^006a1dee-1c36-475d-a44b-a87a00a8ae87^,
      ^IsNull^: false,
      ^Id^: ^264d5bd3-1bf6-4207-a92a-a87a00a8f5e4^,
      ^Voided^: false
    },
    {
      ^QuestionId^: ^b260417c-852f-11e7-bb31-be2e44b06b34^,
      ^ObsDate^: ^\/Date(1517480110004+0300)\/^,
      ^ValueText^: null,
      ^ValueNumeric^: null,
      ^ValueCoded^: ^b25eed36-852f-11e7-bb31-be2e44b06b34^,
      ^ValueMultiCoded^: null,
      ^ValueDateTime^: null,
      ^EncounterId^: ^006a1dee-1c36-475d-a44b-a87a00a8ae87^,
      ^IsNull^: false,
      ^Id^: ^6d2c1db5-80c1-4397-b18c-a87a00a8f5ea^,
      ^Voided^: false
    },
    {
      ^QuestionId^: ^b2605f54-852f-11e7-bb31-be2e44b06b34^,
      ^ObsDate^: ^\/Date(1517480110023+0300)\/^,
      ^ValueText^: null,
      ^ValueNumeric^: null,
      ^ValueCoded^: ^b25ef63c-852f-11e7-bb31-be2e44b06b34^,
      ^ValueMultiCoded^: null,
      ^ValueDateTime^: null,
      ^EncounterId^: ^006a1dee-1c36-475d-a44b-a87a00a8ae87^,
      ^IsNull^: false,
      ^Id^: ^16ea501c-ee1f-43ec-9afe-a87a00a8f5f0^,
      ^Voided^: false
    },
    {
      ^QuestionId^: ^b260665c-852f-11e7-bb31-be2e44b06b34^,
      ^ObsDate^: ^\/Date(1517480110044+0300)\/^,
      ^ValueText^: ^no pretest comments^,
      ^ValueNumeric^: null,
      ^ValueCoded^: null,
      ^ValueMultiCoded^: null,
      ^ValueDateTime^: null,
      ^EncounterId^: ^006a1dee-1c36-475d-a44b-a87a00a8ae87^,
      ^IsNull^: false,
      ^Id^: ^06009242-4845-45d6-ad59-a87a00a8f5f6^,
      ^Voided^: false
    }
  ],
  ^ObsTestResults^: [
    
  ],
  ^ObsFinalTestResults^: [
    
  ],
  ^ObsTraceResults^: [
    
  ],
  ^ObsLinkages^: [
    
  ],
  ^ObsMemberScreenings^: [
    
  ],
  ^ObsFamilyTraceResults^: [
    
  ],
  ^ObsPartnerScreenings^: [
    
  ],
  ^ObsPartnerTraceResults^: [
    
  ],
  ^UserId^: ^8c746eb4-c807-4809-bb69-a87a00987c26^,
  ^IsComplete^: true
},
{
  ^Id^: ^2a7bd6d3-5209-4416-bd51-a87a00a8fb7b^,
  ^ClientId^: ^f5a650d5-94e2-4aa6-a33f-a87a00a6afac^,
  ^FormId^: ^b25ec568-852f-11e7-bb31-be2e44b06b34^,
  ^EncounterTypeId^: ^b262f4ee-852f-11e7-bb31-be2e44b06b34^,
  ^EncounterDate^: ^\/Date(1517480114756+0300)\/^,
  ^ProviderId^: ^158790da-a5c7-4a11-9d49-a7b7016df234^,
  ^DeviceId^: ^60531e20-5ec5-46c8-be4e-a87a00919f2a^,
  ^PracticeId^: ^9639529b-98a5-4930-be6b-a87a008c2c04^,
  ^Started^: ^\/Date(1517480114756+0300)\/^,
  ^Stopped^: null,
  ^KeyPop^: ^FFx^,
  ^OtherKeyPop^: null,
  ^Phone^: ^755123123^,
  ^Obses^: [
    
  ],
  ^ObsTestResults^: [
    {
      ^TestName^: ^HIV Test 1^,
      ^Attempt^: 0,
      ^Kit^: ^b25f0456-852f-11e7-bb31-be2e44b06b34^,
      ^KitDisplay^: null,
      ^KitOther^: ^^,
      ^LotNumber^: ^4444444^,
      ^Expiry^: ^\/Date(1548968400000+0300)\/^,
      ^Result^: ^b25efd8a-852f-11e7-bb31-be2e44b06b34^,
      ^ResultCode^: ^P^,
      ^ResultDisplay^: null,
      ^IsValid^: true,
      ^EncounterId^: ^2a7bd6d3-5209-4416-bd51-a87a00a8fb7b^,
      ^Id^: ^3209fb5e-d7ff-4ce4-bf19-a87a00a91074^,
      ^Voided^: false
    },
    {
      ^TestName^: ^HIV Test 2^,
      ^Attempt^: 0,
      ^Kit^: ^b25f05aa-852f-11e7-bb31-be2e44b06b34^,
      ^KitDisplay^: null,
      ^KitOther^: ^^,
      ^LotNumber^: ^6777^,
      ^Expiry^: ^\/Date(1548968400000+0300)\/^,
      ^Result^: ^b25f001e-852f-11e7-bb31-be2e44b06b34^,
      ^ResultCode^: ^I^,
      ^ResultDisplay^: null,
      ^IsValid^: false,
      ^EncounterId^: ^2a7bd6d3-5209-4416-bd51-a87a00a8fb7b^,
      ^Id^: ^bc94f738-9d77-4e01-8c84-a87a00a92535^,
      ^Voided^: false
    },
    {
      ^TestName^: ^HIV Test 2^,
      ^Attempt^: 0,
      ^Kit^: ^b25f0776-852f-11e7-bb31-be2e44b06b34^,
      ^KitDisplay^: null,
      ^KitOther^: ^Kit xyz^,
      ^LotNumber^: ^566777^,
      ^Expiry^: ^\/Date(1548968400000+0300)\/^,
      ^Result^: ^b25efd8a-852f-11e7-bb31-be2e44b06b34^,
      ^ResultCode^: ^P^,
      ^ResultDisplay^: null,
      ^IsValid^: true,
      ^EncounterId^: ^2a7bd6d3-5209-4416-bd51-a87a00a8fb7b^,
      ^Id^: ^b641f4bd-d2c1-4aa7-8861-a87a00a94603^,
      ^Voided^: false
    }
  ],
  ^ObsFinalTestResults^: [
    {
      ^FirstTestResult^: ^b25efd8a-852f-11e7-bb31-be2e44b06b34^,
      ^FirstTestResultCode^: null,
      ^SecondTestResult^: ^b25efd8a-852f-11e7-bb31-be2e44b06b34^,
      ^SecondTestResultCode^: null,
      ^FinalResult^: ^b25efd8a-852f-11e7-bb31-be2e44b06b34^,
      ^FinalResultCode^: null,
      ^ResultGiven^: ^b25eccd4-852f-11e7-bb31-be2e44b06b34^,
      ^CoupleDiscordant^: ^b25ed1c0-852f-11e7-bb31-be2e44b06b34^,
      ^SelfTestOption^: ^00000000-0000-0000-0000-000000000000^,
      ^Remarks^: ^Maun Maun^,
      ^EncounterId^: ^2a7bd6d3-5209-4416-bd51-a87a00a8fb7b^,
      ^Id^: ^6407a40f-70e3-42cf-a55c-a87a00a91081^,
      ^Voided^: false
    }
  ],
  ^ObsTraceResults^: [
    
  ],
  ^ObsLinkages^: [
    
  ],
  ^ObsMemberScreenings^: [
    
  ],
  ^ObsFamilyTraceResults^: [
    
  ],
  ^ObsPartnerScreenings^: [
    
  ],
  ^ObsPartnerTraceResults^: [
    
  ],
  ^UserId^: ^8c746eb4-c807-4809-bb69-a87a00987c26^,
  ^IsComplete^: false
},
{
  ^Id^: ^dac37677-54b2-4271-b6fb-a87a00a962b7^,
  ^ClientId^: ^f5a650d5-94e2-4aa6-a33f-a87a00a6afac^,
  ^FormId^: ^b25ec112-852f-11e7-bb31-be2e44b06b34^,
  ^EncounterTypeId^: ^b262fc32-852f-11e7-bb31-be2e44b06b34^,
  ^EncounterDate^: ^\/Date(1517480202847+0300)\/^,
  ^ProviderId^: ^158790da-a5c7-4a11-9d49-a7b7016df234^,
  ^DeviceId^: ^60531e20-5ec5-46c8-be4e-a87a00919f2a^,
  ^PracticeId^: ^9639529b-98a5-4930-be6b-a87a008c2c04^,
  ^Started^: ^\/Date(1517480202847+0300)\/^,
  ^Stopped^: null,
  ^KeyPop^: ^FFx^,
  ^OtherKeyPop^: null,
  ^Phone^: ^755123123^,
  ^Obses^: [
    
  ],
  ^ObsTestResults^: [
    
  ],
  ^ObsFinalTestResults^: [
    
  ],
  ^ObsTraceResults^: [
    {
      ^Date^: ^\/Date(1517432400000+0300)\/^,
      ^Mode^: ^b25f159a-852f-11e7-bb31-be2e44b06b34^,
      ^ModeDisplay^: null,
      ^Outcome^: ^b25f0a51-852f-11e7-bb31-be2e44b06b34^,
      ^OutcomeDisplay^: null,
      ^EncounterId^: ^dac37677-54b2-4271-b6fb-a87a00a962b7^,
      ^Id^: ^4a30a12b-e1fb-4fe4-82e4-a87a00a988d5^,
      ^Voided^: false
    }
  ],
  ^ObsLinkages^: [
    
  ],
  ^ObsMemberScreenings^: [
    
  ],
  ^ObsFamilyTraceResults^: [
    
  ],
  ^ObsPartnerScreenings^: [
    
  ],
  ^ObsPartnerTraceResults^: [
    
  ],
  ^UserId^: ^8c746eb4-c807-4809-bb69-a87a00987c26^,
  ^IsComplete^: false
}
]

";
        }
    }
}
