using bgs.types;
using bnet.protocol;
using bnet.protocol.account.v1;
using PegasusShared;

namespace bgs
{
	public class BnetEntityId
	{
		protected ulong m_hi;

		protected ulong m_lo;

		public static BnetEntityId CreateFromEntityId(bgs.types.EntityId src)
		{
			BnetEntityId bnetEntityId = new BnetEntityId();
			bnetEntityId.CopyFrom(src);
			return bnetEntityId;
		}

		public static BnetEntityId CreateFromProtocol(bnet.protocol.EntityId src)
		{
			BnetEntityId bnetEntityId = new BnetEntityId();
			bnetEntityId.SetLo(src.Low);
			bnetEntityId.SetHi(src.High);
			return bnetEntityId;
		}

		public static BnetEntityId CreateFromGameAccountHandle(GameAccountHandle src)
		{
			BnetEntityId bnetEntityId = new BnetEntityId();
			bnetEntityId.SetLo(src.Id);
			bnetEntityId.SetHi(((ulong)src.Region << 32) | src.Program);
			return bnetEntityId;
		}

		public static bgs.types.EntityId CreateEntityId(BnetEntityId src)
		{
			bgs.types.EntityId result = default(bgs.types.EntityId);
			result.hi = src.m_hi;
			result.lo = src.m_lo;
			return result;
		}

		public static bnet.protocol.EntityId CreateForProtocol(BnetEntityId src)
		{
			bnet.protocol.EntityId entityId = new bnet.protocol.EntityId();
			entityId.SetLow(src.GetLo());
			entityId.SetHigh(src.GetHi());
			return entityId;
		}

		public GameAccountHandle GetGameAccountHandle()
		{
			GameAccountHandle gameAccountHandle = new GameAccountHandle();
			gameAccountHandle.SetId((uint)m_lo);
			gameAccountHandle.SetRegion((uint)(int)(m_hi >> 32) & 0xFFu);
			gameAccountHandle.SetProgram((uint)(m_hi & 0xFFFFFFFFu));
			return gameAccountHandle;
		}

		public BnetEntityId Clone()
		{
			return (BnetEntityId)MemberwiseClone();
		}

		public ulong GetHi()
		{
			return m_hi;
		}

		public void SetHi(ulong hi)
		{
			m_hi = hi;
		}

		public ulong GetLo()
		{
			return m_lo;
		}

		public void SetLo(ulong lo)
		{
			m_lo = lo;
		}

		public bool IsEmpty()
		{
			return (m_hi | m_lo) == 0;
		}

		public bool IsValid()
		{
			return m_lo != 0;
		}

		public void CopyFrom(bgs.types.EntityId id)
		{
			m_hi = id.hi;
			m_lo = id.lo;
		}

		public void CopyFrom(BnetEntityId id)
		{
			m_hi = id.m_hi;
			m_lo = id.m_lo;
		}

		public void CopyFrom(BnetId id)
		{
			m_hi = id.Hi;
			m_lo = id.Lo;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			BnetEntityId bnetEntityId = obj as BnetEntityId;
			if ((object)bnetEntityId == null)
			{
				return false;
			}
			if (m_hi == bnetEntityId.m_hi)
			{
				return m_lo == bnetEntityId.m_lo;
			}
			return false;
		}

		public bool Equals(BnetEntityId other)
		{
			if ((object)other == null)
			{
				return false;
			}
			if (m_hi == other.m_hi)
			{
				return m_lo == other.m_lo;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (17 * 11 + m_hi.GetHashCode()) * 11 + m_lo.GetHashCode();
		}

		public static bool operator ==(BnetEntityId a, BnetEntityId b)
		{
			if ((object)a == b)
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			if (a.m_hi == b.m_hi)
			{
				return a.m_lo == b.m_lo;
			}
			return false;
		}

		public static bool operator !=(BnetEntityId a, BnetEntityId b)
		{
			return !(a == b);
		}

		public override string ToString()
		{
			return $"[hi={m_hi} lo={m_lo}]";
		}
	}
}
