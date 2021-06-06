using System;
using System.Collections.Generic;

// Token: 0x0200030A RID: 778
public class GameEntityOptions
{
	// Token: 0x06002A67 RID: 10855 RVA: 0x000D56F0 File Offset: 0x000D38F0
	public GameEntityOptions()
	{
	}

	// Token: 0x06002A68 RID: 10856 RVA: 0x000D570E File Offset: 0x000D390E
	public GameEntityOptions(Map<GameEntityOption, bool> booleanOptions, Map<GameEntityOption, string> stringOptions)
	{
		this.AddBooleanOptions(booleanOptions);
		this.AddStringOptions(stringOptions);
	}

	// Token: 0x06002A69 RID: 10857 RVA: 0x000D573C File Offset: 0x000D393C
	public GameEntityOptions(GameEntityOptions source, Map<GameEntityOption, bool> booleanOptions, Map<GameEntityOption, string> stringOptions)
	{
		this.AddBooleanOptions(source.m_booleanOptions);
		this.AddStringOptions(source.m_stringOptions);
		this.AddBooleanOptions(booleanOptions);
		this.AddStringOptions(stringOptions);
	}

	// Token: 0x06002A6A RID: 10858 RVA: 0x000D578B File Offset: 0x000D398B
	public void AddOptions(Map<GameEntityOption, bool> booleanOptions, Map<GameEntityOption, string> stringOptions)
	{
		this.AddBooleanOptions(booleanOptions);
		this.AddStringOptions(stringOptions);
	}

	// Token: 0x06002A6B RID: 10859 RVA: 0x000D579C File Offset: 0x000D399C
	public void AddBooleanOptions(Map<GameEntityOption, bool> options)
	{
		foreach (KeyValuePair<GameEntityOption, bool> keyValuePair in options)
		{
			this.SetBooleanOption(keyValuePair.Key, keyValuePair.Value);
		}
	}

	// Token: 0x06002A6C RID: 10860 RVA: 0x000D57F8 File Offset: 0x000D39F8
	public void AddStringOptions(Map<GameEntityOption, string> options)
	{
		foreach (KeyValuePair<GameEntityOption, string> keyValuePair in options)
		{
			this.SetStringOption(keyValuePair.Key, keyValuePair.Value);
		}
	}

	// Token: 0x06002A6D RID: 10861 RVA: 0x000D5854 File Offset: 0x000D3A54
	public void SetBooleanOption(GameEntityOption option, bool value)
	{
		if (!this.m_booleanOptions.ContainsKey(option))
		{
			this.m_booleanOptions.Add(option, value);
			return;
		}
		this.m_booleanOptions[option] = value;
	}

	// Token: 0x06002A6E RID: 10862 RVA: 0x000D587F File Offset: 0x000D3A7F
	public void SetStringOption(GameEntityOption option, string value)
	{
		if (!this.m_stringOptions.ContainsKey(option))
		{
			this.m_stringOptions.Add(option, value);
			return;
		}
		this.m_stringOptions[option] = value;
	}

	// Token: 0x06002A6F RID: 10863 RVA: 0x000D58AA File Offset: 0x000D3AAA
	public bool GetBooleanOption(GameEntityOption option)
	{
		return this.m_booleanOptions != null && this.m_booleanOptions.ContainsKey(option) && this.m_booleanOptions[option];
	}

	// Token: 0x06002A70 RID: 10864 RVA: 0x000D58D0 File Offset: 0x000D3AD0
	public string GetStringOption(GameEntityOption option)
	{
		if (this.m_stringOptions != null && this.m_stringOptions.ContainsKey(option))
		{
			return this.m_stringOptions[option];
		}
		return null;
	}

	// Token: 0x040017E1 RID: 6113
	private Map<GameEntityOption, bool> m_booleanOptions = new Map<GameEntityOption, bool>();

	// Token: 0x040017E2 RID: 6114
	private Map<GameEntityOption, string> m_stringOptions = new Map<GameEntityOption, string>();
}
