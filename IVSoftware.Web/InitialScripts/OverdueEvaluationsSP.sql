SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sergio De La Hoz
-- Create date: 2021/02/10
-- Description:	Procedimiento encargado de marcar como reporbada las evaluaciones vencidas
-- =============================================
CREATE OR ALTER PROCEDURE OverdueEvaluations
AS
BEGIN
	SET NOCOUNT ON;

    UPDATE pe
	SET IsApproved = 0,
		Score = 0
	FROM PersonEvaluations pe
	INNER JOIN Evaluation e ON (pe.EvaluationId = E.Id)
	WHERE pe.ResultJson IS NULL AND
		  pe.IsApproved IS NULL AND
		  e.EndDate <= GETDATE()
END
GO