using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace ProgramTan.WebApi.Models;

[CollectionName("users")]
public class UserModel : IEntity
{
	[BsonId]
	[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
	public string Id { get; init; }

	[Required]
	[BsonElement("username")]
	public string Username { get; set; }

	[Required]
	[BsonElement("email")]
	public string Email { get; set; }

	[Required]
	[BsonElement("password")]
	public string Password { get; set; }

	[Required]
	[BsonElement("salt")]
	public string Salt { get; set; }
}
