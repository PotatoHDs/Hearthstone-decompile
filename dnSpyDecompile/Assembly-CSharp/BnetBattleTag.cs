using System;
using UnityEngine;

// Token: 0x02000764 RID: 1892
public class BnetBattleTag
{
	// Token: 0x06006A12 RID: 27154 RVA: 0x00228EBC File Offset: 0x002270BC
	public static BnetBattleTag CreateFromString(string src)
	{
		BnetBattleTag bnetBattleTag = new BnetBattleTag();
		if (!bnetBattleTag.SetString(src))
		{
			return null;
		}
		return bnetBattleTag;
	}

	// Token: 0x06006A13 RID: 27155 RVA: 0x00228EDB File Offset: 0x002270DB
	public BnetBattleTag Clone()
	{
		return (BnetBattleTag)base.MemberwiseClone();
	}

	// Token: 0x06006A14 RID: 27156 RVA: 0x00228EE8 File Offset: 0x002270E8
	public string GetName()
	{
		return this.m_name;
	}

	// Token: 0x06006A15 RID: 27157 RVA: 0x00228EF0 File Offset: 0x002270F0
	public void SetName(string name)
	{
		this.m_name = name;
	}

	// Token: 0x06006A16 RID: 27158 RVA: 0x00228EF9 File Offset: 0x002270F9
	public int GetNumber()
	{
		return this.m_number;
	}

	// Token: 0x06006A17 RID: 27159 RVA: 0x00228F01 File Offset: 0x00227101
	public void SetNumber(int number)
	{
		this.m_number = number;
	}

	// Token: 0x06006A18 RID: 27160 RVA: 0x00228F0A File Offset: 0x0022710A
	public string GetString()
	{
		return string.Format("{0}#{1}", this.m_name, this.m_number);
	}

	// Token: 0x06006A19 RID: 27161 RVA: 0x00228F28 File Offset: 0x00227128
	public bool SetString(string composite)
	{
		if (composite == null)
		{
			Error.AddDevFatal("BnetBattleTag.SetString() - Given null string.", Array.Empty<object>());
			return false;
		}
		string[] array = composite.Split(new char[]
		{
			'#'
		});
		if (array.Length < 2)
		{
			Debug.LogWarningFormat("BnetBattleTag.SetString() - Failed to split BattleTag \"{0}\" into 2 parts - this will prevent this player from showing up in Friends list and other places.", new object[]
			{
				composite
			});
			return false;
		}
		if (!int.TryParse(array[1], out this.m_number))
		{
			Error.AddDevFatal("BnetBattleTag.SetString() - Failed to parse \"{0}\" into a number. Original string: \"{1}\"", new object[]
			{
				array[1],
				composite
			});
			return false;
		}
		this.m_name = array[0];
		return true;
	}

	// Token: 0x06006A1A RID: 27162 RVA: 0x00228FB0 File Offset: 0x002271B0
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		BnetBattleTag bnetBattleTag = obj as BnetBattleTag;
		return bnetBattleTag != null && this.m_name == bnetBattleTag.m_name && this.m_number == bnetBattleTag.m_number;
	}

	// Token: 0x06006A1B RID: 27163 RVA: 0x00228FF1 File Offset: 0x002271F1
	public bool Equals(BnetBattleTag other)
	{
		return other != null && this.m_name == other.m_name && this.m_number == other.m_number;
	}

	// Token: 0x06006A1C RID: 27164 RVA: 0x0022901B File Offset: 0x0022721B
	public override int GetHashCode()
	{
		return (17 * 11 + this.m_name.GetHashCode()) * 11 + this.m_number.GetHashCode();
	}

	// Token: 0x06006A1D RID: 27165 RVA: 0x0022903D File Offset: 0x0022723D
	public static bool operator ==(BnetBattleTag a, BnetBattleTag b)
	{
		return a == b || (a != null && b != null && a.m_name == b.m_name && a.m_number == b.m_number);
	}

	// Token: 0x06006A1E RID: 27166 RVA: 0x00229070 File Offset: 0x00227270
	public static bool operator !=(BnetBattleTag a, BnetBattleTag b)
	{
		return !(a == b);
	}

	// Token: 0x06006A1F RID: 27167 RVA: 0x00228F0A File Offset: 0x0022710A
	public override string ToString()
	{
		return string.Format("{0}#{1}", this.m_name, this.m_number);
	}

	// Token: 0x040056D2 RID: 22226
	private string m_name;

	// Token: 0x040056D3 RID: 22227
	private int m_number;
}
