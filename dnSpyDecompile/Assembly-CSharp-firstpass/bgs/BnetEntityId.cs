using System;
using bgs.types;
using bnet.protocol;
using bnet.protocol.account.v1;
using PegasusShared;

namespace bgs
{
	// Token: 0x0200023B RID: 571
	public class BnetEntityId
	{
		// Token: 0x060023DA RID: 9178 RVA: 0x0007EA79 File Offset: 0x0007CC79
		public static BnetEntityId CreateFromEntityId(bgs.types.EntityId src)
		{
			BnetEntityId bnetEntityId = new BnetEntityId();
			bnetEntityId.CopyFrom(src);
			return bnetEntityId;
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x0007EA87 File Offset: 0x0007CC87
		public static BnetEntityId CreateFromProtocol(bnet.protocol.EntityId src)
		{
			BnetEntityId bnetEntityId = new BnetEntityId();
			bnetEntityId.SetLo(src.Low);
			bnetEntityId.SetHi(src.High);
			return bnetEntityId;
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x0007EAA6 File Offset: 0x0007CCA6
		public static BnetEntityId CreateFromGameAccountHandle(GameAccountHandle src)
		{
			BnetEntityId bnetEntityId = new BnetEntityId();
			bnetEntityId.SetLo((ulong)src.Id);
			bnetEntityId.SetHi((ulong)src.Region << 32 | (ulong)src.Program);
			return bnetEntityId;
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x0007EAD4 File Offset: 0x0007CCD4
		public static bgs.types.EntityId CreateEntityId(BnetEntityId src)
		{
			return new bgs.types.EntityId
			{
				hi = src.m_hi,
				lo = src.m_lo
			};
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x0007EB04 File Offset: 0x0007CD04
		public static bnet.protocol.EntityId CreateForProtocol(BnetEntityId src)
		{
			bnet.protocol.EntityId entityId = new bnet.protocol.EntityId();
			entityId.SetLow(src.GetLo());
			entityId.SetHigh(src.GetHi());
			return entityId;
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x0007EB23 File Offset: 0x0007CD23
		public GameAccountHandle GetGameAccountHandle()
		{
			GameAccountHandle gameAccountHandle = new GameAccountHandle();
			gameAccountHandle.SetId((uint)this.m_lo);
			gameAccountHandle.SetRegion((uint)(this.m_hi >> 32) & 255U);
			gameAccountHandle.SetProgram((uint)(this.m_hi & (ulong)-1));
			return gameAccountHandle;
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x0007EB5D File Offset: 0x0007CD5D
		public BnetEntityId Clone()
		{
			return (BnetEntityId)base.MemberwiseClone();
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x0007EB6A File Offset: 0x0007CD6A
		public ulong GetHi()
		{
			return this.m_hi;
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x0007EB72 File Offset: 0x0007CD72
		public void SetHi(ulong hi)
		{
			this.m_hi = hi;
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x0007EB7B File Offset: 0x0007CD7B
		public ulong GetLo()
		{
			return this.m_lo;
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x0007EB83 File Offset: 0x0007CD83
		public void SetLo(ulong lo)
		{
			this.m_lo = lo;
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x0007EB8C File Offset: 0x0007CD8C
		public bool IsEmpty()
		{
			return (this.m_hi | this.m_lo) == 0UL;
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x0007EB9F File Offset: 0x0007CD9F
		public bool IsValid()
		{
			return this.m_lo > 0UL;
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x0007EBAB File Offset: 0x0007CDAB
		public void CopyFrom(bgs.types.EntityId id)
		{
			this.m_hi = id.hi;
			this.m_lo = id.lo;
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x0007EBC5 File Offset: 0x0007CDC5
		public void CopyFrom(BnetEntityId id)
		{
			this.m_hi = id.m_hi;
			this.m_lo = id.m_lo;
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x0007EBDF File Offset: 0x0007CDDF
		public void CopyFrom(BnetId id)
		{
			this.m_hi = id.Hi;
			this.m_lo = id.Lo;
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x0007EBFC File Offset: 0x0007CDFC
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			BnetEntityId bnetEntityId = obj as BnetEntityId;
			return bnetEntityId != null && this.m_hi == bnetEntityId.m_hi && this.m_lo == bnetEntityId.m_lo;
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x0007EC38 File Offset: 0x0007CE38
		public bool Equals(BnetEntityId other)
		{
			return other != null && this.m_hi == other.m_hi && this.m_lo == other.m_lo;
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x0007EC5D File Offset: 0x0007CE5D
		public override int GetHashCode()
		{
			return (17 * 11 + this.m_hi.GetHashCode()) * 11 + this.m_lo.GetHashCode();
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x0007EC7F File Offset: 0x0007CE7F
		public static bool operator ==(BnetEntityId a, BnetEntityId b)
		{
			return a == b || (a != null && b != null && a.m_hi == b.m_hi && a.m_lo == b.m_lo);
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x0007ECAD File Offset: 0x0007CEAD
		public static bool operator !=(BnetEntityId a, BnetEntityId b)
		{
			return !(a == b);
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x0007ECB9 File Offset: 0x0007CEB9
		public override string ToString()
		{
			return string.Format("[hi={0} lo={1}]", this.m_hi, this.m_lo);
		}

		// Token: 0x04000EA9 RID: 3753
		protected ulong m_hi;

		// Token: 0x04000EAA RID: 3754
		protected ulong m_lo;
	}
}
