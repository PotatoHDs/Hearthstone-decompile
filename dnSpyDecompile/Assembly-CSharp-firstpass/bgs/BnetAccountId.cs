using System;
using bgs.types;
using PegasusShared;

namespace bgs
{
	// Token: 0x0200023A RID: 570
	public class BnetAccountId : BnetEntityId
	{
		// Token: 0x060023D5 RID: 9173 RVA: 0x0007EA3A File Offset: 0x0007CC3A
		public new static BnetAccountId CreateFromEntityId(EntityId src)
		{
			BnetAccountId bnetAccountId = new BnetAccountId();
			bnetAccountId.CopyFrom(src);
			return bnetAccountId;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x0007EA48 File Offset: 0x0007CC48
		public static BnetAccountId CreateFromNet(BnetId src)
		{
			BnetAccountId bnetAccountId = new BnetAccountId();
			bnetAccountId.CopyFrom(src);
			return bnetAccountId;
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x0007EA56 File Offset: 0x0007CC56
		public static BnetAccountId CreateFromBnetEntityId(BnetEntityId src)
		{
			BnetAccountId bnetAccountId = new BnetAccountId();
			bnetAccountId.CopyFrom(src);
			return bnetAccountId;
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x0007EA64 File Offset: 0x0007CC64
		public new BnetAccountId Clone()
		{
			return (BnetAccountId)base.Clone();
		}
	}
}
