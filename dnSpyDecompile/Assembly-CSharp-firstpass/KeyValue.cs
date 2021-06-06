using System;

// Token: 0x02000011 RID: 17
public class KeyValue
{
	// Token: 0x1700000F RID: 15
	// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003E09 File Offset: 0x00002009
	// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003E11 File Offset: 0x00002011
	public Key Key { get; set; }

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003E1A File Offset: 0x0000201A
	// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003E22 File Offset: 0x00002022
	public byte[] Value { get; set; }

	// Token: 0x060000A5 RID: 165 RVA: 0x00003E2B File Offset: 0x0000202B
	public KeyValue(Key key, byte[] value)
	{
		this.Key = key;
		this.Value = value;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00003E41 File Offset: 0x00002041
	public override string ToString()
	{
		return string.Format("[KeyValue: {0}, {1}, {2} bytes]", this.Key.Field, this.Key.WireType, this.Value.Length);
	}
}
