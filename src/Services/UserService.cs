using System.Text.RegularExpressions;
using MongoDB.Driver;
using ProgramTan.WebApi.Configs;
using ProgramTan.WebApi.Models;

namespace ProgramTan.WebApi.Services;

public partial class UserService : ServiceBase<UserModel>
{
	private readonly string pepper;

	public UserService(MongoDbConfig config, string pepper) : base(config) =>
		this.pepper = pepper;

	public async Task<UserModel> GetByEmailAsync(string email) =>
		await Collection.Find(u => u.Email == email)
			.FirstOrDefaultAsync();

	public async Task<UserModel> GetByUsernameAsync(string username) =>
		await Collection.Find(u => u.Username == username)
			.FirstOrDefaultAsync();

	public async Task<UserModel> CreateAsync(UserModel user)
	{
		if (!EmailRegex().IsMatch(user.Email))
			throw new RestrictedException($"Email is invalid");

		if (await GetByEmailAsync(user.Email) is not null)
			throw new DuplicateException(
				$"User with this email already exists"
			);

		if (await GetByUsernameAsync(user.Username) is not null)
			throw new DuplicateException(
				$"User with this name already exists"
			);

		user.Salt = BCrypt.Net.BCrypt.GenerateSalt();
		user.Password = BCrypt.Net.BCrypt.HashPassword(
			$"{user.Password}{user.Salt}{this.pepper}",
			10
		);

		await Collection.InsertOneAsync(user);
		return user;
	}

	public async Task<UserModel> UpdateAsync(UserModel user)
	{
		await Collection.ReplaceOneAsync(u => u.Id == user.Id, user);
		return user;
	}

	[GeneratedRegex("^[A-Za-z0-9.\\-_]+@[A-Za-z0-9.\\-_]+\\.[a-zA-Z]{2,}$")]
	private static partial Regex EmailRegex();

	public async Task DeleteAsync(string id) =>
		await Collection.DeleteOneAsync(u => u.Id == id);
}
