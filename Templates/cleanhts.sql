use master
GO
delete from iqcare.dbo.dtl_PatientContacts 
delete from iqcare.dbo.[DTL_PATIENTHOUSEHOLDINFO] 	
delete from iqcare.dbo.[DTL_RURALRESIDENCE] 	
delete from iqcare.dbo.[DTL_URBANRESIDENCE]	
delete from iqcare.dbo.[DTL_PATIENTHIVPREVCAREENROLLMENT]	
delete from iqcare.dbo.[DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362]	
delete from iqcare.dbo.[DTL_FBCUSTOMFIELD_LinkageAndTracking]	
delete from iqcare.dbo.[DTL_CUSTOMFORM_HTS Tracing_LinkageAndTracking]	
delete from iqcare.dbo.[DTL_CUSTOMFORM_HIV-Test 1_HTC_Lab_MOH_362]	
delete from iqcare.dbo.[DTL_CUSTOMFORM_HIV-Test 2_HTC_Lab_MOH_362]	
delete from iqcare.dbo.[DTL_FBCUSTOMFIELD_FamilyMemberTesting]
delete from iqcare.dbo.[DTL_CUSTOMFORM_Family Member Tracing Form_FamilyTracingForm]
delete from iqcare.dbo.[DTL_FBCUSTOMFIELD_PNSFORM]
delete from iqcare.dbo.[DTL_CUSTOMFORM_Contact Tracing and Outcomes_PNSTRACING]
delete from iqcare.dbo.ord_Visit 
delete from iqcare.dbo.mst_Patient 
delete from iqcare.dbo.lnk_patientprogramstart 
delete from iqcare.dbo.dtl_FamilyInfo 
GO
drop database LiveHAPI
GO