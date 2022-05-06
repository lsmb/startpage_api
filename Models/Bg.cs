using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StartpageAPI.Models;

public class Bg
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string URL { get; set; } = null!;
}
