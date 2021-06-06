using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.matchmaking.v1
{
	public class QueueEntryNotification : IProtoBuf
	{
		public bool HasRequestId;

		private RequestId _RequestId;

		public bool HasWaitTimes;

		private QueueWaitTimes _WaitTimes;

		private List<GameAccountHandle> _Member = new List<GameAccountHandle>();

		public bool HasRequestInitiator;

		private GameAccountHandle _RequestInitiator;

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

		public List<GameAccountHandle> Member
		{
			get
			{
				return _Member;
			}
			set
			{
				_Member = value;
			}
		}

		public List<GameAccountHandle> MemberList => _Member;

		public int MemberCount => _Member.Count;

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

		public void SetRequestId(RequestId val)
		{
			RequestId = val;
		}

		public void SetWaitTimes(QueueWaitTimes val)
		{
			WaitTimes = val;
		}

		public void AddMember(GameAccountHandle val)
		{
			_Member.Add(val);
		}

		public void ClearMember()
		{
			_Member.Clear();
		}

		public void SetMember(List<GameAccountHandle> val)
		{
			Member = val;
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
			if (HasWaitTimes)
			{
				num ^= WaitTimes.GetHashCode();
			}
			foreach (GameAccountHandle item in Member)
			{
				num ^= item.GetHashCode();
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
			if (HasWaitTimes != queueEntryNotification.HasWaitTimes || (HasWaitTimes && !WaitTimes.Equals(queueEntryNotification.WaitTimes)))
			{
				return false;
			}
			if (Member.Count != queueEntryNotification.Member.Count)
			{
				return false;
			}
			for (int i = 0; i < Member.Count; i++)
			{
				if (!Member[i].Equals(queueEntryNotification.Member[i]))
				{
					return false;
				}
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
			if (instance.Member == null)
			{
				instance.Member = new List<GameAccountHandle>();
			}
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
				case 26:
					instance.Member.Add(GameAccountHandle.DeserializeLengthDelimited(stream));
					continue;
				case 34:
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
				RequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.HasWaitTimes)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.WaitTimes.GetSerializedSize());
				QueueWaitTimes.Serialize(stream, instance.WaitTimes);
			}
			if (instance.Member.Count > 0)
			{
				foreach (GameAccountHandle item in instance.Member)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					GameAccountHandle.Serialize(stream, item);
				}
			}
			if (instance.HasRequestInitiator)
			{
				stream.WriteByte(34);
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
			if (HasWaitTimes)
			{
				num++;
				uint serializedSize2 = WaitTimes.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (Member.Count > 0)
			{
				foreach (GameAccountHandle item in Member)
				{
					num++;
					uint serializedSize3 = item.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (HasRequestInitiator)
			{
				num++;
				uint serializedSize4 = RequestInitiator.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}
	}
}
