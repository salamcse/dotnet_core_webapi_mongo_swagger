using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CoreDotNetToken.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        public string name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Roll is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Roll must be numeric")]
        public string roll { get; set; } = string.Empty;
    }
}

