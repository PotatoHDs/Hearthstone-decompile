using System.IO;

namespace bnet.protocol.notification.v1
{
	public class PublishRequest : IProtoBuf
	{
		public bool HasTarget;

		private Target _Target;

		public bool HasNotification;

		private Notification _Notification;

		public Target Target
		{
			get
			{
				return _Target;
			}
			set
			{
				_Target = value;
				HasTarget = value != null;
			}
		}

		public Notification Notification
		{
			get
			{
				return _Notification;
			}
			set
			{
				_Notification = value;
				HasNotification = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetTarget(Target val)
		{
			Target = val;
		}

		public void SetNotification(Notification val)
		{
			Notification = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTarget)
			{
				num ^= Target.GetHashCode();
			}
			if (HasNotification)
			{
				num ^= Notification.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PublishRequest publishRequest = obj as PublishRequest;
			if (publishRequest == null)
			{
				return false;
			}
			if (HasTarget != publishRequest.HasTarget || (HasTarget && !Target.Equals(publishRequest.Target)))
			{
				return false;
			}
			if (HasNotification != publishRequest.HasNotification || (HasNotification && !Notification.Equals(publishRequest.Notification)))
			{
				return false;
			}
			return true;
		}

		public static PublishRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PublishRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PublishRequest Deserialize(Stream stream, PublishRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PublishRequest DeserializeLengthDelimited(Stream stream)
		{
			PublishRequest publishRequest = new PublishRequest();
			DeserializeLengthDelimited(stream, publishRequest);
			return publishRequest;
		}

		public static PublishRequest DeserializeLengthDelimited(Stream stream, PublishRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PublishRequest Deserialize(Stream stream, PublishRequest instance, long limit)
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
					if (instance.Target == null)
					{
						instance.Target = Target.DeserializeLengthDelimited(stream);
					}
					else
					{
						Target.DeserializeLengthDelimited(stream, instance.Target);
					}
					continue;
				case 18:
					if (instance.Notification == null)
					{
						instance.Notification = Notification.DeserializeLengthDelimited(stream);
					}
					else
					{
						Notification.DeserializeLengthDelimited(stream, instance.Notification);
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

		public static void Serialize(Stream stream, PublishRequest instance)
		{
			if (instance.HasTarget)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Target.GetSerializedSize());
				Target.Serialize(stream, instance.Target);
			}
			if (instance.HasNotification)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Notification.GetSerializedSize());
				Notification.Serialize(stream, instance.Notification);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasTarget)
			{
				num++;
				uint serializedSize = Target.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasNotification)
			{
				num++;
				uint serializedSize2 = Notification.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
