using System.IO;

namespace bnet.protocol
{
	public class ProcessId : IProtoBuf
	{
		public uint Label { get; set; }

		public uint Epoch { get; set; }

		public bool IsInitialized => true;

		public void SetLabel(uint val)
		{
			Label = val;
		}

		public void SetEpoch(uint val)
		{
			Epoch = val;
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ Label.GetHashCode() ^ Epoch.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProcessId processId = obj as ProcessId;
			if (processId == null)
			{
				return false;
			}
			if (!Label.Equals(processId.Label))
			{
				return false;
			}
			if (!Epoch.Equals(processId.Epoch))
			{
				return false;
			}
			return true;
		}

		public static ProcessId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ProcessId>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ProcessId Deserialize(Stream stream, ProcessId instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ProcessId DeserializeLengthDelimited(Stream stream)
		{
			ProcessId processId = new ProcessId();
			DeserializeLengthDelimited(stream, processId);
			return processId;
		}

		public static ProcessId DeserializeLengthDelimited(Stream stream, ProcessId instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ProcessId Deserialize(Stream stream, ProcessId instance, long limit)
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
				case 8:
					instance.Label = ProtocolParser.ReadUInt32(stream);
					continue;
				case 16:
					instance.Epoch = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, ProcessId instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Label);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Epoch);
		}

		public uint GetSerializedSize()
		{
			return 0 + ProtocolParser.SizeOfUInt32(Label) + ProtocolParser.SizeOfUInt32(Epoch) + 2;
		}
	}
}
