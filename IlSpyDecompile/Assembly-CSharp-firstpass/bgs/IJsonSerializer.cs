namespace bgs
{
	public interface IJsonSerializer
	{
		T Deserialize<T>(string json);

		string Serialize(object obj);
	}
}
