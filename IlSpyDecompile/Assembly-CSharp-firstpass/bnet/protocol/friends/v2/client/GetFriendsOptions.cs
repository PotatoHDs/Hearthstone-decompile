using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class GetFriendsOptions : IProtoBuf
	{
		public bool HasFetchNames;

		private bool _FetchNames;

		public bool FetchNames
		{
			get
			{
				return _FetchNames;
			}
			set
			{
				_FetchNames = value;
				HasFetchNames = true;
			}
		}

		public bool IsInitialized => true;

		public void SetFetchNames(bool val)
		{
			FetchNames = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasFetchNames)
			{
				num ^= FetchNames.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetFriendsOptions getFriendsOptions = obj as GetFriendsOptions;
			if (getFriendsOptions == null)
			{
				return false;
			}
			if (HasFetchNames != getFriendsOptions.HasFetchNames || (HasFetchNames && !FetchNames.Equals(getFriendsOptions.FetchNames)))
			{
				return false;
			}
			return true;
		}

		public static GetFriendsOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFriendsOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetFriendsOptions Deserialize(Stream stream, GetFriendsOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetFriendsOptions DeserializeLengthDelimited(Stream stream)
		{
			GetFriendsOptions getFriendsOptions = new GetFriendsOptions();
			DeserializeLengthDelimited(stream, getFriendsOptions);
			return getFriendsOptions;
		}

		public static GetFriendsOptions DeserializeLengthDelimited(Stream stream, GetFriendsOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetFriendsOptions Deserialize(Stream stream, GetFriendsOptions instance, long limit)
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
					instance.FetchNames = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, GetFriendsOptions instance)
		{
			if (instance.HasFetchNames)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.FetchNames);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasFetchNames)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
