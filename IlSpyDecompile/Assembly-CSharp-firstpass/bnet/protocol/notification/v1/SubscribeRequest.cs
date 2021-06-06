using System.IO;

namespace bnet.protocol.notification.v1
{
	public class SubscribeRequest : IProtoBuf
	{
		public bool HasSubscription;

		private Subscription _Subscription;

		public Subscription Subscription
		{
			get
			{
				return _Subscription;
			}
			set
			{
				_Subscription = value;
				HasSubscription = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetSubscription(Subscription val)
		{
			Subscription = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasSubscription)
			{
				num ^= Subscription.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscribeRequest subscribeRequest = obj as SubscribeRequest;
			if (subscribeRequest == null)
			{
				return false;
			}
			if (HasSubscription != subscribeRequest.HasSubscription || (HasSubscription && !Subscription.Equals(subscribeRequest.Subscription)))
			{
				return false;
			}
			return true;
		}

		public static SubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			DeserializeLengthDelimited(stream, subscribeRequest);
			return subscribeRequest;
		}

		public static SubscribeRequest DeserializeLengthDelimited(Stream stream, SubscribeRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscribeRequest Deserialize(Stream stream, SubscribeRequest instance, long limit)
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
				case 10:
					if (instance.Subscription == null)
					{
						instance.Subscription = Subscription.DeserializeLengthDelimited(stream);
					}
					else
					{
						Subscription.DeserializeLengthDelimited(stream, instance.Subscription);
					}
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

		public static void Serialize(Stream stream, SubscribeRequest instance)
		{
			if (instance.HasSubscription)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Subscription.GetSerializedSize());
				Subscription.Serialize(stream, instance.Subscription);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasSubscription)
			{
				num++;
				uint serializedSize = Subscription.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
