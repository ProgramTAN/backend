using MongoDB.Driver;
using MongoDbGenericRepository.Attributes;
using ProgramTan.WebApi.Models;

namespace ProgramTan.WebApi.Services;

public class ServiceBase<T> where T : IEntity
{
	protected IMongoCollection<T> Collection { get; }

	public ServiceBase(MongoDbConfig config)
	{
		var client = new MongoClient(config.ConnectionString);
		var database = client.GetDatabase(config.Name);

		string collectionName = ((CollectionNameAttribute)Attribute
			.GetCustomAttribute(
				typeof(T),
				typeof(CollectionNameAttribute
			)
		)).Name;

		this.Collection = database.GetCollection<T>(collectionName);
	}

	public async Task<IEnumerable<T>> GetAll() =>
		await this.Collection.Find(x => true).ToListAsync();

	public async Task<T> GetById(string id) =>
		await this.Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
}
