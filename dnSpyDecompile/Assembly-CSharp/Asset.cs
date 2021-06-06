using System;

// Token: 0x02000849 RID: 2121
public class Asset
{
	// Token: 0x06007317 RID: 29463 RVA: 0x002509B9 File Offset: 0x0024EBB9
	public Asset(string guid)
	{
		this.m_guid = guid;
	}

	// Token: 0x06007318 RID: 29464 RVA: 0x002509C8 File Offset: 0x0024EBC8
	public string GetGuid()
	{
		return this.m_guid;
	}

	// Token: 0x06007319 RID: 29465 RVA: 0x002509D0 File Offset: 0x0024EBD0
	public override string ToString()
	{
		return string.Format("{{guid={0}}}", this.m_guid);
	}

	// Token: 0x04005BAB RID: 23467
	private string m_guid;
}
