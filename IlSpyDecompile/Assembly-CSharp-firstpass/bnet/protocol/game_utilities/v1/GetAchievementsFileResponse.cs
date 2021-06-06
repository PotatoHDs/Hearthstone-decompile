using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	public class GetAchievementsFileResponse : IProtoBuf
	{
		public bool HasContentHandle;

		private ContentHandle _ContentHandle;

		public ContentHandle ContentHandle
		{
			get
			{
				return _ContentHandle;
			}
			set
			{
				_ContentHandle = value;
				HasContentHandle = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetContentHandle(ContentHandle val)
		{
			ContentHandle = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasContentHandle)
			{
				num ^= ContentHandle.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetAchievementsFileResponse getAchievementsFileResponse = obj as GetAchievementsFileResponse;
			if (getAchievementsFileResponse == null)
			{
				return false;
			}
			if (HasContentHandle != getAchievementsFileResponse.HasContentHandle || (HasContentHandle && !ContentHandle.Equals(getAchievementsFileResponse.ContentHandle)))
			{
				return false;
			}
			return true;
		}

		public static GetAchievementsFileResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAchievementsFileResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetAchievementsFileResponse Deserialize(Stream stream, GetAchievementsFileResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetAchievementsFileResponse DeserializeLengthDelimited(Stream stream)
		{
			GetAchievementsFileResponse getAchievementsFileResponse = new GetAchievementsFileResponse();
			DeserializeLengthDelimited(stream, getAchievementsFileResponse);
			return getAchievementsFileResponse;
		}

		public static GetAchievementsFileResponse DeserializeLengthDelimited(Stream stream, GetAchievementsFileResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetAchievementsFileResponse Deserialize(Stream stream, GetAchievementsFileResponse instance, long limit)
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
					if (instance.ContentHandle == null)
					{
						instance.ContentHandle = ContentHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						ContentHandle.DeserializeLengthDelimited(stream, instance.ContentHandle);
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

		public static void Serialize(Stream stream, GetAchievementsFileResponse instance)
		{
			if (instance.HasContentHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ContentHandle.GetSerializedSize());
				ContentHandle.Serialize(stream, instance.ContentHandle);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasContentHandle)
			{
				num++;
				uint serializedSize = ContentHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
