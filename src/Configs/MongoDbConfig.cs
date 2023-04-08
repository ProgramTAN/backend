namespace ProgramTan.WebApi.Configs;

public class MongoDbConfig
{
	public string Name { get; init; }
	public string Host { get; init; }
	public int Port { get; init; }
	public string ConnectionString => $"mongodb://{Host}:{Port}";
}
