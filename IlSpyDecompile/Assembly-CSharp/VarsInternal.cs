using System.Collections.Generic;
using UnityEngine;

internal class VarsInternal
{
	private static VarsInternal s_instance;

	private Map<string, string> m_vars = new Map<string, string>();

	private ConfigFile m_configFile = new ConfigFile();

	public IEnumerable<string> AllKeys => m_vars.Keys;

	private VarsInternal(bool loadFile)
	{
		if (loadFile)
		{
			string clientConfigPath = Vars.GetClientConfigPath();
			if (!LoadConfig(clientConfigPath))
			{
				Debug.LogError(string.Format("Failed to load config file: ", clientConfigPath));
			}
		}
	}

	public static VarsInternal Get()
	{
		if (s_instance == null)
		{
			s_instance = new VarsInternal(loadFile: true);
		}
		return s_instance;
	}

	public static void RefreshVars(bool loadFile)
	{
		s_instance = new VarsInternal(loadFile);
	}

	public bool TryGetValue(string key, out string value)
	{
		return m_vars.TryGetValue(key, out value);
	}

	public string Value(string key)
	{
		return m_vars[key];
	}

	public void Set(string key, string value, bool permanent)
	{
		m_vars[key] = value;
		if (permanent)
		{
			m_configFile.Set(key, value);
		}
	}

	public void Clear(string key)
	{
		if (m_vars.ContainsKey(key))
		{
			m_vars.Remove(key);
		}
	}

	private bool LoadConfig(string path)
	{
		if (!m_configFile.LightLoad(path))
		{
			return false;
		}
		foreach (ConfigFile.Line line in m_configFile.GetLines())
		{
			m_vars[line.m_fullKey] = line.m_value;
		}
		return true;
	}

	public bool SaveConfig(string path)
	{
		return m_configFile.Save(path);
	}

	public string GenerateText()
	{
		return m_configFile.GenerateText();
	}
}
