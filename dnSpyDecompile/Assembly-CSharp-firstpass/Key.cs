using System;

// Token: 0x02000010 RID: 16
public class Key
{
	// Token: 0x1700000D RID: 13
	// (get) Token: 0x0600009B RID: 155 RVA: 0x00003DAF File Offset: 0x00001FAF
	// (set) Token: 0x0600009C RID: 156 RVA: 0x00003DB7 File Offset: 0x00001FB7
	public uint Field { get; set; }

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600009D RID: 157 RVA: 0x00003DC0 File Offset: 0x00001FC0
	// (set) Token: 0x0600009E RID: 158 RVA: 0x00003DC8 File Offset: 0x00001FC8
	public Wire WireType { get; set; }

	// Token: 0x0600009F RID: 159 RVA: 0x00003DD1 File Offset: 0x00001FD1
	public Key(uint field, Wire wireType)
	{
		this.Field = field;
		this.WireType = wireType;
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00003DE7 File Offset: 0x00001FE7
	public override string ToString()
	{
		return string.Format("[Key: {0}, {1}]", this.Field, this.WireType);
	}
}
