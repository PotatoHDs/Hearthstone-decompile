using System;
using System.Text;

// Token: 0x02000776 RID: 1910
[Serializable]
public class FourCC
{
	// Token: 0x06006BFA RID: 27642 RVA: 0x000052CE File Offset: 0x000034CE
	public FourCC()
	{
	}

	// Token: 0x06006BFB RID: 27643 RVA: 0x0022FB81 File Offset: 0x0022DD81
	public FourCC(uint value)
	{
		this.m_value = value;
	}

	// Token: 0x06006BFC RID: 27644 RVA: 0x0022FB90 File Offset: 0x0022DD90
	public FourCC(string stringVal)
	{
		this.SetString(stringVal);
	}

	// Token: 0x06006BFD RID: 27645 RVA: 0x0022FB9F File Offset: 0x0022DD9F
	public FourCC Clone()
	{
		FourCC fourCC = new FourCC();
		fourCC.CopyFrom(this);
		return fourCC;
	}

	// Token: 0x06006BFE RID: 27646 RVA: 0x0022FBAD File Offset: 0x0022DDAD
	public uint GetValue()
	{
		return this.m_value;
	}

	// Token: 0x06006BFF RID: 27647 RVA: 0x0022FBB5 File Offset: 0x0022DDB5
	public void SetValue(uint val)
	{
		this.m_value = val;
	}

	// Token: 0x06006C00 RID: 27648 RVA: 0x0022FBC0 File Offset: 0x0022DDC0
	public string GetString()
	{
		StringBuilder stringBuilder = new StringBuilder(4);
		for (int i = 24; i >= 0; i -= 8)
		{
			char c = (char)(this.m_value >> i & 255U);
			if (c != '\0')
			{
				stringBuilder.Append(c);
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06006C01 RID: 27649 RVA: 0x0022FC08 File Offset: 0x0022DE08
	public void SetString(string str)
	{
		this.m_value = 0U;
		int num = 0;
		while (num < str.Length && num < 4)
		{
			this.m_value = (this.m_value << 8 | (uint)((byte)str[num]));
			num++;
		}
	}

	// Token: 0x06006C02 RID: 27650 RVA: 0x0022FC48 File Offset: 0x0022DE48
	public void CopyFrom(FourCC other)
	{
		this.m_value = other.m_value;
	}

	// Token: 0x06006C03 RID: 27651 RVA: 0x0022FC56 File Offset: 0x0022DE56
	public static implicit operator FourCC(uint val)
	{
		return new FourCC(val);
	}

	// Token: 0x06006C04 RID: 27652 RVA: 0x0022FC5E File Offset: 0x0022DE5E
	public static bool operator ==(uint val, FourCC fourCC)
	{
		return !(fourCC == null) && val == fourCC.m_value;
	}

	// Token: 0x06006C05 RID: 27653 RVA: 0x0022FC74 File Offset: 0x0022DE74
	public static bool operator ==(FourCC fourCC, uint val)
	{
		return !(fourCC == null) && fourCC.m_value == val;
	}

	// Token: 0x06006C06 RID: 27654 RVA: 0x0022FC8A File Offset: 0x0022DE8A
	public static bool operator !=(uint val, FourCC fourCC)
	{
		return !(val == fourCC);
	}

	// Token: 0x06006C07 RID: 27655 RVA: 0x0022FC96 File Offset: 0x0022DE96
	public static bool operator !=(FourCC fourCC, uint val)
	{
		return !(fourCC == val);
	}

	// Token: 0x06006C08 RID: 27656 RVA: 0x0022FCA4 File Offset: 0x0022DEA4
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		FourCC fourCC = obj as FourCC;
		return fourCC != null && this.m_value == fourCC.m_value;
	}

	// Token: 0x06006C09 RID: 27657 RVA: 0x0022FCD0 File Offset: 0x0022DED0
	public bool Equals(FourCC other)
	{
		return other != null && this.m_value == other.m_value;
	}

	// Token: 0x06006C0A RID: 27658 RVA: 0x0022FCE5 File Offset: 0x0022DEE5
	public override int GetHashCode()
	{
		return this.m_value.GetHashCode();
	}

	// Token: 0x06006C0B RID: 27659 RVA: 0x0022FCF2 File Offset: 0x0022DEF2
	public static bool operator ==(FourCC a, FourCC b)
	{
		return a == b || (a != null && b != null && a.m_value == b.m_value);
	}

	// Token: 0x06006C0C RID: 27660 RVA: 0x0022FD10 File Offset: 0x0022DF10
	public static bool operator !=(FourCC a, FourCC b)
	{
		return !(a == b);
	}

	// Token: 0x06006C0D RID: 27661 RVA: 0x0022FD1C File Offset: 0x0022DF1C
	public override string ToString()
	{
		return this.GetString();
	}

	// Token: 0x04005741 RID: 22337
	protected uint m_value;
}
