using Blizzard.T5.Core;

public class VarKey
{
	private string m_key;

	public bool HasValue
	{
		get
		{
			if (VarsInternal.Get().TryGetValue(m_key, out var value))
			{
				return value != null;
			}
			return false;
		}
	}

	public VarKey(string key)
	{
		m_key = key;
	}

	public VarKey(string key, string subKey)
	{
		m_key = key + "." + subKey;
	}

	public VarKey Key(string subKey)
	{
		return new VarKey(m_key, subKey);
	}

	public string GetStr(string def)
	{
		if (VarsInternal.Get().TryGetValue(m_key, out var value))
		{
			return value;
		}
		return def;
	}

	public int GetInt(int def)
	{
		if (VarsInternal.Get().TryGetValue(m_key, out var value))
		{
			return GeneralUtils.ForceInt(value);
		}
		return def;
	}

	public uint GetUInt(uint def)
	{
		if (VarsInternal.Get().TryGetValue(m_key, out var value))
		{
			return GeneralUtils.ForceUInt(value);
		}
		return def;
	}

	public long GetLong(long defaultValue)
	{
		if (VarsInternal.Get().TryGetValue(m_key, out var value))
		{
			return GeneralUtils.ForceLong(value);
		}
		return defaultValue;
	}

	public float GetFloat(float def)
	{
		if (VarsInternal.Get().TryGetValue(m_key, out var value))
		{
			return GeneralUtils.ForceFloat(value);
		}
		return def;
	}

	public double GetDouble(double defaultValue)
	{
		if (VarsInternal.Get().TryGetValue(m_key, out var value))
		{
			return GeneralUtils.ForceDouble(value, defaultValue);
		}
		return defaultValue;
	}

	public bool GetBool(bool def)
	{
		if (VarsInternal.Get().TryGetValue(m_key, out var value))
		{
			return GeneralUtils.ForceBool(value);
		}
		return def;
	}

	public void Set(string value, bool permanent)
	{
		VarsInternal.Get().Set(m_key, value, permanent);
	}

	public void Clear()
	{
		VarsInternal.Get().Clear(m_key);
	}
}
