CREATE PROCEDURE [dbo].[stp_ConstructionProject_View]
	@ProjectId INT
AS
BEGIN
	SELECT 
		*
    FROM view_ConstructionProject VCP
    WHERE VCP.Id = @ProjectId
END