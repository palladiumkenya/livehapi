delete from clients;
DELETE from Persons where id in(
SELECT PersonId
  FROM [LiveHAPIDev].[dbo].[PersonNames]
  where not(FirstName like N'interop' or FirstName like N'system'))

DElete from InvalidMessages