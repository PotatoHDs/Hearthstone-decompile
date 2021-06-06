using System.IO;
using System.Text;
using bnet.protocol.account.v1;
using bnet.protocol.Types;

namespace bnet.protocol.voice.v2.server
{
	public class CreateChannelJoinTokenRequest : IProtoBuf
	{
		public bool HasChannelUri;

		private string _ChannelUri;

		public bool HasAccountId;

		private AccountId _AccountId;

		public bool HasRequestedJoinType;

		private VoiceJoinType _RequestedJoinType;

		public string ChannelUri
		{
			get
			{
				return _ChannelUri;
			}
			set
			{
				_ChannelUri = value;
				HasChannelUri = value != null;
			}
		}

		public AccountId AccountId
		{
			get
			{
				return _AccountId;
			}
			set
			{
				_AccountId = value;
				HasAccountId = value != null;
			}
		}

		public VoiceJoinType RequestedJoinType
		{
			get
			{
				return _RequestedJoinType;
			}
			set
			{
				_RequestedJoinType = value;
				HasRequestedJoinType = true;
			}
		}

		public bool IsInitialized => true;

		public void SetChannelUri(string val)
		{
			ChannelUri = val;
		}

		public void SetAccountId(AccountId val)
		{
			AccountId = val;
		}

		public void SetRequestedJoinType(VoiceJoinType val)
		{
			RequestedJoinType = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasChannelUri)
			{
				num ^= ChannelUri.GetHashCode();
			}
			if (HasAccountId)
			{
				num ^= AccountId.GetHashCode();
			}
			if (HasRequestedJoinType)
			{
				num ^= RequestedJoinType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateChannelJoinTokenRequest createChannelJoinTokenRequest = obj as CreateChannelJoinTokenRequest;
			if (createChannelJoinTokenRequest == null)
			{
				return false;
			}
			if (HasChannelUri != createChannelJoinTokenRequest.HasChannelUri || (HasChannelUri && !ChannelUri.Equals(createChannelJoinTokenRequest.ChannelUri)))
			{
				return false;
			}
			if (HasAccountId != createChannelJoinTokenRequest.HasAccountId || (HasAccountId && !AccountId.Equals(createChannelJoinTokenRequest.AccountId)))
			{
				return false;
			}
			if (HasRequestedJoinType != createChannelJoinTokenRequest.HasRequestedJoinType || (HasRequestedJoinType && !RequestedJoinType.Equals(createChannelJoinTokenRequest.RequestedJoinType)))
			{
				return false;
			}
			return true;
		}

		public static CreateChannelJoinTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelJoinTokenRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CreateChannelJoinTokenRequest Deserialize(Stream stream, CreateChannelJoinTokenRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CreateChannelJoinTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelJoinTokenRequest createChannelJoinTokenRequest = new CreateChannelJoinTokenRequest();
			DeserializeLengthDelimited(stream, createChannelJoinTokenRequest);
			return createChannelJoinTokenRequest;
		}

		public static CreateChannelJoinTokenRequest DeserializeLengthDelimited(Stream stream, CreateChannelJoinTokenRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CreateChannelJoinTokenRequest Deserialize(Stream stream, CreateChannelJoinTokenRequest instance, long limit)
		{
			instance.RequestedJoinType = VoiceJoinType.VOICE_JOIN_NORMAL;
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
					instance.ChannelUri = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					if (instance.AccountId == null)
					{
						instance.AccountId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.AccountId);
					}
					continue;
				case 24:
					instance.RequestedJoinType = (VoiceJoinType)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, CreateChannelJoinTokenRequest instance)
		{
			if (instance.HasChannelUri)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelUri));
			}
			if (instance.HasAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				AccountId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasRequestedJoinType)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.RequestedJoinType);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasChannelUri)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(ChannelUri);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasAccountId)
			{
				num++;
				uint serializedSize = AccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasRequestedJoinType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)RequestedJoinType);
			}
			return num;
		}
	}
}
