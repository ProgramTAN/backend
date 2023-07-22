using MongoDB.Driver;
using ProgramTan.WebApi.Configs;
using ProgramTan.WebApi.Models;

namespace ProgramTan.WebApi.Services;

public class GuildService : ServiceBase<GuildModel>
{
	public GuildService(MongoDbConfig config) : base(config) { }

	public async Task<GuildModel> CreateGuildAsync(ulong guildId)
	{
		var guild = new GuildModel
		{
			GuildId = guildId,
			Settings = new(),
			Classes = new()
		};

		await Collection.InsertOneAsync(guild);
		return guild;
	}
}
