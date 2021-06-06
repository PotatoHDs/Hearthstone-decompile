using System;
using Blizzard.T5.Core;

// Token: 0x020009FF RID: 2559
public class VarKey
{
	// Token: 0x06008ADB RID: 35547 RVA: 0x002C724E File Offset: 0x002C544E
	public VarKey(string key)
	{
		this.m_key = key;
	}

	// Token: 0x06008ADC RID: 35548 RVA: 0x002C725D File Offset: 0x002C545D
	public VarKey(string key, string subKey)
	{
		this.m_key = key + "." + subKey;
	}

	// Token: 0x06008ADD RID: 35549 RVA: 0x002C7277 File Offset: 0x002C5477
	public VarKey Key(string subKey)
	{
		return new VarKey(this.m_key, subKey);
	}

	// Token: 0x170007C3 RID: 1987
	// (get) Token: 0x06008ADE RID: 35550 RVA: 0x002C7288 File Offset: 0x002C5488
	public bool HasValue
	{
		get
		{
			string text;
			return VarsInternal.Get().TryGetValue(this.m_key, out text) && text != null;
		}
	}

	// Token: 0x06008ADF RID: 35551 RVA: 0x002C72B0 File Offset: 0x002C54B0
	public string GetStr(string def)
	{
		string result;
		if (VarsInternal.Get().TryGetValue(this.m_key, out result))
		{
			return result;
		}
		return def;
	}

	// Token: 0x06008AE0 RID: 35552 RVA: 0x002C72D4 File Offset: 0x002C54D4
	public int GetInt(int def)
	{
		string str;
		if (VarsInternal.Get().TryGetValue(this.m_key, out str))
		{
			return GeneralUtils.ForceInt(str);
		}
		return def;
	}

	// Token: 0x06008AE1 RID: 35553 RVA: 0x002C7300 File Offset: 0x002C5500
	public uint GetUInt(uint def)
	{
		string str;
		if (VarsInternal.Get().TryGetValue(this.m_key, out str))
		{
			return GeneralUtils.ForceUInt(str);
		}
		return def;
	}

	// Token: 0x06008AE2 RID: 35554 RVA: 0x002C732C File Offset: 0x002C552C
	public long GetLong(long defaultValue)
	{
		string str;
		if (VarsInternal.Get().TryGetValue(this.m_key, out str))
		{
			return GeneralUtils.ForceLong(str);
		}
		return defaultValue;
	}

	// Token: 0x06008AE3 RID: 35555 RVA: 0x002C7358 File Offset: 0x002C5558
	public float GetFloat(float def)
	{
		string str;
		if (VarsInternal.Get().TryGetValue(this.m_key, out str))
		{
			return GeneralUtils.ForceFloat(str);
		}
		return def;
	}

	// Token: 0x06008AE4 RID: 35556 RVA: 0x002C7384 File Offset: 0x002C5584
	public double GetDouble(double defaultValue)
	{
		string str;
		if (VarsInternal.Get().TryGetValue(this.m_key, out str))
		{
			return GeneralUtils.ForceDouble(str, defaultValue);
		}
		return defaultValue;
	}

	// Token: 0x06008AE5 RID: 35557 RVA: 0x002C73B0 File Offset: 0x002C55B0
	public bool GetBool(bool def)
	{
		string strVal;
		if (VarsInternal.Get().TryGetValue(this.m_key, out strVal))
		{
			return GeneralUtils.ForceBool(strVal);
		}
		return def;
	}

	// Token: 0x06008AE6 RID: 35558 RVA: 0x002C73D9 File Offset: 0x002C55D9
	public void Set(string value, bool permanent)
	{
		VarsInternal.Get().Set(this.m_key, value, permanent);
	}

	// Token: 0x06008AE7 RID: 35559 RVA: 0x002C73ED File Offset: 0x002C55ED
	public void Clear()
	{
		VarsInternal.Get().Clear(this.m_key);
	}

	// Token: 0x0400738A RID: 29578
	private string m_key;
}
