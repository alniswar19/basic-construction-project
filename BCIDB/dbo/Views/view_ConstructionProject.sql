CREATE VIEW [dbo].[view_ConstructionProject]
	AS
SELECT
    CP.[Id],
    CP.[Name],
    CP.[Description],
    CP.[Stage],
    CP.[Location],
    CP.[StartDate],
    CP.[Category],
    CP.[__CREATE_TS] as CreatedTs,
    CP.[__UPDATE_TS] as UpdatedTs,
    CP.[CreatorId],
    U.[FirstName],
    U.[LastName],
    U.[Email]
FROM dbo.tblConstructionProject CP
LEFT JOIN dbo.tblUser U
    ON CP.CreatorId = U.Id