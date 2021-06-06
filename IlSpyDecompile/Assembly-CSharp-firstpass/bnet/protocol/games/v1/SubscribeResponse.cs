using System.IO;

namespace bnet.protocol.games.v1
{
	public class SubscribeResponse : IProtoBuf
	{
		public bool HasSubscriptionId;

		private ulong _SubscriptionId;

		public ulong SubscriptionId
		{
			get
			{
				return _SubscriptionId;
			}
			set
			{
				_SubscriptionId = value;
				HasSubscriptionId = true;
			}
		}

		public bool IsInitialized => true;

		public void SetSubscriptionId(ulong val)
		{
			SubscriptionId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSubscriptionId)
			{
				num ^= SubscriptionId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscribeResponse subscribeResponse = obj as SubscribeResponse;
			if (subscribeResponse == null)
			{
				return false;
			}
			if (HasSubscriptionId != subscribeResponse.HasSubscriptionId || (HasSubscriptionId && !SubscriptionId.Equals(subscribeResponse.SubscriptionId)))
			{
				return false;
			}
			return true;
		}

		public static SubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResponse subscribeResponse = new SubscribeResponse();
			DeserializeLengthDelimited(stream, subscribeResponse);
			return subscribeResponse;
		}

		public static SubscribeResponse DeserializeLengthDelimited(Stream stream, SubscribeResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance, long limit)
		{
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
					instance.SubscriptionId = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, SubscribeResponse instance)
		{
			if (instance.HasSubscriptionId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.SubscriptionId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSubscriptionId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(SubscriptionId);
			}
			return num;
		}
	}
}
