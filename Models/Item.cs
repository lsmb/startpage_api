using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StartpageAPI.Models;

public class Item
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? id { get; set; }

    public string name { get; set; } = null!;

    [BsonRepresentation(BsonType.Int32)]
    public int priority { get; set; }

    public List<Link>? links { get; set; }
}
