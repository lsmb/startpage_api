namespace StartpageAPI.Models;

public class StartpageDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string LinksCollectionName { get; set; } = null!;

    public string BgCollectionName { get; set; } = null!;

    public string ItemsCollectionName { get; set; } = null!;

}
