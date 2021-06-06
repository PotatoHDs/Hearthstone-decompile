using System.IO;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	public class QueueExitNotification : IProtoBuf
	{
		public bool HasRequestId;

		private FindGameRequestId _RequestId;

		public bool HasResult;

		private uint _Result;

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

		public uint Result
		{
			get
			{
				return _Result;
			}
			set
			{
				_Result = value;
				HasResult = true;
			}
		}

		public bool IsInitialized => true;

		public void SetRequestId(FindGameRequestId val)
		{
			RequestId = val;
		}

		public void SetResult(uint val)
		{
			Result = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRequestId)
			{
				num ^= RequestId.GetHashCode();
			}
			if (HasResult)
			{
				num ^= Result.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			QueueExitNotification queueExitNotification = obj as QueueExitNotification;
			if (queueExitNotification == null)
			{
				return false;
			}
			if (HasRequestId != queueExitNotification.HasRequestId || (HasRequestId && !RequestId.Equals(queueExitNotification.RequestId)))
			{
				return false;
			}
			if (HasResult != queueExitNotification.HasResult || (HasResult && !Result.Equals(queueExitNotification.Result)))
			{
				return false;
			}
			return true;
		}

		public static QueueExitNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueExitNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static QueueExitNotification Deserialize(Stream stream, QueueExitNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static QueueExitNotification DeserializeLengthDelimited(Stream stream)
		{
			QueueExitNotification queueExitNotification = new QueueExitNotification();
			DeserializeLengthDelimited(stream, queueExitNotification);
			return queueExitNotification;
		}

		public static QueueExitNotification DeserializeLengthDelimited(Stream stream, QueueExitNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static QueueExitNotification Deserialize(Stream stream, QueueExitNotification instance, long limit)
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
				case 16:
					instance.Result = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, QueueExitNotification instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				FindGameRequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.HasResult)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Result);
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
			if (HasResult)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Result);
			}
			return num;
		}
	}
}
