using System.IO;
using System.Text;

namespace bnet.protocol.broadcast.v1
{
	public class StartBroadcastRequest : IProtoBuf
	{
		public bool HasBroadcast;

		private Broadcast _Broadcast;

		public bool HasId;

		private string _Id;

		public Broadcast Broadcast
		{
			get
			{
				return _Broadcast;
			}
			set
			{
				_Broadcast = value;
				HasBroadcast = value != null;
			}
		}

		public string Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				HasId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetBroadcast(Broadcast val)
		{
			Broadcast = val;
		}

		public void SetId(string val)
		{
			Id = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasBroadcast)
			{
				num ^= Broadcast.GetHashCode();
			}
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			StartBroadcastRequest startBroadcastRequest = obj as StartBroadcastRequest;
			if (startBroadcastRequest == null)
			{
				return false;
			}
			if (HasBroadcast != startBroadcastRequest.HasBroadcast || (HasBroadcast && !Broadcast.Equals(startBroadcastRequest.Broadcast)))
			{
				return false;
			}
			if (HasId != startBroadcastRequest.HasId || (HasId && !Id.Equals(startBroadcastRequest.Id)))
			{
				return false;
			}
			return true;
		}

		public static StartBroadcastRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<StartBroadcastRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static StartBroadcastRequest Deserialize(Stream stream, StartBroadcastRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static StartBroadcastRequest DeserializeLengthDelimited(Stream stream)
		{
			StartBroadcastRequest startBroadcastRequest = new StartBroadcastRequest();
			DeserializeLengthDelimited(stream, startBroadcastRequest);
			return startBroadcastRequest;
		}

		public static StartBroadcastRequest DeserializeLengthDelimited(Stream stream, StartBroadcastRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static StartBroadcastRequest Deserialize(Stream stream, StartBroadcastRequest instance, long limit)
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
					if (instance.Broadcast == null)
					{
						instance.Broadcast = Broadcast.DeserializeLengthDelimited(stream);
					}
					else
					{
						Broadcast.DeserializeLengthDelimited(stream, instance.Broadcast);
					}
					continue;
				case 18:
					instance.Id = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, StartBroadcastRequest instance)
		{
			if (instance.HasBroadcast)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Broadcast.GetSerializedSize());
				Broadcast.Serialize(stream, instance.Broadcast);
			}
			if (instance.HasId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Id));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasBroadcast)
			{
				num++;
				uint serializedSize = Broadcast.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Id);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
