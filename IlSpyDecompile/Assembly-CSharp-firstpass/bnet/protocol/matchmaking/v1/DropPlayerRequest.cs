using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	public class DropPlayerRequest : IProtoBuf
	{
		public bool HasOptions;

		private RemovePlayerOptions _Options;

		public RemovePlayerOptions Options
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

		public void SetOptions(RemovePlayerOptions val)
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
			DropPlayerRequest dropPlayerRequest = obj as DropPlayerRequest;
			if (dropPlayerRequest == null)
			{
				return false;
			}
			if (HasOptions != dropPlayerRequest.HasOptions || (HasOptions && !Options.Equals(dropPlayerRequest.Options)))
			{
				return false;
			}
			return true;
		}

		public static DropPlayerRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DropPlayerRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DropPlayerRequest Deserialize(Stream stream, DropPlayerRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DropPlayerRequest DeserializeLengthDelimited(Stream stream)
		{
			DropPlayerRequest dropPlayerRequest = new DropPlayerRequest();
			DeserializeLengthDelimited(stream, dropPlayerRequest);
			return dropPlayerRequest;
		}

		public static DropPlayerRequest DeserializeLengthDelimited(Stream stream, DropPlayerRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DropPlayerRequest Deserialize(Stream stream, DropPlayerRequest instance, long limit)
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
						instance.Options = RemovePlayerOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						RemovePlayerOptions.DeserializeLengthDelimited(stream, instance.Options);
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

		public static void Serialize(Stream stream, DropPlayerRequest instance)
		{
			if (instance.HasOptions)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				RemovePlayerOptions.Serialize(stream, instance.Options);
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
