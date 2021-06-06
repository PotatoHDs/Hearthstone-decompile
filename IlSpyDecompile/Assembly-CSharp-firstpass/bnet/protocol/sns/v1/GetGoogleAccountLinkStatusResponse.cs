using System.IO;
using System.Text;

namespace bnet.protocol.sns.v1
{
	public class GetGoogleAccountLinkStatusResponse : IProtoBuf
	{
		public bool HasAccountLinked;

		private bool _AccountLinked;

		public bool HasGoogleId;

		private string _GoogleId;

		public bool HasDisplayName;

		private string _DisplayName;

		public bool AccountLinked
		{
			get
			{
				return _AccountLinked;
			}
			set
			{
				_AccountLinked = value;
				HasAccountLinked = true;
			}
		}

		public string GoogleId
		{
			get
			{
				return _GoogleId;
			}
			set
			{
				_GoogleId = value;
				HasGoogleId = value != null;
			}
		}

		public string DisplayName
		{
			get
			{
				return _DisplayName;
			}
			set
			{
				_DisplayName = value;
				HasDisplayName = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAccountLinked(bool val)
		{
			AccountLinked = val;
		}

		public void SetGoogleId(string val)
		{
			GoogleId = val;
		}

		public void SetDisplayName(string val)
		{
			DisplayName = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccountLinked)
			{
				num ^= AccountLinked.GetHashCode();
			}
			if (HasGoogleId)
			{
				num ^= GoogleId.GetHashCode();
			}
			if (HasDisplayName)
			{
				num ^= DisplayName.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGoogleAccountLinkStatusResponse getGoogleAccountLinkStatusResponse = obj as GetGoogleAccountLinkStatusResponse;
			if (getGoogleAccountLinkStatusResponse == null)
			{
				return false;
			}
			if (HasAccountLinked != getGoogleAccountLinkStatusResponse.HasAccountLinked || (HasAccountLinked && !AccountLinked.Equals(getGoogleAccountLinkStatusResponse.AccountLinked)))
			{
				return false;
			}
			if (HasGoogleId != getGoogleAccountLinkStatusResponse.HasGoogleId || (HasGoogleId && !GoogleId.Equals(getGoogleAccountLinkStatusResponse.GoogleId)))
			{
				return false;
			}
			if (HasDisplayName != getGoogleAccountLinkStatusResponse.HasDisplayName || (HasDisplayName && !DisplayName.Equals(getGoogleAccountLinkStatusResponse.DisplayName)))
			{
				return false;
			}
			return true;
		}

		public static GetGoogleAccountLinkStatusResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGoogleAccountLinkStatusResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetGoogleAccountLinkStatusResponse Deserialize(Stream stream, GetGoogleAccountLinkStatusResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetGoogleAccountLinkStatusResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGoogleAccountLinkStatusResponse getGoogleAccountLinkStatusResponse = new GetGoogleAccountLinkStatusResponse();
			DeserializeLengthDelimited(stream, getGoogleAccountLinkStatusResponse);
			return getGoogleAccountLinkStatusResponse;
		}

		public static GetGoogleAccountLinkStatusResponse DeserializeLengthDelimited(Stream stream, GetGoogleAccountLinkStatusResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetGoogleAccountLinkStatusResponse Deserialize(Stream stream, GetGoogleAccountLinkStatusResponse instance, long limit)
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
					instance.AccountLinked = ProtocolParser.ReadBool(stream);
					continue;
				case 18:
					instance.GoogleId = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.DisplayName = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, GetGoogleAccountLinkStatusResponse instance)
		{
			if (instance.HasAccountLinked)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.AccountLinked);
			}
			if (instance.HasGoogleId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.GoogleId));
			}
			if (instance.HasDisplayName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DisplayName));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAccountLinked)
			{
				num++;
				num++;
			}
			if (HasGoogleId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(GoogleId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasDisplayName)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(DisplayName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
