using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.voice.v2.server
{
	public class MuteAccountInChannelRequest : IProtoBuf
	{
		public bool HasChannelUri;

		private string _ChannelUri;

		public bool HasAccountId;

		private AccountId _AccountId;

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

		public bool IsInitialized => true;

		public void SetChannelUri(string val)
		{
			ChannelUri = val;
		}

		public void SetAccountId(AccountId val)
		{
			AccountId = val;
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
			return num;
		}

		public override bool Equals(object obj)
		{
			MuteAccountInChannelRequest muteAccountInChannelRequest = obj as MuteAccountInChannelRequest;
			if (muteAccountInChannelRequest == null)
			{
				return false;
			}
			if (HasChannelUri != muteAccountInChannelRequest.HasChannelUri || (HasChannelUri && !ChannelUri.Equals(muteAccountInChannelRequest.ChannelUri)))
			{
				return false;
			}
			if (HasAccountId != muteAccountInChannelRequest.HasAccountId || (HasAccountId && !AccountId.Equals(muteAccountInChannelRequest.AccountId)))
			{
				return false;
			}
			return true;
		}

		public static MuteAccountInChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MuteAccountInChannelRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static MuteAccountInChannelRequest Deserialize(Stream stream, MuteAccountInChannelRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static MuteAccountInChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			MuteAccountInChannelRequest muteAccountInChannelRequest = new MuteAccountInChannelRequest();
			DeserializeLengthDelimited(stream, muteAccountInChannelRequest);
			return muteAccountInChannelRequest;
		}

		public static MuteAccountInChannelRequest DeserializeLengthDelimited(Stream stream, MuteAccountInChannelRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static MuteAccountInChannelRequest Deserialize(Stream stream, MuteAccountInChannelRequest instance, long limit)
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

		public static void Serialize(Stream stream, MuteAccountInChannelRequest instance)
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
			return num;
		}
	}
}
