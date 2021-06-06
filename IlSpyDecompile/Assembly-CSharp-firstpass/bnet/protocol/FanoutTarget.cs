using System.IO;
using System.Text;

namespace bnet.protocol
{
	public class FanoutTarget : IProtoBuf
	{
		public bool HasClientId;

		private string _ClientId;

		public bool HasKey;

		private byte[] _Key;

		public string ClientId
		{
			get
			{
				return _ClientId;
			}
			set
			{
				_ClientId = value;
				HasClientId = value != null;
			}
		}

		public byte[] Key
		{
			get
			{
				return _Key;
			}
			set
			{
				_Key = value;
				HasKey = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetClientId(string val)
		{
			ClientId = val;
		}

		public void SetKey(byte[] val)
		{
			Key = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasClientId)
			{
				num ^= ClientId.GetHashCode();
			}
			if (HasKey)
			{
				num ^= Key.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FanoutTarget fanoutTarget = obj as FanoutTarget;
			if (fanoutTarget == null)
			{
				return false;
			}
			if (HasClientId != fanoutTarget.HasClientId || (HasClientId && !ClientId.Equals(fanoutTarget.ClientId)))
			{
				return false;
			}
			if (HasKey != fanoutTarget.HasKey || (HasKey && !Key.Equals(fanoutTarget.Key)))
			{
				return false;
			}
			return true;
		}

		public static FanoutTarget ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FanoutTarget>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FanoutTarget Deserialize(Stream stream, FanoutTarget instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FanoutTarget DeserializeLengthDelimited(Stream stream)
		{
			FanoutTarget fanoutTarget = new FanoutTarget();
			DeserializeLengthDelimited(stream, fanoutTarget);
			return fanoutTarget;
		}

		public static FanoutTarget DeserializeLengthDelimited(Stream stream, FanoutTarget instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FanoutTarget Deserialize(Stream stream, FanoutTarget instance, long limit)
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
					instance.ClientId = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.Key = ProtocolParser.ReadBytes(stream);
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

		public static void Serialize(Stream stream, FanoutTarget instance)
		{
			if (instance.HasClientId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
			if (instance.HasKey)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.Key);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasClientId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasKey)
			{
				num++;
				num += (uint)((int)ProtocolParser.SizeOfUInt32(Key.Length) + Key.Length);
			}
			return num;
		}
	}
}
