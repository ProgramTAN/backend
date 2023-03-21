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
	public string Username { get; init; }

	[Required]
	[BsonElement("password")]
	public string Password { get; init; }
}
