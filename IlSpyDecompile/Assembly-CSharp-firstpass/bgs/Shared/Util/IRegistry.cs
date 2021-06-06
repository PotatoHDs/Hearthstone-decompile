namespace bgs.Shared.Util
{
	public interface IRegistry
	{
		BattleNetErrors RetrieveVector(string path, string name, out byte[] vec, bool encrypt = true);

		BattleNetErrors RetrieveString(string path, string name, out string s, bool encrypt = false);

		BattleNetErrors RetrieveInt(string path, string name, out int i);

		BattleNetErrors DeleteData(string path, string name);
	}
}
