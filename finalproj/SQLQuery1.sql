ALTER PROCEDURE spReadOneUser
    @Email NVARCHAR(255)
AS
BEGIN
    SELECT 
        [UserID],
        [Username],
        [FirstName],
        [LastName],
        [Email],
        [Password],
        [DateOfBirth],
        [Gender],
        [TypeOfIBD],
        [ProfilePicture]
    FROM Users
    WHERE Email = @Email
END;