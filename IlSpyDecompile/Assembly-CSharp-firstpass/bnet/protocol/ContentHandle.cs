using System;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	public class ContentHandle : IProtoBuf
	{
		public bool HasProtoUrl;

		private string _ProtoUrl;

		public uint Region { get; set; }

		public uint Usage { get; set; }

		public byte[] Hash { get; set; }

		public string ProtoUrl
		{
			get
			{
				return _ProtoUrl;
			}
			set
			{
				_ProtoUrl = value;
				HasProtoUrl = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetRegion(uint val)
		{
			Region = val;
		}

		public void SetUsage(uint val)
		{
			Usage = val;
		}

		public void SetHash(byte[] val)
		{
			Hash = val;
		}

		public void SetProtoUrl(string val)
		{
			ProtoUrl = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Region.GetHashCode();
			hashCode ^= Usage.GetHashCode();
			hashCode ^= Hash.GetHashCode();
			if (HasProtoUrl)
			{
				hashCode ^= ProtoUrl.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ContentHandle contentHandle = obj as ContentHandle;
			if (contentHandle == null)
			{
				return false;
			}
			if (!Region.Equals(contentHandle.Region))
			{
				return false;
			}
			if (!Usage.Equals(contentHandle.Usage))
			{
				return false;
			}
			if (!Hash.Equals(contentHandle.Hash))
			{
				return false;
			}
			if (HasProtoUrl != contentHandle.HasProtoUrl || (HasProtoUrl && !ProtoUrl.Equals(contentHandle.ProtoUrl)))
			{
				return false;
			}
			return true;
		}

		public static ContentHandle ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ContentHandle>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ContentHandle Deserialize(Stream stream, ContentHandle instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ContentHandle DeserializeLengthDelimited(Stream stream)
		{
			ContentHandle contentHandle = new ContentHandle();
			DeserializeLengthDelimited(stream, contentHandle);
			return contentHandle;
		}

		public static ContentHandle DeserializeLengthDelimited(Stream stream, ContentHandle instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ContentHandle Deserialize(Stream stream, ContentHandle instance, long limit)
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
				case 13:
					instance.Region = binaryReader.ReadUInt32();
					continue;
				case 21:
					instance.Usage = binaryReader.ReadUInt32();
					continue;
				case 26:
					instance.Hash = ProtocolParser.ReadBytes(stream);
					continue;
				case 34:
					instance.ProtoUrl = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, ContentHandle instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Region);
			stream.WriteByte(21);
			binaryWriter.Write(instance.Usage);
			if (instance.Hash == null)
			{
				throw new ArgumentNullException("Hash", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, instance.Hash);
			if (instance.HasProtoUrl)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProtoUrl));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 4;
			num += 4;
			num += (uint)((int)ProtocolParser.SizeOfUInt32(Hash.Length) + Hash.Length);
			if (HasProtoUrl)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ProtoUrl);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 3;
		}
	}
}
