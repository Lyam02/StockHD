CREATE VIEW Stat AS
SELECT 
    l.Name AS LocationName, 
    GROUP_CONCAT(DISTINCT at.Name ORDER BY at.Name) AS AssetTypes, 
    COUNT(a.Id) AS NbMatériel
FROM 
    Assets a
JOIN 
    AssetType at ON a.AssetTypeId = at.Id
JOIN 
    Locations l ON a.LocationId = l.Id
GROUP BY 
    l.Id, 
    l.Name;
