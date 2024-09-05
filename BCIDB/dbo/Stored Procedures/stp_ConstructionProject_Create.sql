CREATE PROCEDURE [dbo].[stp_ConstructionProject_Create]
    @Name        VARCHAR (200),
    @Location    VARCHAR (500),
    @Stage       SMALLINT,
    @Category    VARCHAR (100),
    @StartDate   DATETIME,
    @Description VARCHAR (MAX),
    @CreatorId   UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO [dbo].[tblConstructionProject] (
        Name,
        Location,
        Stage,
        Category,
        StartDate,
        Description,
        CreatorId)
    OUTPUT INSERTED.Id
    VALUES (@Name,
         @Location,
            @Stage,
            @Category,
            @StartDate,
            @Description,
            @CreatorId
    )
END