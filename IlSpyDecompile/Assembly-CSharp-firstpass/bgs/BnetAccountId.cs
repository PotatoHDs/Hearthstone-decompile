using bgs.types;
using PegasusShared;

namespace bgs
{
	public class BnetAccountId : BnetEntityId
	{
		public new static BnetAccountId CreateFromEntityId(EntityId src)
		{
			BnetAccountId bnetAccountId = new BnetAccountId();
			bnetAccountId.CopyFrom(src);
			return bnetAccountId;
		}

		public static BnetAccountId CreateFromNet(BnetId src)
		{
			BnetAccountId bnetAccountId = new BnetAccountId();
			bnetAccountId.CopyFrom(src);
			return bnetAccountId;
		}

		public static BnetAccountId CreateFromBnetEntityId(BnetEntityId src)
		{
			BnetAccountId bnetAccountId = new BnetAccountId();
			bnetAccountId.CopyFrom(src);
			return bnetAccountId;
		}

		public new BnetAccountId Clone()
		{
			return (BnetAccountId)base.Clone();
		}
	}
}
