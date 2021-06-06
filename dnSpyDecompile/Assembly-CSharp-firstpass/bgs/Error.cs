using System;

namespace bgs
{
	// Token: 0x020001FD RID: 509
	public struct Error
	{
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x0006D056 File Offset: 0x0006B256
		// (set) Token: 0x06001F29 RID: 7977 RVA: 0x0006D05E File Offset: 0x0006B25E
		public BattleNetErrors EnumVal { get; private set; }

		// Token: 0x06001F2A RID: 7978 RVA: 0x0006D067 File Offset: 0x0006B267
		public Error(BattleNetErrors code)
		{
			this = default(Error);
			this.EnumVal = code;
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x0006D077 File Offset: 0x0006B277
		public static implicit operator Error(BattleNetErrors code)
		{
			return new Error(code);
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0006D077 File Offset: 0x0006B277
		public static implicit operator Error(uint code)
		{
			return new Error((BattleNetErrors)code);
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x0006D07F File Offset: 0x0006B27F
		public uint Code
		{
			get
			{
				return (uint)this.EnumVal;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001F2E RID: 7982 RVA: 0x0006D088 File Offset: 0x0006B288
		public string Name
		{
			get
			{
				return this.EnumVal.ToString();
			}
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x0006D0A9 File Offset: 0x0006B2A9
		public static bool operator ==(Error a, BattleNetErrors b)
		{
			return a.EnumVal == b;
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x0006D0B5 File Offset: 0x0006B2B5
		public static bool operator !=(Error a, BattleNetErrors b)
		{
			return a.EnumVal != b;
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x0006D0A9 File Offset: 0x0006B2A9
		public static bool operator ==(Error a, uint b)
		{
			return a.EnumVal == (BattleNetErrors)b;
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x0006D0B5 File Offset: 0x0006B2B5
		public static bool operator !=(Error a, uint b)
		{
			return a.EnumVal != (BattleNetErrors)b;
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x0006D0C4 File Offset: 0x0006B2C4
		public override string ToString()
		{
			return string.Format("{0} {1}", this.Code, this.Name);
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x0006D0E4 File Offset: 0x0006B2E4
		public override bool Equals(object obj)
		{
			if (obj is BattleNetErrors)
			{
				return this.EnumVal == (BattleNetErrors)obj;
			}
			if (obj is Error)
			{
				return this.EnumVal == ((Error)obj).EnumVal;
			}
			if (obj is int)
			{
				return this.EnumVal == (BattleNetErrors)((int)obj);
			}
			if (obj is uint)
			{
				return this.EnumVal == (BattleNetErrors)((uint)obj);
			}
			if (obj is long)
			{
				return (ulong)this.EnumVal == (ulong)((long)obj);
			}
			if (obj is ulong)
			{
				return (ulong)this.EnumVal == (ulong)obj;
			}
			return base.Equals(obj);
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x0006D198 File Offset: 0x0006B398
		public override int GetHashCode()
		{
			return this.Code.GetHashCode();
		}
	}
}
