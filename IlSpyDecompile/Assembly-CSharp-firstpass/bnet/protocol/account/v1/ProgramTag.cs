using System.IO;

namespace bnet.protocol.account.v1
{
	public class ProgramTag : IProtoBuf
	{
		public bool HasProgram;

		private uint _Program;

		public bool HasTag;

		private uint _Tag;

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public uint Tag
		{
			get
			{
				return _Tag;
			}
			set
			{
				_Tag = value;
				HasTag = true;
			}
		}

		public bool IsInitialized => true;

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetTag(uint val)
		{
			Tag = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasTag)
			{
				num ^= Tag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ProgramTag programTag = obj as ProgramTag;
			if (programTag == null)
			{
				return false;
			}
			if (HasProgram != programTag.HasProgram || (HasProgram && !Program.Equals(programTag.Program)))
			{
				return false;
			}
			if (HasTag != programTag.HasTag || (HasTag && !Tag.Equals(programTag.Tag)))
			{
				return false;
			}
			return true;
		}

		public static ProgramTag ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ProgramTag>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProgramTag Deserialize(Stream stream, ProgramTag instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProgramTag DeserializeLengthDelimited(Stream stream)
		{
			ProgramTag programTag = new ProgramTag();
			DeserializeLengthDelimited(stream, programTag);
			return programTag;
		}

		public static ProgramTag DeserializeLengthDelimited(Stream stream, ProgramTag instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProgramTag Deserialize(Stream stream, ProgramTag instance, long limit)
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
					instance.Tag = binaryReader.ReadUInt32();
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

		public static void Serialize(Stream stream, ProgramTag instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasProgram)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasTag)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Tag);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (HasTag)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
