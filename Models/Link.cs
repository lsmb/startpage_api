using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StartpageAPI.Models;

public class Link
{
    public string name { get; set; } = null!;

    public string URL { get; set; } = null!;

    [BsonRepresentation(BsonType.Int32)]
    public int priority { get; set; }
}
