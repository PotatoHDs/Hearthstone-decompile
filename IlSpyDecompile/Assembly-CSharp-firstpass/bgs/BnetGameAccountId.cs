using bgs.types;
using bnet.protocol;
using bnet.protocol.account.v1;
using PegasusShared;

namespace bgs
{
	public class BnetGameAccountId : BnetEntityId
	{
		public new static BnetGameAccountId CreateFromEntityId(bgs.types.EntityId src)
		{
			BnetGameAccountId bnetGameAccountId = new BnetGameAccountId();
			bnetGameAccountId.CopyFrom(src);
			return bnetGameAccountId;
		}

		public static BnetGameAccountId CreateFromNet(BnetId src)
		{
			BnetGameAccountId bnetGameAccountId = new BnetGameAccountId();
			bnetGameAccountId.CopyFrom(src);
			return bnetGameAccountId;
		}

		public new static BnetGameAccountId CreateFromProtocol(bnet.protocol.EntityId src)
		{
			BnetGameAccountId bnetGameAccountId = new BnetGameAccountId();
			bnetGameAccountId.SetLo(src.Low);
			bnetGameAccountId.SetHi(src.High);
			return bnetGameAccountId;
		}

		public new static BnetGameAccountId CreateFromGameAccountHandle(GameAccountHandle handle)
		{
			return CreateFromEntityId(BnetEntityId.CreateEntityId(BnetEntityId.CreateFromGameAccountHandle(handle)));
		}

		public new BnetGameAccountId Clone()
		{
			return (BnetGameAccountId)base.Clone();
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			BnetGameAccountId bnetGameAccountId = obj as BnetGameAccountId;
			if ((object)bnetGameAccountId == null)
			{
				return false;
			}
			return GetGameAccountHandle().Equals(bnetGameAccountId.GetGameAccountHandle());
		}

		public bool Equals(BnetGameAccountId other)
		{
			if ((object)other == null)
			{
				return false;
			}
			return GetGameAccountHandle().Equals(other.GetGameAccountHandle());
		}

		public override int GetHashCode()
		{
			return (17 * 11 + (m_hi & 0xFFFFFFFFFFuL).GetHashCode()) * 11 + m_lo.GetHashCode();
		}

		public static bool operator ==(BnetGameAccountId a, BnetGameAccountId b)
		{
			if ((object)a == b)
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			return a.GetGameAccountHandle().Equals(b.GetGameAccountHandle());
		}

		public static bool operator !=(BnetGameAccountId a, BnetGameAccountId b)
		{
			return !(a == b);
		}
	}
}
