using MongoDB.Driver;
using ProgramTan.WebApi.Models;

namespace ProgramTan.WebApi.Services;

public class UserService : ServiceBase<UserModel>
{
	public UserService(MongoDbConfig config) : base(config) { }

	public async Task<UserModel> CreateAsync(UserModel user)
	{
		await this.Collection.InsertOneAsync(user);
		return user;
	}

	public async Task<UserModel> UpdateAsync(UserModel user)
	{
		await this.Collection.ReplaceOneAsync(u => u.Id == user.Id, user);
		return user;
	}

	public async Task DeleteAsync(string id) =>
		await this.Collection.DeleteOneAsync(u => u.Id == id);
}
