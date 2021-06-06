using System.IO;
using System.Text;

namespace bnet.protocol.sns.v1
{
	public class GetFacebookAccountLinkStatusResponse : IProtoBuf
	{
		public bool HasAccountLinked;

		private bool _AccountLinked;

		public bool HasFbId;

		private string _FbId;

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

		public string FbId
		{
			get
			{
				return _FbId;
			}
			set
			{
				_FbId = value;
				HasFbId = value != null;
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

		public void SetFbId(string val)
		{
			FbId = val;
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
			if (HasFbId)
			{
				num ^= FbId.GetHashCode();
			}
			if (HasDisplayName)
			{
				num ^= DisplayName.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetFacebookAccountLinkStatusResponse getFacebookAccountLinkStatusResponse = obj as GetFacebookAccountLinkStatusResponse;
			if (getFacebookAccountLinkStatusResponse == null)
			{
				return false;
			}
			if (HasAccountLinked != getFacebookAccountLinkStatusResponse.HasAccountLinked || (HasAccountLinked && !AccountLinked.Equals(getFacebookAccountLinkStatusResponse.AccountLinked)))
			{
				return false;
			}
			if (HasFbId != getFacebookAccountLinkStatusResponse.HasFbId || (HasFbId && !FbId.Equals(getFacebookAccountLinkStatusResponse.FbId)))
			{
				return false;
			}
			if (HasDisplayName != getFacebookAccountLinkStatusResponse.HasDisplayName || (HasDisplayName && !DisplayName.Equals(getFacebookAccountLinkStatusResponse.DisplayName)))
			{
				return false;
			}
			return true;
		}

		public static GetFacebookAccountLinkStatusResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFacebookAccountLinkStatusResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetFacebookAccountLinkStatusResponse Deserialize(Stream stream, GetFacebookAccountLinkStatusResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetFacebookAccountLinkStatusResponse DeserializeLengthDelimited(Stream stream)
		{
			GetFacebookAccountLinkStatusResponse getFacebookAccountLinkStatusResponse = new GetFacebookAccountLinkStatusResponse();
			DeserializeLengthDelimited(stream, getFacebookAccountLinkStatusResponse);
			return getFacebookAccountLinkStatusResponse;
		}

		public static GetFacebookAccountLinkStatusResponse DeserializeLengthDelimited(Stream stream, GetFacebookAccountLinkStatusResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetFacebookAccountLinkStatusResponse Deserialize(Stream stream, GetFacebookAccountLinkStatusResponse instance, long limit)
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
					instance.FbId = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, GetFacebookAccountLinkStatusResponse instance)
		{
			if (instance.HasAccountLinked)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.AccountLinked);
			}
			if (instance.HasFbId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FbId));
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
			if (HasFbId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(FbId);
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
