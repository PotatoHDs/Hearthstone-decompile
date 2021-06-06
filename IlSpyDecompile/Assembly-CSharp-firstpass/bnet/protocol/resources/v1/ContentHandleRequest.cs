using System.IO;

namespace bnet.protocol.resources.v1
{
	public class ContentHandleRequest : IProtoBuf
	{
		public bool HasVersion;

		private uint _Version;

		public uint Program { get; set; }

		public uint Stream { get; set; }

		public uint Version
		{
			get
			{
				return _Version;
			}
			set
			{
				_Version = value;
				HasVersion = true;
			}
		}

		public bool IsInitialized => true;

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetStream(uint val)
		{
			Stream = val;
		}

		public void SetVersion(uint val)
		{
			Version = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Program.GetHashCode();
			hashCode ^= Stream.GetHashCode();
			if (HasVersion)
			{
				hashCode ^= Version.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ContentHandleRequest contentHandleRequest = obj as ContentHandleRequest;
			if (contentHandleRequest == null)
			{
				return false;
			}
			if (!Program.Equals(contentHandleRequest.Program))
			{
				return false;
			}
			if (!Stream.Equals(contentHandleRequest.Stream))
			{
				return false;
			}
			if (HasVersion != contentHandleRequest.HasVersion || (HasVersion && !Version.Equals(contentHandleRequest.Version)))
			{
				return false;
			}
			return true;
		}

		public static ContentHandleRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ContentHandleRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ContentHandleRequest Deserialize(Stream stream, ContentHandleRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ContentHandleRequest DeserializeLengthDelimited(Stream stream)
		{
			ContentHandleRequest contentHandleRequest = new ContentHandleRequest();
			DeserializeLengthDelimited(stream, contentHandleRequest);
			return contentHandleRequest;
		}

		public static ContentHandleRequest DeserializeLengthDelimited(Stream stream, ContentHandleRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ContentHandleRequest Deserialize(Stream stream, ContentHandleRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Version = 1701729619u;
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
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 21:
					instance.Stream = binaryReader.ReadUInt32();
					continue;
				case 29:
					instance.Version = binaryReader.ReadUInt32();
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

		public static void Serialize(Stream stream, ContentHandleRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Program);
			stream.WriteByte(21);
			binaryWriter.Write(instance.Stream);
			if (instance.HasVersion)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.Version);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 4;
			num += 4;
			if (HasVersion)
			{
				num++;
				num += 4;
			}
			return num + 2;
		}
	}
}
