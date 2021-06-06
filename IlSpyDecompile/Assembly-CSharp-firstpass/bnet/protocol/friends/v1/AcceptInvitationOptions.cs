using System.IO;

namespace bnet.protocol.friends.v1
{
	public class AcceptInvitationOptions : IProtoBuf
	{
		public bool HasRole;

		private uint _Role;

		public bool HasProgram;

		private uint _Program;

		public uint Role
		{
			get
			{
				return _Role;
			}
			set
			{
				_Role = value;
				HasRole = true;
			}
		}

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

		public bool IsInitialized => true;

		public void SetRole(uint val)
		{
			Role = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasRole)
			{
				num ^= Role.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AcceptInvitationOptions acceptInvitationOptions = obj as AcceptInvitationOptions;
			if (acceptInvitationOptions == null)
			{
				return false;
			}
			if (HasRole != acceptInvitationOptions.HasRole || (HasRole && !Role.Equals(acceptInvitationOptions.Role)))
			{
				return false;
			}
			if (HasProgram != acceptInvitationOptions.HasProgram || (HasProgram && !Program.Equals(acceptInvitationOptions.Program)))
			{
				return false;
			}
			return true;
		}

		public static AcceptInvitationOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AcceptInvitationOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AcceptInvitationOptions Deserialize(Stream stream, AcceptInvitationOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AcceptInvitationOptions DeserializeLengthDelimited(Stream stream)
		{
			AcceptInvitationOptions acceptInvitationOptions = new AcceptInvitationOptions();
			DeserializeLengthDelimited(stream, acceptInvitationOptions);
			return acceptInvitationOptions;
		}

		public static AcceptInvitationOptions DeserializeLengthDelimited(Stream stream, AcceptInvitationOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AcceptInvitationOptions Deserialize(Stream stream, AcceptInvitationOptions instance, long limit)
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
				case 8:
					instance.Role = ProtocolParser.ReadUInt32(stream);
					continue;
				case 21:
					instance.Program = binaryReader.ReadUInt32();
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

		public static void Serialize(Stream stream, AcceptInvitationOptions instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasRole)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.Role);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasRole)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Role);
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
