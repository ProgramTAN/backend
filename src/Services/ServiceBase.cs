using System.Collections;
using MongoDB.Driver;
using MongoDbGenericRepository.Attributes;
using ProgramTan.WebApi.Configs;
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

	public async Task<IEnumerable<T>> GetAllAsync() =>
		await this.Collection.Find(x => true).ToListAsync();

	public async Task<T> GetByIdAsync(string id)
	{
		T entity = await this.Collection.Find(x => x.Id == id)
			.FirstOrDefaultAsync() ?? throw new NotFoundException(
				$"{typeof(T).Name.Replace("model", "")} with id {id} not found"
			);
		ValidateFields(entity, "Id");

		return entity;
	}

	protected void ValidateFields(T entity) =>
		this.ValidateFields(entity, entity.GetType().GetProperties()
			.Select(p => p.Name).ToArray());

	protected void ValidateFields(T entity, params string[] fieldsToCheck)
	{
		foreach (var field in fieldsToCheck)
		{
			var property = entity.GetType().GetProperty(field);
			string errorMessage = $"{field} cannot be empty";

			var value = property.GetValue(entity)
				?? throw new RestrictedException(errorMessage);

			bool isEmpty = value is IList list && list.Count == 0 ||
				string.IsNullOrWhiteSpace(value.ToString());

			if (isEmpty)
				throw new RestrictedException(errorMessage);
		}
	}
}
