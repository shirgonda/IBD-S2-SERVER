ALTER PROCEDURE spReadUser
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
    FROM Users;
END;