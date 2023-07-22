using MongoDB.Driver;
using ProgramTan.WebApi.Configs;
using ProgramTan.WebApi.Models;

namespace ProgramTan.WebApi.Services;

public class GuildService : ServiceBase<GuildModel>
{
	public GuildService(MongoDbConfig config) : base(config) { }

	public async Task<GuildModel> GetByGuildIdAsync(ulong guildId) =>
		await Collection.Find(g => g.GuildId == guildId)
			.FirstOrDefaultAsync();

	public async Task<GuildModel> CreateGuildAsync(GuildModel guild)
	{
		await Collection.InsertOneAsync(guild);
		return guild;
	}

	public async Task<GuildModel> UpdateGuildAsync(GuildModel guild)
	{
		await Collection.ReplaceOneAsync(g => g.Id == guild.Id, guild);
		return guild;
	}

	public async Task DeleteGuildAsync(string id) =>
		await Collection.DeleteOneAsync(g => g.Id == id);
}