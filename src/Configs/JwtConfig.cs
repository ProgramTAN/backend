namespace ProgramTan.WebApi.Configs;

public class JwtConfig
{
	public string Secret { get; init; }
	public int Expiration { get; init; }
}
