using System;
using System.Text;

namespace bgs
{
	// Token: 0x02000250 RID: 592
	[Serializable]
	public class FourCC
	{
		// Token: 0x060024A0 RID: 9376 RVA: 0x00002654 File Offset: 0x00000854
		public FourCC()
		{
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000818A8 File Offset: 0x0007FAA8
		public FourCC(uint value)
		{
			this.m_value = value;
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000818B7 File Offset: 0x0007FAB7
		public FourCC(string stringVal)
		{
			this.SetString(stringVal);
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000818C6 File Offset: 0x0007FAC6
		public FourCC Clone()
		{
			FourCC fourCC = new FourCC();
			fourCC.CopyFrom(this);
			return fourCC;
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000818D4 File Offset: 0x0007FAD4
		public uint GetValue()
		{
			return this.m_value;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000818DC File Offset: 0x0007FADC
		public void SetValue(uint val)
		{
			this.m_value = val;
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000818E8 File Offset: 0x0007FAE8
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

		// Token: 0x060024A7 RID: 9383 RVA: 0x00081930 File Offset: 0x0007FB30
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

		// Token: 0x060024A8 RID: 9384 RVA: 0x00081970 File Offset: 0x0007FB70
		public void CopyFrom(FourCC other)
		{
			this.m_value = other.m_value;
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x0008197E File Offset: 0x0007FB7E
		public static implicit operator FourCC(uint val)
		{
			return new FourCC(val);
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x00081986 File Offset: 0x0007FB86
		public static bool operator ==(uint val, FourCC fourCC)
		{
			return !(fourCC == null) && val == fourCC.m_value;
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x0008199C File Offset: 0x0007FB9C
		public static bool operator ==(FourCC fourCC, uint val)
		{
			return !(fourCC == null) && fourCC.m_value == val;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000819B2 File Offset: 0x0007FBB2
		public static bool operator !=(uint val, FourCC fourCC)
		{
			return !(val == fourCC);
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000819BE File Offset: 0x0007FBBE
		public static bool operator !=(FourCC fourCC, uint val)
		{
			return !(fourCC == val);
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000819CC File Offset: 0x0007FBCC
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			FourCC fourCC = obj as FourCC;
			return fourCC != null && this.m_value == fourCC.m_value;
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000819F8 File Offset: 0x0007FBF8
		public bool Equals(FourCC other)
		{
			return other != null && this.m_value == other.m_value;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x00081A0D File Offset: 0x0007FC0D
		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x00081A1A File Offset: 0x0007FC1A
		public static bool operator ==(FourCC a, FourCC b)
		{
			return a == b || (a != null && b != null && a.m_value == b.m_value);
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x00081A38 File Offset: 0x0007FC38
		public static bool operator !=(FourCC a, FourCC b)
		{
			return !(a == b);
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x00081A44 File Offset: 0x0007FC44
		public override string ToString()
		{
			return this.GetString();
		}

		// Token: 0x04000F47 RID: 3911
		protected uint m_value;
	}
}
