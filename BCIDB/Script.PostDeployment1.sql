/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS (SELECT TOP 1 1 FROM tblUser)
INSERT INTO tblUser (FirstName, LastName, Email, Password)
VALUES ('Ardall', 'Leonardo', 'bci.test@mailinator.com', 'test')

IF NOT EXISTS (SELECT TOP 1 1 FROM tblConstructionProject)
BEGIN
    DECLARE @creatorId UNIQUEIDENTIFIER
    SELECT TOP 1 @creatorId = Id FROM tblUser
    INSERT INTO tblConstructionProject (Name, Category, Stage, Location, Description, CreatorId, StartDate)
    VALUES ('name', 'category', 3, 'location', 'description', @creatorId, GETUTCDATE())
END