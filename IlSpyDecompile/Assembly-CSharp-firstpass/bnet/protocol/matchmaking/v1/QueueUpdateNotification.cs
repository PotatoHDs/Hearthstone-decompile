using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class QueueUpdateNotification : IProtoBuf
	{
		public bool HasRequestId;

		private RequestId _RequestId;

		public bool HasWaitTimes;

		private QueueWaitTimes _WaitTimes;

		public bool HasIsMatchmaking;

		private bool _IsMatchmaking;

		public RequestId RequestId
		{
			get
			{
				return _RequestId;
			}
			set
			{
				_RequestId = value;
				HasRequestId = value != null;
			}
		}

		public QueueWaitTimes WaitTimes
		{
			get
			{
				return _WaitTimes;
			}
			set
			{
				_WaitTimes = value;
				HasWaitTimes = value != null;
			}
		}

		public bool IsMatchmaking
		{
			get
			{
				return _IsMatchmaking;
			}
			set
			{
				_IsMatchmaking = value;
				HasIsMatchmaking = true;
			}
		}

		public bool IsInitialized => true;

		public void SetRequestId(RequestId val)
		{
			RequestId = val;
		}

		public void SetWaitTimes(QueueWaitTimes val)
		{
			WaitTimes = val;
		}

		public void SetIsMatchmaking(bool val)
		{
			IsMatchmaking = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRequestId)
			{
				num ^= RequestId.GetHashCode();
			}
			if (HasWaitTimes)
			{
				num ^= WaitTimes.GetHashCode();
			}
			if (HasIsMatchmaking)
			{
				num ^= IsMatchmaking.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			QueueUpdateNotification queueUpdateNotification = obj as QueueUpdateNotification;
			if (queueUpdateNotification == null)
			{
				return false;
			}
			if (HasRequestId != queueUpdateNotification.HasRequestId || (HasRequestId && !RequestId.Equals(queueUpdateNotification.RequestId)))
			{
				return false;
			}
			if (HasWaitTimes != queueUpdateNotification.HasWaitTimes || (HasWaitTimes && !WaitTimes.Equals(queueUpdateNotification.WaitTimes)))
			{
				return false;
			}
			if (HasIsMatchmaking != queueUpdateNotification.HasIsMatchmaking || (HasIsMatchmaking && !IsMatchmaking.Equals(queueUpdateNotification.IsMatchmaking)))
			{
				return false;
			}
			return true;
		}

		public static QueueUpdateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueUpdateNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static QueueUpdateNotification Deserialize(Stream stream, QueueUpdateNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static QueueUpdateNotification DeserializeLengthDelimited(Stream stream)
		{
			QueueUpdateNotification queueUpdateNotification = new QueueUpdateNotification();
			DeserializeLengthDelimited(stream, queueUpdateNotification);
			return queueUpdateNotification;
		}

		public static QueueUpdateNotification DeserializeLengthDelimited(Stream stream, QueueUpdateNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static QueueUpdateNotification Deserialize(Stream stream, QueueUpdateNotification instance, long limit)
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
					if (instance.RequestId == null)
					{
						instance.RequestId = RequestId.DeserializeLengthDelimited(stream);
					}
					else
					{
						RequestId.DeserializeLengthDelimited(stream, instance.RequestId);
					}
					continue;
				case 18:
					if (instance.WaitTimes == null)
					{
						instance.WaitTimes = QueueWaitTimes.DeserializeLengthDelimited(stream);
					}
					else
					{
						QueueWaitTimes.DeserializeLengthDelimited(stream, instance.WaitTimes);
					}
					continue;
				case 24:
					instance.IsMatchmaking = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, QueueUpdateNotification instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.HasWaitTimes)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.WaitTimes.GetSerializedSize());
				QueueWaitTimes.Serialize(stream, instance.WaitTimes);
			}
			if (instance.HasIsMatchmaking)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsMatchmaking);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRequestId)
			{
				num++;
				uint serializedSize = RequestId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasWaitTimes)
			{
				num++;
				uint serializedSize2 = WaitTimes.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasIsMatchmaking)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
