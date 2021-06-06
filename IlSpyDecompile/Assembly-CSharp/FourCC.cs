using System;
using System.Text;

[Serializable]
public class FourCC
{
	protected uint m_value;

	public FourCC()
	{
	}

	public FourCC(uint value)
	{
		m_value = value;
	}

	public FourCC(string stringVal)
	{
		SetString(stringVal);
	}

	public FourCC Clone()
	{
		FourCC fourCC = new FourCC();
		fourCC.CopyFrom(this);
		return fourCC;
	}

	public uint GetValue()
	{
		return m_value;
	}

	public void SetValue(uint val)
	{
		m_value = val;
	}

	public string GetString()
	{
		StringBuilder stringBuilder = new StringBuilder(4);
		for (int num = 24; num >= 0; num -= 8)
		{
			char c = (char)((m_value >> num) & 0xFFu);
			if (c != 0)
			{
				stringBuilder.Append(c);
			}
		}
		return stringBuilder.ToString();
	}

	public void SetString(string str)
	{
		m_value = 0u;
		for (int i = 0; i < str.Length && i < 4; i++)
		{
			m_value = (m_value << 8) | (byte)str[i];
		}
	}

	public void CopyFrom(FourCC other)
	{
		m_value = other.m_value;
	}

	public static implicit operator FourCC(uint val)
	{
		return new FourCC(val);
	}

	public static bool operator ==(uint val, FourCC fourCC)
	{
		if (fourCC == null)
		{
			return false;
		}
		return val == fourCC.m_value;
	}

	public static bool operator ==(FourCC fourCC, uint val)
	{
		if (fourCC == null)
		{
			return false;
		}
		return fourCC.m_value == val;
	}

	public static bool operator !=(uint val, FourCC fourCC)
	{
		return !(val == fourCC);
	}

	public static bool operator !=(FourCC fourCC, uint val)
	{
		return !(fourCC == val);
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		FourCC fourCC = obj as FourCC;
		if ((object)fourCC == null)
		{
			return false;
		}
		return m_value == fourCC.m_value;
	}

	public bool Equals(FourCC other)
	{
		if ((object)other == null)
		{
			return false;
		}
		return m_value == other.m_value;
	}

	public override int GetHashCode()
	{
		return m_value.GetHashCode();
	}

	public static bool operator ==(FourCC a, FourCC b)
	{
		if ((object)a == b)
		{
			return true;
		}
		if ((object)a == null || (object)b == null)
		{
			return false;
		}
		return a.m_value == b.m_value;
	}

	public static bool operator !=(FourCC a, FourCC b)
	{
		return !(a == b);
	}

	public override string ToString()
	{
		return GetString();
	}
}
