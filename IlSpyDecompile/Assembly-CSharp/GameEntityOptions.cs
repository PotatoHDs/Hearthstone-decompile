using System.Collections.Generic;

public class GameEntityOptions
{
	private Map<GameEntityOption, bool> m_booleanOptions = new Map<GameEntityOption, bool>();

	private Map<GameEntityOption, string> m_stringOptions = new Map<GameEntityOption, string>();

	public GameEntityOptions()
	{
	}

	public GameEntityOptions(Map<GameEntityOption, bool> booleanOptions, Map<GameEntityOption, string> stringOptions)
	{
		AddBooleanOptions(booleanOptions);
		AddStringOptions(stringOptions);
	}

	public GameEntityOptions(GameEntityOptions source, Map<GameEntityOption, bool> booleanOptions, Map<GameEntityOption, string> stringOptions)
	{
		AddBooleanOptions(source.m_booleanOptions);
		AddStringOptions(source.m_stringOptions);
		AddBooleanOptions(booleanOptions);
		AddStringOptions(stringOptions);
	}

	public void AddOptions(Map<GameEntityOption, bool> booleanOptions, Map<GameEntityOption, string> stringOptions)
	{
		AddBooleanOptions(booleanOptions);
		AddStringOptions(stringOptions);
	}

	public void AddBooleanOptions(Map<GameEntityOption, bool> options)
	{
		foreach (KeyValuePair<GameEntityOption, bool> option in options)
		{
			SetBooleanOption(option.Key, option.Value);
		}
	}

	public void AddStringOptions(Map<GameEntityOption, string> options)
	{
		foreach (KeyValuePair<GameEntityOption, string> option in options)
		{
			SetStringOption(option.Key, option.Value);
		}
	}

	public void SetBooleanOption(GameEntityOption option, bool value)
	{
		if (!m_booleanOptions.ContainsKey(option))
		{
			m_booleanOptions.Add(option, value);
		}
		else
		{
			m_booleanOptions[option] = value;
		}
	}

	public void SetStringOption(GameEntityOption option, string value)
	{
		if (!m_stringOptions.ContainsKey(option))
		{
			m_stringOptions.Add(option, value);
		}
		else
		{
			m_stringOptions[option] = value;
		}
	}

	public bool GetBooleanOption(GameEntityOption option)
	{
		if (m_booleanOptions != null && m_booleanOptions.ContainsKey(option))
		{
			return m_booleanOptions[option];
		}
		return false;
	}

	public string GetStringOption(GameEntityOption option)
	{
		if (m_stringOptions != null && m_stringOptions.ContainsKey(option))
		{
			return m_stringOptions[option];
		}
		return null;
	}
}
