using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	public class GetLoginTokenRequest : IProtoBuf
	{
		public bool HasMemberId;

		private GameAccountHandle _MemberId;

		public GameAccountHandle MemberId
		{
			get
			{
				return _MemberId;
			}
			set
			{
				_MemberId = value;
				HasMemberId = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetMemberId(GameAccountHandle val)
		{
			MemberId = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMemberId)
			{
				num ^= MemberId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetLoginTokenRequest getLoginTokenRequest = obj as GetLoginTokenRequest;
			if (getLoginTokenRequest == null)
			{
				return false;
			}
			if (HasMemberId != getLoginTokenRequest.HasMemberId || (HasMemberId && !MemberId.Equals(getLoginTokenRequest.MemberId)))
			{
				return false;
			}
			return true;
		}

		public static GetLoginTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetLoginTokenRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetLoginTokenRequest Deserialize(Stream stream, GetLoginTokenRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetLoginTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			GetLoginTokenRequest getLoginTokenRequest = new GetLoginTokenRequest();
			DeserializeLengthDelimited(stream, getLoginTokenRequest);
			return getLoginTokenRequest;
		}

		public static GetLoginTokenRequest DeserializeLengthDelimited(Stream stream, GetLoginTokenRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetLoginTokenRequest Deserialize(Stream stream, GetLoginTokenRequest instance, long limit)
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
				case 26:
					if (instance.MemberId == null)
					{
						instance.MemberId = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.MemberId);
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

		public static void Serialize(Stream stream, GetLoginTokenRequest instance)
		{
			if (instance.HasMemberId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.MemberId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMemberId)
			{
				num++;
				uint serializedSize = MemberId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
