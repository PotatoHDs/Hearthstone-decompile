using System;
using bgs.types;
using bnet.protocol;
using bnet.protocol.account.v1;
using PegasusShared;

namespace bgs
{
	// Token: 0x0200023D RID: 573
	public class BnetGameAccountId : BnetEntityId
	{
		// Token: 0x060023FD RID: 9213 RVA: 0x0007EE04 File Offset: 0x0007D004
		public new static BnetGameAccountId CreateFromEntityId(bgs.types.EntityId src)
		{
			BnetGameAccountId bnetGameAccountId = new BnetGameAccountId();
			bnetGameAccountId.CopyFrom(src);
			return bnetGameAccountId;
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x0007EE12 File Offset: 0x0007D012
		public static BnetGameAccountId CreateFromNet(BnetId src)
		{
			BnetGameAccountId bnetGameAccountId = new BnetGameAccountId();
			bnetGameAccountId.CopyFrom(src);
			return bnetGameAccountId;
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x0007EE20 File Offset: 0x0007D020
		public new static BnetGameAccountId CreateFromProtocol(bnet.protocol.EntityId src)
		{
			BnetGameAccountId bnetGameAccountId = new BnetGameAccountId();
			bnetGameAccountId.SetLo(src.Low);
			bnetGameAccountId.SetHi(src.High);
			return bnetGameAccountId;
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x0007EE3F File Offset: 0x0007D03F
		public new static BnetGameAccountId CreateFromGameAccountHandle(GameAccountHandle handle)
		{
			return BnetGameAccountId.CreateFromEntityId(BnetEntityId.CreateEntityId(BnetEntityId.CreateFromGameAccountHandle(handle)));
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x0007EE51 File Offset: 0x0007D051
		public new BnetGameAccountId Clone()
		{
			return (BnetGameAccountId)base.Clone();
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x0007EE60 File Offset: 0x0007D060
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			BnetGameAccountId bnetGameAccountId = obj as BnetGameAccountId;
			return bnetGameAccountId != null && base.GetGameAccountHandle().Equals(bnetGameAccountId.GetGameAccountHandle());
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x0007EE8F File Offset: 0x0007D08F
		public bool Equals(BnetGameAccountId other)
		{
			return other != null && base.GetGameAccountHandle().Equals(other.GetGameAccountHandle());
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x0007EEA8 File Offset: 0x0007D0A8
		public override int GetHashCode()
		{
			return (17 * 11 + (this.m_hi & 1099511627775UL).GetHashCode()) * 11 + this.m_lo.GetHashCode();
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x0007EEE2 File Offset: 0x0007D0E2
		public static bool operator ==(BnetGameAccountId a, BnetGameAccountId b)
		{
			return a == b || (a != null && b != null && a.GetGameAccountHandle().Equals(b.GetGameAccountHandle()));
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x0007EF03 File Offset: 0x0007D103
		public static bool operator !=(BnetGameAccountId a, BnetGameAccountId b)
		{
			return !(a == b);
		}
	}
}
