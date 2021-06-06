using System;

// Token: 0x020007B2 RID: 1970
[AttributeUsage(AttributeTargets.Property)]
public class DbfFieldAttribute : Attribute
{
	// Token: 0x06006D39 RID: 27961 RVA: 0x00233CE6 File Offset: 0x00231EE6
	public DbfFieldAttribute(string varName)
	{
		this.m_varName = varName;
	}

	// Token: 0x040057E6 RID: 22502
	public string m_varName;
}
