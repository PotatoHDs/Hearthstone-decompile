using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	public class GetAchievementsFileRequest : IProtoBuf
	{
		public bool HasHost;

		private ProcessId _Host;

		public ProcessId Host
		{
			get
			{
				return _Host;
			}
			set
			{
				_Host = value;
				HasHost = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetHost(ProcessId val)
		{
			Host = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasHost)
			{
				num ^= Host.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetAchievementsFileRequest getAchievementsFileRequest = obj as GetAchievementsFileRequest;
			if (getAchievementsFileRequest == null)
			{
				return false;
			}
			if (HasHost != getAchievementsFileRequest.HasHost || (HasHost && !Host.Equals(getAchievementsFileRequest.Host)))
			{
				return false;
			}
			return true;
		}

		public static GetAchievementsFileRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAchievementsFileRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetAchievementsFileRequest Deserialize(Stream stream, GetAchievementsFileRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetAchievementsFileRequest DeserializeLengthDelimited(Stream stream)
		{
			GetAchievementsFileRequest getAchievementsFileRequest = new GetAchievementsFileRequest();
			DeserializeLengthDelimited(stream, getAchievementsFileRequest);
			return getAchievementsFileRequest;
		}

		public static GetAchievementsFileRequest DeserializeLengthDelimited(Stream stream, GetAchievementsFileRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetAchievementsFileRequest Deserialize(Stream stream, GetAchievementsFileRequest instance, long limit)
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
					if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
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

		public static void Serialize(Stream stream, GetAchievementsFileRequest instance)
		{
			if (instance.HasHost)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasHost)
			{
				num++;
				uint serializedSize = Host.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
