DECLARE @Username as NVARCHAR(50);
DECLARE @Password as NVARCHAR(50);

set @Username = 'ezelljd';
set @Password = 12345;

exec sp_simpleLogin @Username, @Password;