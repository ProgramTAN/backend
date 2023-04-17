using MongoDB.Driver;
using ProgramTan.WebApi.Configs;
using ProgramTan.WebApi.Models;

namespace ProgramTan.WebApi.Services;

public class CompetitionService : ServiceBase<CompetitionModel>
{
	public CompetitionService(MongoDbConfig config) : base(config) { }

	public async Task<CompetitionModel> Create(CompetitionModel competition)
	{
		await this.Collection.InsertOneAsync(competition);
		return competition;
	}

	public async Task<CompetitionModel> Update(CompetitionModel competition)
	{
		await this.Collection.ReplaceOneAsync(x => x.Id == competition.Id, competition);
		return competition;
	}

	public async Task Delete(string id) =>
		await this.Collection.DeleteOneAsync(x => x.Id == id);
}
