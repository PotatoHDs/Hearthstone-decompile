using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A01 RID: 2561
internal class VarsInternal
{
	// Token: 0x06008AF5 RID: 35573 RVA: 0x002C752C File Offset: 0x002C572C
	private VarsInternal(bool loadFile)
	{
		if (!loadFile)
		{
			return;
		}
		string clientConfigPath = Vars.GetClientConfigPath();
		if (!this.LoadConfig(clientConfigPath))
		{
			Debug.LogError(string.Format("Failed to load config file: ", clientConfigPath));
		}
	}

	// Token: 0x06008AF6 RID: 35574 RVA: 0x002C7578 File Offset: 0x002C5778
	public static VarsInternal Get()
	{
		if (VarsInternal.s_instance == null)
		{
			VarsInternal.s_instance = new VarsInternal(true);
		}
		return VarsInternal.s_instance;
	}

	// Token: 0x06008AF7 RID: 35575 RVA: 0x002C7591 File Offset: 0x002C5791
	public static void RefreshVars(bool loadFile)
	{
		VarsInternal.s_instance = new VarsInternal(loadFile);
	}

	// Token: 0x170007C5 RID: 1989
	// (get) Token: 0x06008AF8 RID: 35576 RVA: 0x002C759E File Offset: 0x002C579E
	public IEnumerable<string> AllKeys
	{
		get
		{
			return this.m_vars.Keys;
		}
	}

	// Token: 0x06008AF9 RID: 35577 RVA: 0x002C75AB File Offset: 0x002C57AB
	public bool TryGetValue(string key, out string value)
	{
		return this.m_vars.TryGetValue(key, out value);
	}

	// Token: 0x06008AFA RID: 35578 RVA: 0x002C75BA File Offset: 0x002C57BA
	public string Value(string key)
	{
		return this.m_vars[key];
	}

	// Token: 0x06008AFB RID: 35579 RVA: 0x002C75C8 File Offset: 0x002C57C8
	public void Set(string key, string value, bool permanent)
	{
		this.m_vars[key] = value;
		if (permanent)
		{
			this.m_configFile.Set(key, value);
		}
	}

	// Token: 0x06008AFC RID: 35580 RVA: 0x002C75E8 File Offset: 0x002C57E8
	public void Clear(string key)
	{
		if (this.m_vars.ContainsKey(key))
		{
			this.m_vars.Remove(key);
		}
	}

	// Token: 0x06008AFD RID: 35581 RVA: 0x002C7608 File Offset: 0x002C5808
	private bool LoadConfig(string path)
	{
		if (!this.m_configFile.LightLoad(path))
		{
			return false;
		}
		foreach (ConfigFile.Line line in this.m_configFile.GetLines())
		{
			this.m_vars[line.m_fullKey] = line.m_value;
		}
		return true;
	}

	// Token: 0x06008AFE RID: 35582 RVA: 0x002C7684 File Offset: 0x002C5884
	public bool SaveConfig(string path)
	{
		return this.m_configFile.Save(path);
	}

	// Token: 0x06008AFF RID: 35583 RVA: 0x002C7692 File Offset: 0x002C5892
	public string GenerateText()
	{
		return this.m_configFile.GenerateText();
	}

	// Token: 0x04007390 RID: 29584
	private static VarsInternal s_instance;

	// Token: 0x04007391 RID: 29585
	private Map<string, string> m_vars = new Map<string, string>();

	// Token: 0x04007392 RID: 29586
	private ConfigFile m_configFile = new ConfigFile();
}
