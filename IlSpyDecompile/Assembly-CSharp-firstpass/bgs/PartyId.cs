using bgs.types;
using bnet.protocol;
using bnet.protocol.channel.v2;

namespace bgs
{
	public class PartyId
	{
		public static readonly PartyId Empty = new PartyId(0uL, 0uL);

		public ulong Hi { get; private set; }

		public ulong Lo { get; private set; }

		public bool IsEmpty
		{
			get
			{
				if (Hi == 0L)
				{
					return Lo == 0;
				}
				return false;
			}
		}

		public PartyId()
		{
			Hi = (Lo = 0uL);
		}

		public PartyId(ulong highBits, ulong lowBits)
		{
			Set(highBits, lowBits);
		}

		public PartyId(bgs.types.EntityId partyEntityId)
		{
			Set(partyEntityId.hi, partyEntityId.lo);
		}

		public void Set(ulong highBits, ulong lowBits)
		{
			Hi = highBits;
			Lo = lowBits;
		}

		public static implicit operator PartyId(BnetEntityId entityId)
		{
			if (entityId == null)
			{
				return null;
			}
			return new PartyId(entityId.GetHi(), entityId.GetLo());
		}

		public static bool operator ==(PartyId a, PartyId b)
		{
			if ((object)a == null)
			{
				return (object)b == null;
			}
			if ((object)b == null)
			{
				return false;
			}
			if (a.Hi == b.Hi)
			{
				return a.Lo == b.Lo;
			}
			return false;
		}

		public static bool operator !=(PartyId a, PartyId b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			if (obj is PartyId)
			{
				return this == (PartyId)obj;
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return Hi.GetHashCode() ^ Lo.GetHashCode();
		}

		public override string ToString()
		{
			return $"{Hi}-{Lo}";
		}

		public static PartyId FromEntityId(bgs.types.EntityId entityId)
		{
			return new PartyId(entityId);
		}

		public static PartyId FromBnetEntityId(BnetEntityId entityId)
		{
			return new PartyId(entityId.GetHi(), entityId.GetLo());
		}

		public static PartyId FromProtocol(bnet.protocol.EntityId protoEntityId)
		{
			return new PartyId(protoEntityId.High, protoEntityId.Low);
		}

		public static PartyId FromChannelId(ChannelId channelId)
		{
			if (channelId == null)
			{
				return null;
			}
			return new PartyId(((ulong)channelId.Host.Epoch << 32) | channelId.Host.Label, channelId.Id);
		}

		public bgs.types.EntityId ToEntityId()
		{
			bgs.types.EntityId result = default(bgs.types.EntityId);
			result.hi = Hi;
			result.lo = Lo;
			return result;
		}

		public BnetEntityId ToBnetEntityId()
		{
			BnetEntityId bnetEntityId = new BnetEntityId();
			bnetEntityId.SetHi(Hi);
			bnetEntityId.SetLo(Lo);
			return bnetEntityId;
		}

		public ChannelId ToChannelId()
		{
			ChannelId channelId = new ChannelId();
			ProcessId processId = new ProcessId();
			channelId.Id = (uint)Lo;
			channelId.SetHost(processId);
			processId.SetLabel((uint)(int)Hi & 0xFFFFFFFFu);
			processId.SetEpoch((uint)(Hi >> 32));
			return channelId;
		}
	}
}
