SELECT 
    Text AS Errors
INTO ReportError.csv
FROM 2020-07-01_info.log
WHERE Text LIKE '%ERROR%'