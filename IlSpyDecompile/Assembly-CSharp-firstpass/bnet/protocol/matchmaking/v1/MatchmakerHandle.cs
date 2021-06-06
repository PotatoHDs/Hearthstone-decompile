using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class MatchmakerHandle : IProtoBuf
	{
		public bool HasAddr;

		private HostProxyPair _Addr;

		public bool HasId;

		private uint _Id;

		public HostProxyPair Addr
		{
			get
			{
				return _Addr;
			}
			set
			{
				_Addr = value;
				HasAddr = value != null;
			}
		}

		public uint Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				HasId = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAddr(HostProxyPair val)
		{
			Addr = val;
		}

		public void SetId(uint val)
		{
			Id = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAddr)
			{
				num ^= Addr.GetHashCode();
			}
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MatchmakerHandle matchmakerHandle = obj as MatchmakerHandle;
			if (matchmakerHandle == null)
			{
				return false;
			}
			if (HasAddr != matchmakerHandle.HasAddr || (HasAddr && !Addr.Equals(matchmakerHandle.Addr)))
			{
				return false;
			}
			if (HasId != matchmakerHandle.HasId || (HasId && !Id.Equals(matchmakerHandle.Id)))
			{
				return false;
			}
			return true;
		}

		public static MatchmakerHandle ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MatchmakerHandle>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MatchmakerHandle Deserialize(Stream stream, MatchmakerHandle instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MatchmakerHandle DeserializeLengthDelimited(Stream stream)
		{
			MatchmakerHandle matchmakerHandle = new MatchmakerHandle();
			DeserializeLengthDelimited(stream, matchmakerHandle);
			return matchmakerHandle;
		}

		public static MatchmakerHandle DeserializeLengthDelimited(Stream stream, MatchmakerHandle instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MatchmakerHandle Deserialize(Stream stream, MatchmakerHandle instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (instance.Addr == null)
					{
						instance.Addr = HostProxyPair.DeserializeLengthDelimited(stream);
					}
					else
					{
						HostProxyPair.DeserializeLengthDelimited(stream, instance.Addr);
					}
					continue;
				case 21:
					instance.Id = binaryReader.ReadUInt32();
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

		public static void Serialize(Stream stream, MatchmakerHandle instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAddr)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Addr.GetSerializedSize());
				HostProxyPair.Serialize(stream, instance.Addr);
			}
			if (instance.HasId)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Id);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAddr)
			{
				num++;
				uint serializedSize = Addr.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasId)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
