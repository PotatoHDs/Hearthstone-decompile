using bnet.protocol;

namespace bgs.types
{
	public struct EntityId
	{
		public ulong hi;

		public ulong lo;

		public bnet.protocol.EntityId ToProtocol()
		{
			bnet.protocol.EntityId entityId = new bnet.protocol.EntityId();
			entityId.SetHigh(hi);
			entityId.SetLow(lo);
			return entityId;
		}

		public EntityId(EntityId copyFrom)
		{
			hi = copyFrom.hi;
			lo = copyFrom.lo;
		}

		public EntityId(bnet.protocol.EntityId protoEntityId)
		{
			hi = protoEntityId.High;
			lo = protoEntityId.Low;
		}

		public override bool Equals(object obj)
		{
			if (obj is EntityId)
			{
				return this == (EntityId)obj;
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return hi.GetHashCode() ^ lo.GetHashCode();
		}

		public static bool operator ==(EntityId a, EntityId b)
		{
			if (a.hi == b.hi)
			{
				return a.lo == b.lo;
			}
			return false;
		}

		public static bool operator !=(EntityId a, EntityId b)
		{
			return !(a == b);
		}
	}
}
