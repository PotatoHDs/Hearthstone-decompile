using System.IO;
using bnet.protocol.Types;

namespace bnet.protocol
{
	public class GetEventOptions : IProtoBuf
	{
		public bool HasFetchFrom;

		private ulong _FetchFrom;

		public bool HasFetchUntil;

		private ulong _FetchUntil;

		public bool HasMaxEvents;

		private uint _MaxEvents;

		public bool HasOrder;

		private EventOrder _Order;

		public ulong FetchFrom
		{
			get
			{
				return _FetchFrom;
			}
			set
			{
				_FetchFrom = value;
				HasFetchFrom = true;
			}
		}

		public ulong FetchUntil
		{
			get
			{
				return _FetchUntil;
			}
			set
			{
				_FetchUntil = value;
				HasFetchUntil = true;
			}
		}

		public uint MaxEvents
		{
			get
			{
				return _MaxEvents;
			}
			set
			{
				_MaxEvents = value;
				HasMaxEvents = true;
			}
		}

		public EventOrder Order
		{
			get
			{
				return _Order;
			}
			set
			{
				_Order = value;
				HasOrder = true;
			}
		}

		public bool IsInitialized => true;

		public void SetFetchFrom(ulong val)
		{
			FetchFrom = val;
		}

		public void SetFetchUntil(ulong val)
		{
			FetchUntil = val;
		}

		public void SetMaxEvents(uint val)
		{
			MaxEvents = val;
		}

		public void SetOrder(EventOrder val)
		{
			Order = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFetchFrom)
			{
				num ^= FetchFrom.GetHashCode();
			}
			if (HasFetchUntil)
			{
				num ^= FetchUntil.GetHashCode();
			}
			if (HasMaxEvents)
			{
				num ^= MaxEvents.GetHashCode();
			}
			if (HasOrder)
			{
				num ^= Order.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetEventOptions getEventOptions = obj as GetEventOptions;
			if (getEventOptions == null)
			{
				return false;
			}
			if (HasFetchFrom != getEventOptions.HasFetchFrom || (HasFetchFrom && !FetchFrom.Equals(getEventOptions.FetchFrom)))
			{
				return false;
			}
			if (HasFetchUntil != getEventOptions.HasFetchUntil || (HasFetchUntil && !FetchUntil.Equals(getEventOptions.FetchUntil)))
			{
				return false;
			}
			if (HasMaxEvents != getEventOptions.HasMaxEvents || (HasMaxEvents && !MaxEvents.Equals(getEventOptions.MaxEvents)))
			{
				return false;
			}
			if (HasOrder != getEventOptions.HasOrder || (HasOrder && !Order.Equals(getEventOptions.Order)))
			{
				return false;
			}
			return true;
		}

		public static GetEventOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetEventOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetEventOptions Deserialize(Stream stream, GetEventOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetEventOptions DeserializeLengthDelimited(Stream stream)
		{
			GetEventOptions getEventOptions = new GetEventOptions();
			DeserializeLengthDelimited(stream, getEventOptions);
			return getEventOptions;
		}

		public static GetEventOptions DeserializeLengthDelimited(Stream stream, GetEventOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetEventOptions Deserialize(Stream stream, GetEventOptions instance, long limit)
		{
			instance.Order = EventOrder.EVENT_DESCENDING;
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 8:
					instance.FetchFrom = ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.FetchUntil = ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.MaxEvents = ProtocolParser.ReadUInt32(stream);
					continue;
				case 32:
					instance.Order = (EventOrder)ProtocolParser.ReadUInt64(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetEventOptions instance)
		{
			if (instance.HasFetchFrom)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.FetchFrom);
			}
			if (instance.HasFetchUntil)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.FetchUntil);
			}
			if (instance.HasMaxEvents)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.MaxEvents);
			}
			if (instance.HasOrder)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Order);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasFetchFrom)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(FetchFrom);
			}
			if (HasFetchUntil)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(FetchUntil);
			}
			if (HasMaxEvents)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MaxEvents);
			}
			if (HasOrder)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Order);
			}
			return num;
		}
	}
}
