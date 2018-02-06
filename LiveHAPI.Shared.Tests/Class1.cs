using System;
using System.Collections.Generic;
using System.Text;

namespace LiveHAPI.Shared.Tests
{
    class Class1
    {
        public Class1()
        {
            var s = @"
[
  {
    ^Id^: ^6d53f1a8-944b-4972-99a9-a87b00a17319^,
    ^ClientId^: ^2475c516-0147-4faa-818a-a87b00a111c7^,
    ^FormId^: ^b25ec112-852f-11e7-bb31-be2e46b06b37^,
    ^EncounterTypeId^: ^b262fda4-877f-11e7-bb31-be2e44b68b34^,
    ^EncounterDate^: ^\/Date(1517564869201+0300)\/^,
    ^ProviderId^: ^158790da-a5c7-4a11-9d49-a7b7016df234^,
    ^DeviceId^: ^b3312abd-b6de-4037-bb65-a87b009fcd7a^,
    ^PracticeId^: ^2cb820d5-43a9-4018-b651-a87b0097f173^,
    ^Started^: ^\/Date(1517564869206+0300)\/^,
    ^Stopped^: null,
    ^KeyPop^: ^NA^,
    ^OtherKeyPop^: null,
    ^Phone^: ^^,
    ^Obses^: [
      
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
      {
        ^ScreeningDate^: ^\/Date(1517518800000+0300)\/^,
        ^PnsAccepted^: ^b25ed04e-852f-11e7-bb31-be2e44b06b34^,
        ^IPVScreening^: ^b25eccd4-852f-11e7-bb31-be2e44b06b34^,
        ^PhysicalAssult^: ^b25eccd4-852f-11e7-bb31-be2e44b06b34^,
        ^Threatened^: ^b25eccd4-852f-11e7-bb31-be2e44b06b34^,
        ^SexuallyUncomfortable^: ^b25eccd4-852f-11e7-bb31-be2e44b06b34^,
        ^IPVOutcome^: ^a577c7de-0052-11e8-aa69-0ed5f89f718b^,
        ^Occupation^: ^farmer^,
        ^PNSRealtionship^: ^74907dae-0053-11e8-ba89-0ed5f89f718b^,
        ^LivingWithClient^: ^b25ed04e-852f-11e7-bb31-be2e44b06b34^,
        ^HivStatus^: ^b25efb78-852f-11e7-bb31-be2e44b06b34^,
        ^PNSApproach^: ^cdeaf184-0055-11e8-ba89-0ed5f89f718b^,
        ^Eligibility^: ^b25eccd4-852f-11e7-bb31-be2e44b06b34^,
        ^BookingDate^: ^\/Date(1522357200000+0300)\/^,
        ^Remarks^: null,
        ^EncounterId^: ^6d53f1a8-944b-4972-99a9-a87b00a17319^,
        ^Id^: ^edc04cf5-f1d5-441e-94c9-a87b00a19169^,
        ^Voided^: false
      }
    ],
    ^ObsPartnerTraceResults^: [
      
    ],
    ^UserId^: ^61a9e04c-2ed0-414a-9387-a7b7016df233^,
    ^IsComplete^: false
  },
  {
    ^Id^: ^a26dd6b4-20c3-45db-905f-a87b00a1972a^,
    ^ClientId^: ^2475c516-0147-4faa-818a-a87b00a111c7^,
    ^FormId^: ^b25ec112-852f-11e7-bb31-be2e46b06b38^,
    ^EncounterTypeId^: ^b262fda4-877f-11e7-bb31-be2e44b69b34^,
    ^EncounterDate^: ^\/Date(1517564899978+0300)\/^,
    ^ProviderId^: ^158790da-a5c7-4a11-9d49-a7b7016df234^,
    ^DeviceId^: ^b3312abd-b6de-4037-bb65-a87b009fcd7a^,
    ^PracticeId^: ^2cb820d5-43a9-4018-b651-a87b0097f173^,
    ^Started^: ^\/Date(1517564899978+0300)\/^,
    ^Stopped^: null,
    ^KeyPop^: ^NA^,
    ^OtherKeyPop^: null,
    ^Phone^: ^^,
    ^Obses^: [
      
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
      {
        ^Date^: ^\/Date(1517518800000+0300)\/^,
        ^Mode^: ^b25f136a-852f-11e7-bb31-be2e44b06b34^,
        ^ModeDisplay^: null,
        ^Outcome^: ^b25f102c-852f-11e7-bb31-be2e44b06b34^,
        ^OutcomeDisplay^: null,
        ^Consent^: ^b25ed04e-852f-11e7-bb31-be2e44b06b34^,
        ^BookingDate^: ^\/Date(1521925200000+0300)\/^,
        ^EncounterId^: ^a26dd6b4-20c3-45db-905f-a87b00a1972a^,
        ^Id^: ^383c414b-e1ed-4ea1-99ab-a87b00a1a6f2^,
        ^Voided^: false
      },
      {
        ^Date^: ^\/Date(1517518800000+0300)\/^,
        ^Mode^: ^b25f159a-852f-11e7-bb31-be2e44b06b34^,
        ^ModeDisplay^: null,
        ^Outcome^: ^b25f0a50-852f-11e7-bb31-be2e44b06b34^,
        ^OutcomeDisplay^: null,
        ^Consent^: ^b25ed04e-852f-11e7-bb31-be2e44b06b34^,
        ^BookingDate^: ^\/Date(1521666000000+0300)\/^,
        ^EncounterId^: ^a26dd6b4-20c3-45db-905f-a87b00a1972a^,
        ^Id^: ^60930a08-b1d5-492d-ab09-a87b00a1afb3^,
        ^Voided^: false
      }
    ],
    ^UserId^: ^61a9e04c-2ed0-414a-9387-a7b7016df233^,
    ^IsComplete^: false
  }
]
";
        }
    }
}
