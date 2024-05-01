ALTER PROCEDURE [dbo].[spInsertUser]
    @Username NVARCHAR(50),
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(255),
    @Password NVARCHAR(255),
    @DateOfBirth DATE,
    @Gender NVARCHAR(10),
    @TypeOfIBD NVARCHAR(50),
    @ProfilePicture NVARCHAR(255)
AS
BEGIN
    -- Insert user data into the Users table
    INSERT INTO [dbo].[Users] (
        [Username],
        [FirstName],
        [LastName],
        [Email],
        [Password],
        [DateOfBirth],
        [Gender],
        [TypeOfIBD],
        [ProfilePicture]
    ) VALUES (
        @Username,
        @FirstName,
        @LastName,
        @Email,
        @Password,
        @DateOfBirth,
        @Gender,
        @TypeOfIBD,
        @ProfilePicture
    );
END
