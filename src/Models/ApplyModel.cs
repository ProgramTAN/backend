using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace ProgramTan.WebApi.Models;

[CollectionName("applies")]
public class ApplyModel : IEntity
{
	[BsonId]
	[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
	public string Id { get; init; }

	[Required]
	[BsonElement("email")]
	public string Email { get; set; }

	[BsonElement("phone")]
	public string Phone { get; set; }

	[Required]
	[BsonElement("github")]
	public string Github { get; set; }
}
