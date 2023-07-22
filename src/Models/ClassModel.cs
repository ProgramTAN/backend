using MongoDB.Bson.Serialization.Attributes;

namespace ProgramTan.WebApi.Models;

public class ClassModel : IEntity
{
	[BsonId]
	[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
	public string Id { get; init; }

	[BsonElement("guild")]
	public ulong GuildId { get; set; }

	[BsonElement("teacher")]
	public ulong TeacherId { get; set; }

	[BsonElement("title")]
	public string Title { get; set; }

	[BsonElement("description")]
	public string Description { get; set; }

	[BsonElement("courseUrl")]
	public string CourseUrl { get; set; }

	[BsonElement("isApproved")]
	public bool IsApproved { get; set; }

	[BsonElement("textChannel")]
	public ulong TextChannelId { get; set; }

	[BsonElement("voiceChannel")]
	public ulong VoiceChannelId { get; set; }
}
