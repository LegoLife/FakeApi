namespace API.Services;

public static class ServerNameGenerator
{
	private static readonly string[] Adjectives = { "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Silver", "Golden" };
	private static readonly string[] Nouns = { "Server", "Machine", "Node", "Host", "Cluster", "System", "Gateway", "Cloud" };
	private static readonly string[] Types = {"DC","DB","APP" }; 
	private static Random random = new Random();

	public static string GenerateServerName()
	{
		string adjective = GetRandomElement(Adjectives);
		string noun = GetRandomElement(Nouns);
		string type = GetRandomElement(Types);
		
		int number = random.Next(1000, 9999);
		string serverName = $"{adjective}-{type}-{noun}-{number}";

		return serverName;
	}

	private static string GetRandomElement(string[] array)
	{
		int index = random.Next(0, array.Length);
		return array[index];
	}
}