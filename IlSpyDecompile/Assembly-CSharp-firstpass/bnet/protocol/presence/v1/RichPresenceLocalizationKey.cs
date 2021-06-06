using System.IO;

namespace bnet.protocol.presence.v1
{
	public class RichPresenceLocalizationKey : IProtoBuf
	{
		public uint Program { get; set; }

		public uint Stream { get; set; }

		public uint LocalizationId { get; set; }

		public bool IsInitialized => true;

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetStream(uint val)
		{
			Stream = val;
		}

		public void SetLocalizationId(uint val)
		{
			LocalizationId = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Program.GetHashCode() ^ Stream.GetHashCode() ^ LocalizationId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			RichPresenceLocalizationKey richPresenceLocalizationKey = obj as RichPresenceLocalizationKey;
			if (richPresenceLocalizationKey == null)
			{
				return false;
			}
			if (!Program.Equals(richPresenceLocalizationKey.Program))
			{
				return false;
			}
			if (!Stream.Equals(richPresenceLocalizationKey.Stream))
			{
				return false;
			}
			if (!LocalizationId.Equals(richPresenceLocalizationKey.LocalizationId))
			{
				return false;
			}
			return true;
		}

		public static RichPresenceLocalizationKey ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RichPresenceLocalizationKey>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RichPresenceLocalizationKey Deserialize(Stream stream, RichPresenceLocalizationKey instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RichPresenceLocalizationKey DeserializeLengthDelimited(Stream stream)
		{
			RichPresenceLocalizationKey richPresenceLocalizationKey = new RichPresenceLocalizationKey();
			DeserializeLengthDelimited(stream, richPresenceLocalizationKey);
			return richPresenceLocalizationKey;
		}

		public static RichPresenceLocalizationKey DeserializeLengthDelimited(Stream stream, RichPresenceLocalizationKey instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RichPresenceLocalizationKey Deserialize(Stream stream, RichPresenceLocalizationKey instance, long limit)
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
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 21:
					instance.Stream = binaryReader.ReadUInt32();
					continue;
				case 24:
					instance.LocalizationId = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, RichPresenceLocalizationKey instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Program);
			stream.WriteByte(21);
			binaryWriter.Write(instance.Stream);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.LocalizationId);
		}

		public uint GetSerializedSize()
		{
			return 0 + 4 + 4 + ProtocolParser.SizeOfUInt32(LocalizationId) + 3;
		}
	}
}
