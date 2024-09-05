CREATE PROCEDURE [dbo].[stp_ConstructionProject_Update]
    @Id          INT,
    @Name        VARCHAR (200) = NULL,
    @Location    VARCHAR (500) = NULL,
    @Stage       SMALLINT = NULL,
    @Category    VARCHAR (100) = NULL,
    @StartDate   DATETIME = NULL,
    @Description VARCHAR (MAX) = NULL
AS
BEGIN
	UPDATE [dbo].[tblConstructionProject]
    SET 
        Name = ISNULL(@Name, Name),
        Location = ISNULL(@Location, Location),
        Stage = ISNULL(@Stage,Stage),
        Category = ISNULL(@Category,Category),
        StartDate = ISNULL(@StartDate,StartDate),
        Description = ISNULL(@Description, Description),
        __UPDATE_TS = GETUTCDATE() 
    OUTPUT INSERTED.*
    WHERE Id = @Id
END