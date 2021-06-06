using System.ComponentModel;

public class CardBackData
{
	public enum CardBackSource
	{
		[Description("startup")]
		STARTUP = 0,
		[Description("season")]
		SEASON = 1,
		[Description("achieve")]
		ACHIEVE = 2,
		[Description("fixed_reward")]
		FIXED_REWARD = 3,
		[Description("tavern_brawl")]
		TAVERN_BRAWL = 5,
		[Description("reward_system")]
		REWARD_SYSTEM = 6
	}

	public int ID { get; private set; }

	public CardBackSource Source { get; private set; }

	public long SourceData { get; private set; }

	public string Name { get; private set; }

	public bool Enabled { get; private set; }

	public string PrefabName { get; private set; }

	public CardBackData(int id, CardBackSource source, long sourceData, string name, bool enabled, string prefabName)
	{
		ID = id;
		Source = source;
		SourceData = sourceData;
		Name = name;
		Enabled = enabled;
		PrefabName = prefabName;
	}

	public override string ToString()
	{
		return $"[CardBackData: ID={ID}, Source={Name}, SourceData={Source}, Name={SourceData}, Enabled={Enabled}, PrefabPath={PrefabName}]";
	}
}
