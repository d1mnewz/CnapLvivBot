namespace CnapLvivBot.Core.Caching
{
	public interface ICacheKeyGenerator
	{
		string Generate<T>(string id);
	}
}