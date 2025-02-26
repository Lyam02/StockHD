// a rajouter dans le IActionResult Index(), si il y est déjà
// Bien sur si il y est déjà, faut pas le mettre une deuxième fois
public IActionResult Index()
{
    var query = @"
    SELECT 
        at.Name AS AssetTypeName, 
        COUNT(a.Id) AS AssetCount,
        SUM(CASE WHEN l.Name = 'Stock' THEN 1 ELSE 0 END) AS StockAssetCount
        FROM 
        AssetType at
        LEFT JOIN 
            Assets a ON a.AssetTypeId = at.Id
        LEFT JOIN 
            Locations l ON a.LocationId = l.Id
        GROUP BY 
            at.Name
        HAVING 
            (StockAssetCount = 0)
            OR COUNT(a.Id) = 0
            OR StockAssetCount = 1
            OR StockAssetCount = 2
            OR StockAssetCount = 3
            OR StockAssetCount = 4
            OR StockAssetCount = 5
            OR StockAssetCount = 6
            OR StockAssetCount = 7
            OR StockAssetCount = 8
            OR StockAssetCount = 9
            OR StockAssetCount = 10
            OR StockAssetCount > 1;";

    var alertAssets = _context.AlertAssets
                          .FromSqlRaw(query)
                          .ToList();

    return View(alertAssets);
}