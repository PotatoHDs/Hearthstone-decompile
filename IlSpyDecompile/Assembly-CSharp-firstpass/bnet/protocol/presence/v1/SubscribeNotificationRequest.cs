using System;
using System.IO;

namespace bnet.protocol.presence.v1
{
	public class SubscribeNotificationRequest : IProtoBuf
	{
		public EntityId EntityId { get; set; }

		public bool IsInitialized => true;

		public void SetEntityId(EntityId val)
		{
			EntityId = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ EntityId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SubscribeNotificationRequest subscribeNotificationRequest = obj as SubscribeNotificationRequest;
			if (subscribeNotificationRequest == null)
			{
				return false;
			}
			if (!EntityId.Equals(subscribeNotificationRequest.EntityId))
			{
				return false;
			}
			return true;
		}

		public static SubscribeNotificationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeNotificationRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SubscribeNotificationRequest Deserialize(Stream stream, SubscribeNotificationRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SubscribeNotificationRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeNotificationRequest subscribeNotificationRequest = new SubscribeNotificationRequest();
			DeserializeLengthDelimited(stream, subscribeNotificationRequest);
			return subscribeNotificationRequest;
		}

		public static SubscribeNotificationRequest DeserializeLengthDelimited(Stream stream, SubscribeNotificationRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SubscribeNotificationRequest Deserialize(Stream stream, SubscribeNotificationRequest instance, long limit)
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
					if (instance.EntityId == null)
					{
						instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
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

		public static void Serialize(Stream stream, SubscribeNotificationRequest instance)
		{
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = EntityId.GetSerializedSize();
			return 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1;
		}
	}
}
