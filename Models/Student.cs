using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CoreDotNetToken.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("roll")]
        public int Roll { get; set; }

    }
}

