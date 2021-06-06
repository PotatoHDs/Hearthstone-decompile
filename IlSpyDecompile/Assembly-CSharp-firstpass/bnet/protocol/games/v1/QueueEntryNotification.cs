using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	public class QueueEntryNotification : IProtoBuf
	{
		public bool HasRequestId;

		private FindGameRequestId _RequestId;

		public bool HasUpdateInfo;

		private QueueUpdate _UpdateInfo;

		public bool HasRequestInitiator;

		private GameAccountHandle _RequestInitiator;

		public FindGameRequestId RequestId
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

		public QueueUpdate UpdateInfo
		{
			get
			{
				return _UpdateInfo;
			}
			set
			{
				_UpdateInfo = value;
				HasUpdateInfo = value != null;
			}
		}

		public GameAccountHandle RequestInitiator
		{
			get
			{
				return _RequestInitiator;
			}
			set
			{
				_RequestInitiator = value;
				HasRequestInitiator = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetRequestId(FindGameRequestId val)
		{
			RequestId = val;
		}

		public void SetUpdateInfo(QueueUpdate val)
		{
			UpdateInfo = val;
		}

		public void SetRequestInitiator(GameAccountHandle val)
		{
			RequestInitiator = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRequestId)
			{
				num ^= RequestId.GetHashCode();
			}
			if (HasUpdateInfo)
			{
				num ^= UpdateInfo.GetHashCode();
			}
			if (HasRequestInitiator)
			{
				num ^= RequestInitiator.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			QueueEntryNotification queueEntryNotification = obj as QueueEntryNotification;
			if (queueEntryNotification == null)
			{
				return false;
			}
			if (HasRequestId != queueEntryNotification.HasRequestId || (HasRequestId && !RequestId.Equals(queueEntryNotification.RequestId)))
			{
				return false;
			}
			if (HasUpdateInfo != queueEntryNotification.HasUpdateInfo || (HasUpdateInfo && !UpdateInfo.Equals(queueEntryNotification.UpdateInfo)))
			{
				return false;
			}
			if (HasRequestInitiator != queueEntryNotification.HasRequestInitiator || (HasRequestInitiator && !RequestInitiator.Equals(queueEntryNotification.RequestInitiator)))
			{
				return false;
			}
			return true;
		}

		public static QueueEntryNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueEntryNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static QueueEntryNotification Deserialize(Stream stream, QueueEntryNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static QueueEntryNotification DeserializeLengthDelimited(Stream stream)
		{
			QueueEntryNotification queueEntryNotification = new QueueEntryNotification();
			DeserializeLengthDelimited(stream, queueEntryNotification);
			return queueEntryNotification;
		}

		public static QueueEntryNotification DeserializeLengthDelimited(Stream stream, QueueEntryNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static QueueEntryNotification Deserialize(Stream stream, QueueEntryNotification instance, long limit)
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
						instance.RequestId = FindGameRequestId.DeserializeLengthDelimited(stream);
					}
					else
					{
						FindGameRequestId.DeserializeLengthDelimited(stream, instance.RequestId);
					}
					continue;
				case 18:
					if (instance.UpdateInfo == null)
					{
						instance.UpdateInfo = QueueUpdate.DeserializeLengthDelimited(stream);
					}
					else
					{
						QueueUpdate.DeserializeLengthDelimited(stream, instance.UpdateInfo);
					}
					continue;
				case 26:
					if (instance.RequestInitiator == null)
					{
						instance.RequestInitiator = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.RequestInitiator);
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

		public static void Serialize(Stream stream, QueueEntryNotification instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				FindGameRequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.HasUpdateInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.UpdateInfo.GetSerializedSize());
				QueueUpdate.Serialize(stream, instance.UpdateInfo);
			}
			if (instance.HasRequestInitiator)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.RequestInitiator.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.RequestInitiator);
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
			if (HasUpdateInfo)
			{
				num++;
				uint serializedSize2 = UpdateInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasRequestInitiator)
			{
				num++;
				uint serializedSize3 = RequestInitiator.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
