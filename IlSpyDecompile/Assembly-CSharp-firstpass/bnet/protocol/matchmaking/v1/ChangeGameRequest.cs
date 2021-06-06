using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class ChangeGameRequest : IProtoBuf
	{
		public bool HasOptions;

		private UpdateGameOptions _Options;

		public UpdateGameOptions Options
		{
			get
			{
				return _Options;
			}
			set
			{
				_Options = value;
				HasOptions = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetOptions(UpdateGameOptions val)
		{
			Options = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasOptions)
			{
				num ^= Options.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChangeGameRequest changeGameRequest = obj as ChangeGameRequest;
			if (changeGameRequest == null)
			{
				return false;
			}
			if (HasOptions != changeGameRequest.HasOptions || (HasOptions && !Options.Equals(changeGameRequest.Options)))
			{
				return false;
			}
			return true;
		}

		public static ChangeGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChangeGameRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChangeGameRequest Deserialize(Stream stream, ChangeGameRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChangeGameRequest DeserializeLengthDelimited(Stream stream)
		{
			ChangeGameRequest changeGameRequest = new ChangeGameRequest();
			DeserializeLengthDelimited(stream, changeGameRequest);
			return changeGameRequest;
		}

		public static ChangeGameRequest DeserializeLengthDelimited(Stream stream, ChangeGameRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChangeGameRequest Deserialize(Stream stream, ChangeGameRequest instance, long limit)
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
					if (instance.Options == null)
					{
						instance.Options = UpdateGameOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						UpdateGameOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		public static void Serialize(Stream stream, ChangeGameRequest instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				UpdateGameOptions.Serialize(stream, instance.Options);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasOptions)
			{
				num++;
				uint serializedSize = Options.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
