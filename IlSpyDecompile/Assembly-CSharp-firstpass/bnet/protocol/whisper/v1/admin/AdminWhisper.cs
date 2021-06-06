using System.IO;

namespace bnet.protocol.whisper.v1.admin
{
	public class AdminWhisper : IProtoBuf
	{
		public bool HasWhisper;

		private Whisper _Whisper;

		public Whisper Whisper
		{
			get
			{
				return _Whisper;
			}
			set
			{
				_Whisper = value;
				HasWhisper = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetWhisper(Whisper val)
		{
			Whisper = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasWhisper)
			{
				num ^= Whisper.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AdminWhisper adminWhisper = obj as AdminWhisper;
			if (adminWhisper == null)
			{
				return false;
			}
			if (HasWhisper != adminWhisper.HasWhisper || (HasWhisper && !Whisper.Equals(adminWhisper.Whisper)))
			{
				return false;
			}
			return true;
		}

		public static AdminWhisper ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AdminWhisper>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AdminWhisper Deserialize(Stream stream, AdminWhisper instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AdminWhisper DeserializeLengthDelimited(Stream stream)
		{
			AdminWhisper adminWhisper = new AdminWhisper();
			DeserializeLengthDelimited(stream, adminWhisper);
			return adminWhisper;
		}

		public static AdminWhisper DeserializeLengthDelimited(Stream stream, AdminWhisper instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AdminWhisper Deserialize(Stream stream, AdminWhisper instance, long limit)
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
					if (instance.Whisper == null)
					{
						instance.Whisper = Whisper.DeserializeLengthDelimited(stream);
					}
					else
					{
						Whisper.DeserializeLengthDelimited(stream, instance.Whisper);
					}
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

		public static void Serialize(Stream stream, AdminWhisper instance)
		{
			if (instance.HasWhisper)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Whisper.GetSerializedSize());
				Whisper.Serialize(stream, instance.Whisper);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasWhisper)
			{
				num++;
				uint serializedSize = Whisper.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
