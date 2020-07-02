SELECT
TRIM(SUBSTR(text, 25, 5)) AS [Log_levels],
COUNT([Index]) AS [Total_Count]
INTO ReportCount.CSV
FROM 2020-07-02_info.log
WHERE (CASE TRIM(SUBSTR(text, 25, 5))
         WHEN 'ERROR' THEN 1
         WHEN 'DEBUG' THEN 1
         WHEN 'INFO' THEN 1
         ELSE 2
         END = 1)
 GROUP BY TRIM(SUBSTR(text, 25, 5))
 