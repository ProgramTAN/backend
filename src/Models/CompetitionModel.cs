using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace ProgramTan.WebApi.Models;

[CollectionName("competitions")]
public class CompetitionModel : IEntity
{
	[BsonId]
	[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
	public string Id { get; init; }

	[Required]
	[BsonElement("content")]
	public string Content { get; set; }

	[Required]
	[BsonElement("startDate")]
	public DateTime StartDate { get; set; }

	[Required]
	[BsonElement("endDate")]
	public DateTime EndDate { get; set; }
}
