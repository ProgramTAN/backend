using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace ProgramTan.WebApi.Models;

[CollectionName("guilds")]
public class GuildModel : IEntity
{
	[BsonId]
	[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
	public string Id { get; init; }

	[Required]
	[BsonElement("guild")]
	public ulong GuildId { get; set; }

	[Required]
	[BsonElement("settings")]
	public List<GuildSettings> Settings { get; set; }

	[Required]
	[BsonElement("classes")]
	public List<ClassModel> Classes { get; set; }
}

public class GuildSettings
{
	public GuildSettingCategories Category { get; set; }
	public GuildSettingChannels Channel { get; set; }
	public GuildSettingRoles Role { get; set; }
}

public class GuildSettingCategories
{
	public ulong Teaching { get; set; }
}

public class GuildSettingChannels
{
	public ulong Classes { get; set; }
	public ulong ClassRequest { get; set; }
	public ulong PingRequest { get; set; }
}

public class GuildSettingRoles
{
	public ulong ClassRequest { get; set; }
	public ulong Moderator { get; set; }
	public ulong NewClass { get; set; }
	public ulong PingRequest { get; set; }
	public ulong Student { get; set; }
	public ulong Teacher { get; set; }
	public List<ulong> Requestables { get; set; }
}
