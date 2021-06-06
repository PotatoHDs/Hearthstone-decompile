using UnityEngine;

public class BnetBattleTag
{
	private string m_name;

	private int m_number;

	public static BnetBattleTag CreateFromString(string src)
	{
		BnetBattleTag bnetBattleTag = new BnetBattleTag();
		if (!bnetBattleTag.SetString(src))
		{
			return null;
		}
		return bnetBattleTag;
	}

	public BnetBattleTag Clone()
	{
		return (BnetBattleTag)MemberwiseClone();
	}

	public string GetName()
	{
		return m_name;
	}

	public void SetName(string name)
	{
		m_name = name;
	}

	public int GetNumber()
	{
		return m_number;
	}

	public void SetNumber(int number)
	{
		m_number = number;
	}

	public string GetString()
	{
		return $"{m_name}#{m_number}";
	}

	public bool SetString(string composite)
	{
		if (composite == null)
		{
			Error.AddDevFatal("BnetBattleTag.SetString() - Given null string.");
			return false;
		}
		string[] array = composite.Split('#');
		if (array.Length < 2)
		{
			Debug.LogWarningFormat("BnetBattleTag.SetString() - Failed to split BattleTag \"{0}\" into 2 parts - this will prevent this player from showing up in Friends list and other places.", composite);
			return false;
		}
		if (!int.TryParse(array[1], out m_number))
		{
			Error.AddDevFatal("BnetBattleTag.SetString() - Failed to parse \"{0}\" into a number. Original string: \"{1}\"", array[1], composite);
			return false;
		}
		m_name = array[0];
		return true;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		BnetBattleTag bnetBattleTag = obj as BnetBattleTag;
		if ((object)bnetBattleTag == null)
		{
			return false;
		}
		if (m_name == bnetBattleTag.m_name)
		{
			return m_number == bnetBattleTag.m_number;
		}
		return false;
	}

	public bool Equals(BnetBattleTag other)
	{
		if ((object)other == null)
		{
			return false;
		}
		if (m_name == other.m_name)
		{
			return m_number == other.m_number;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return (17 * 11 + m_name.GetHashCode()) * 11 + m_number.GetHashCode();
	}

	public static bool operator ==(BnetBattleTag a, BnetBattleTag b)
	{
		if ((object)a == b)
		{
			return true;
		}
		if ((object)a == null || (object)b == null)
		{
			return false;
		}
		if (a.m_name == b.m_name)
		{
			return a.m_number == b.m_number;
		}
		return false;
	}

	public static bool operator !=(BnetBattleTag a, BnetBattleTag b)
	{
		return !(a == b);
	}

	public override string ToString()
	{
		return $"{m_name}#{m_number}";
	}
}
