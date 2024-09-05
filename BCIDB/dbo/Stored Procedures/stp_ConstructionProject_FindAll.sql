CREATE PROCEDURE [dbo].[stp_ConstructionProject_FindAll]
	@ProjectStage SMALLINT = NULL,
    @PageNumber INT NULL = 1,
	@PageSize INT NULL = 100,
    @OrderBy VARCHAR(128) NULL = N'-createTs'
AS
BEGIN
	SELECT 
		*
    FROM view_ConstructionProject VCP
    WHERE VCP.Stage = ISNULL(@ProjectStage, VCP.Stage)
    ORDER BY
		CASE WHEN ISNULL(@OrderBy, '') LIKE '%-createTs%' THEN VCP.CreatedTs END DESC,
		CASE WHEN ISNULL(@OrderBy, '') LIKE '%+createTs%' THEN VCP.CreatedTs END ASC
	OFFSET (@PageNumber-1)*@PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY

    SELECT
		COUNT(1)
	FROM view_ConstructionProject VCP
    WHERE VCP.Stage = ISNULL(@ProjectStage, VCP.Stage)
END