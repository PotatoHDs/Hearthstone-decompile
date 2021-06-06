using System.IO;

namespace bnet.protocol.authentication.v1
{
	public class ServerStateChangeRequest : IProtoBuf
	{
		public uint State { get; set; }

		public ulong EventTime { get; set; }

		public bool IsInitialized => true;

		public void SetState(uint val)
		{
			State = val;
		}

		public void SetEventTime(ulong val)
		{
			EventTime = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ State.GetHashCode() ^ EventTime.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ServerStateChangeRequest serverStateChangeRequest = obj as ServerStateChangeRequest;
			if (serverStateChangeRequest == null)
			{
				return false;
			}
			if (!State.Equals(serverStateChangeRequest.State))
			{
				return false;
			}
			if (!EventTime.Equals(serverStateChangeRequest.EventTime))
			{
				return false;
			}
			return true;
		}

		public static ServerStateChangeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ServerStateChangeRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ServerStateChangeRequest Deserialize(Stream stream, ServerStateChangeRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ServerStateChangeRequest DeserializeLengthDelimited(Stream stream)
		{
			ServerStateChangeRequest serverStateChangeRequest = new ServerStateChangeRequest();
			DeserializeLengthDelimited(stream, serverStateChangeRequest);
			return serverStateChangeRequest;
		}

		public static ServerStateChangeRequest DeserializeLengthDelimited(Stream stream, ServerStateChangeRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ServerStateChangeRequest Deserialize(Stream stream, ServerStateChangeRequest instance, long limit)
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
					instance.State = ProtocolParser.ReadUInt32(stream);
					continue;
				case 16:
					instance.EventTime = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ServerStateChangeRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.State);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.EventTime);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt32(State) + ProtocolParser.SizeOfUInt64(EventTime) + 2;
		}
	}
}
