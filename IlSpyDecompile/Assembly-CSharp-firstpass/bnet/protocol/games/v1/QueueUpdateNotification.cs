using System.IO;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	public class QueueUpdateNotification : IProtoBuf
	{
		public bool HasRequestId;

		private FindGameRequestId _RequestId;

		public bool HasUpdateInfo;

		private QueueUpdate _UpdateInfo;

		public bool HasMatchmaking;

		private bool _Matchmaking;

		public bool HasMatchmakerResult;

		private uint _MatchmakerResult;

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

		public bool Matchmaking
		{
			get
			{
				return _Matchmaking;
			}
			set
			{
				_Matchmaking = value;
				HasMatchmaking = true;
			}
		}

		public uint MatchmakerResult
		{
			get
			{
				return _MatchmakerResult;
			}
			set
			{
				_MatchmakerResult = value;
				HasMatchmakerResult = true;
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

		public void SetMatchmaking(bool val)
		{
			Matchmaking = val;
		}

		public void SetMatchmakerResult(uint val)
		{
			MatchmakerResult = val;
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
			if (HasMatchmaking)
			{
				num ^= Matchmaking.GetHashCode();
			}
			if (HasMatchmakerResult)
			{
				num ^= MatchmakerResult.GetHashCode();
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
			if (HasUpdateInfo != queueUpdateNotification.HasUpdateInfo || (HasUpdateInfo && !UpdateInfo.Equals(queueUpdateNotification.UpdateInfo)))
			{
				return false;
			}
			if (HasMatchmaking != queueUpdateNotification.HasMatchmaking || (HasMatchmaking && !Matchmaking.Equals(queueUpdateNotification.Matchmaking)))
			{
				return false;
			}
			if (HasMatchmakerResult != queueUpdateNotification.HasMatchmakerResult || (HasMatchmakerResult && !MatchmakerResult.Equals(queueUpdateNotification.MatchmakerResult)))
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
				case 24:
					instance.Matchmaking = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.MatchmakerResult = ProtocolParser.ReadUInt32(stream);
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
				FindGameRequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.HasUpdateInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.UpdateInfo.GetSerializedSize());
				QueueUpdate.Serialize(stream, instance.UpdateInfo);
			}
			if (instance.HasMatchmaking)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Matchmaking);
			}
			if (instance.HasMatchmakerResult)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.MatchmakerResult);
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
			if (HasMatchmaking)
			{
				num++;
				num++;
			}
			if (HasMatchmakerResult)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(MatchmakerResult);
			}
			return num;
		}
	}
}
