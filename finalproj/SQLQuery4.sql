ALTER PROCEDURE [dbo].[spLogInUser]
    @Email VARCHAR(255),
    @Password NVARCHAR(255)
AS
BEGIN
    SELECT 
        Username,
        FirstName,
        LastName,
        Email,
        Password, 
        DateOfBirth,
        Gender,
        TypeOfIBD,
        ProfilePicture
    FROM Users
    WHERE 
        Email = @Email AND Password = @Password
END
