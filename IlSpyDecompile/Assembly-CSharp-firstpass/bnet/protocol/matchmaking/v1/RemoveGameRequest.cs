using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class RemoveGameRequest : IProtoBuf
	{
		public bool HasOptions;

		private RemoveGameOptions _Options;

		public RemoveGameOptions Options
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

		public void SetOptions(RemoveGameOptions val)
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
			RemoveGameRequest removeGameRequest = obj as RemoveGameRequest;
			if (removeGameRequest == null)
			{
				return false;
			}
			if (HasOptions != removeGameRequest.HasOptions || (HasOptions && !Options.Equals(removeGameRequest.Options)))
			{
				return false;
			}
			return true;
		}

		public static RemoveGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemoveGameRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RemoveGameRequest Deserialize(Stream stream, RemoveGameRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RemoveGameRequest DeserializeLengthDelimited(Stream stream)
		{
			RemoveGameRequest removeGameRequest = new RemoveGameRequest();
			DeserializeLengthDelimited(stream, removeGameRequest);
			return removeGameRequest;
		}

		public static RemoveGameRequest DeserializeLengthDelimited(Stream stream, RemoveGameRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RemoveGameRequest Deserialize(Stream stream, RemoveGameRequest instance, long limit)
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
						instance.Options = RemoveGameOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						RemoveGameOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		public static void Serialize(Stream stream, RemoveGameRequest instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				RemoveGameOptions.Serialize(stream, instance.Options);
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
