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
}
