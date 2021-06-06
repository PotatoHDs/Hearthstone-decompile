using System.IO;
using System.Text;

namespace bnet.protocol
{
	public class Provider : IProtoBuf
	{
		public bool HasName;

		private string _Name;

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
				HasName = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetName(string val)
		{
			Name = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasName)
			{
				num ^= Name.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Provider provider = obj as Provider;
			if (provider == null)
			{
				return false;
			}
			if (HasName != provider.HasName || (HasName && !Name.Equals(provider.Name)))
			{
				return false;
			}
			return true;
		}

		public static Provider ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Provider>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Provider Deserialize(Stream stream, Provider instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Provider DeserializeLengthDelimited(Stream stream)
		{
			Provider provider = new Provider();
			DeserializeLengthDelimited(stream, provider);
			return provider;
		}

		public static Provider DeserializeLengthDelimited(Stream stream, Provider instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Provider Deserialize(Stream stream, Provider instance, long limit)
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
					instance.Name = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, Provider instance)
		{
			if (instance.HasName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}
	}
}
