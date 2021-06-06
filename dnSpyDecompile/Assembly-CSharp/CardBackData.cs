using System;
using System.ComponentModel;

// Token: 0x020000DB RID: 219
public class CardBackData
{
	// Token: 0x06000CB2 RID: 3250 RVA: 0x00049D25 File Offset: 0x00047F25
	public CardBackData(int id, CardBackData.CardBackSource source, long sourceData, string name, bool enabled, string prefabName)
	{
		this.ID = id;
		this.Source = source;
		this.SourceData = sourceData;
		this.Name = name;
		this.Enabled = enabled;
		this.PrefabName = prefabName;
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00049D5A File Offset: 0x00047F5A
	// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00049D62 File Offset: 0x00047F62
	public int ID { get; private set; }

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00049D6B File Offset: 0x00047F6B
	// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x00049D73 File Offset: 0x00047F73
	public CardBackData.CardBackSource Source { get; private set; }

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00049D7C File Offset: 0x00047F7C
	// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x00049D84 File Offset: 0x00047F84
	public long SourceData { get; private set; }

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00049D8D File Offset: 0x00047F8D
	// (set) Token: 0x06000CBA RID: 3258 RVA: 0x00049D95 File Offset: 0x00047F95
	public string Name { get; private set; }

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00049D9E File Offset: 0x00047F9E
	// (set) Token: 0x06000CBC RID: 3260 RVA: 0x00049DA6 File Offset: 0x00047FA6
	public bool Enabled { get; private set; }

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00049DAF File Offset: 0x00047FAF
	// (set) Token: 0x06000CBE RID: 3262 RVA: 0x00049DB7 File Offset: 0x00047FB7
	public string PrefabName { get; private set; }

	// Token: 0x06000CBF RID: 3263 RVA: 0x00049DC0 File Offset: 0x00047FC0
	public override string ToString()
	{
		return string.Format("[CardBackData: ID={0}, Source={1}, SourceData={2}, Name={3}, Enabled={4}, PrefabPath={5}]", new object[]
		{
			this.ID,
			this.Name,
			this.Source,
			this.SourceData,
			this.Enabled,
			this.PrefabName
		});
	}

	// Token: 0x020013E4 RID: 5092
	public enum CardBackSource
	{
		// Token: 0x0400A82F RID: 43055
		[Description("startup")]
		STARTUP,
		// Token: 0x0400A830 RID: 43056
		[Description("season")]
		SEASON,
		// Token: 0x0400A831 RID: 43057
		[Description("achieve")]
		ACHIEVE,
		// Token: 0x0400A832 RID: 43058
		[Description("fixed_reward")]
		FIXED_REWARD,
		// Token: 0x0400A833 RID: 43059
		[Description("tavern_brawl")]
		TAVERN_BRAWL = 5,
		// Token: 0x0400A834 RID: 43060
		[Description("reward_system")]
		REWARD_SYSTEM
	}
}
