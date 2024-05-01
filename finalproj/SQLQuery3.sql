CREATE PROCEDURE spDeleteUser
    @Email VARCHAR(255)
AS
BEGIN
    DELETE FROM Users WHERE Email = @Email;
    SELECT @@ROWCOUNT AS 'EffectedRows'; 
END
