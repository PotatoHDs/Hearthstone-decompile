using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.sns.v1
{
	public class GetFacebookBnetFriendsRequest : IProtoBuf
	{
		public static class Types
		{
			public enum ProfilePictureType
			{
				SMALL,
				NORMAL,
				LARGE,
				SQUARE
			}
		}

		public bool HasIdentity;

		private bnet.protocol.account.v1.Identity _Identity;

		public bool HasProfilePictureType;

		private Types.ProfilePictureType _ProfilePictureType;

		public bool HasToken;

		private uint _Token;

		public bool HasAddress;

		private ObjectAddress _Address;

		public bnet.protocol.account.v1.Identity Identity
		{
			get
			{
				return _Identity;
			}
			set
			{
				_Identity = value;
				HasIdentity = value != null;
			}
		}

		public Types.ProfilePictureType ProfilePictureType
		{
			get
			{
				return _ProfilePictureType;
			}
			set
			{
				_ProfilePictureType = value;
				HasProfilePictureType = true;
			}
		}

		public uint Token
		{
			get
			{
				return _Token;
			}
			set
			{
				_Token = value;
				HasToken = true;
			}
		}

		public ObjectAddress Address
		{
			get
			{
				return _Address;
			}
			set
			{
				_Address = value;
				HasAddress = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetIdentity(bnet.protocol.account.v1.Identity val)
		{
			Identity = val;
		}

		public void SetProfilePictureType(Types.ProfilePictureType val)
		{
			ProfilePictureType = val;
		}

		public void SetToken(uint val)
		{
			Token = val;
		}

		public void SetAddress(ObjectAddress val)
		{
			Address = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasIdentity)
			{
				num ^= Identity.GetHashCode();
			}
			if (HasProfilePictureType)
			{
				num ^= ProfilePictureType.GetHashCode();
			}
			if (HasToken)
			{
				num ^= Token.GetHashCode();
			}
			if (HasAddress)
			{
				num ^= Address.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetFacebookBnetFriendsRequest getFacebookBnetFriendsRequest = obj as GetFacebookBnetFriendsRequest;
			if (getFacebookBnetFriendsRequest == null)
			{
				return false;
			}
			if (HasIdentity != getFacebookBnetFriendsRequest.HasIdentity || (HasIdentity && !Identity.Equals(getFacebookBnetFriendsRequest.Identity)))
			{
				return false;
			}
			if (HasProfilePictureType != getFacebookBnetFriendsRequest.HasProfilePictureType || (HasProfilePictureType && !ProfilePictureType.Equals(getFacebookBnetFriendsRequest.ProfilePictureType)))
			{
				return false;
			}
			if (HasToken != getFacebookBnetFriendsRequest.HasToken || (HasToken && !Token.Equals(getFacebookBnetFriendsRequest.Token)))
			{
				return false;
			}
			if (HasAddress != getFacebookBnetFriendsRequest.HasAddress || (HasAddress && !Address.Equals(getFacebookBnetFriendsRequest.Address)))
			{
				return false;
			}
			return true;
		}

		public static GetFacebookBnetFriendsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFacebookBnetFriendsRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetFacebookBnetFriendsRequest Deserialize(Stream stream, GetFacebookBnetFriendsRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetFacebookBnetFriendsRequest DeserializeLengthDelimited(Stream stream)
		{
			GetFacebookBnetFriendsRequest getFacebookBnetFriendsRequest = new GetFacebookBnetFriendsRequest();
			DeserializeLengthDelimited(stream, getFacebookBnetFriendsRequest);
			return getFacebookBnetFriendsRequest;
		}

		public static GetFacebookBnetFriendsRequest DeserializeLengthDelimited(Stream stream, GetFacebookBnetFriendsRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetFacebookBnetFriendsRequest Deserialize(Stream stream, GetFacebookBnetFriendsRequest instance, long limit)
		{
			instance.ProfilePictureType = Types.ProfilePictureType.SMALL;
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
					if (instance.Identity == null)
					{
						instance.Identity = bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream, instance.Identity);
					}
					continue;
				case 16:
					instance.ProfilePictureType = (Types.ProfilePictureType)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.Token = ProtocolParser.ReadUInt32(stream);
					continue;
				case 34:
					if (instance.Address == null)
					{
						instance.Address = ObjectAddress.DeserializeLengthDelimited(stream);
					}
					else
					{
						ObjectAddress.DeserializeLengthDelimited(stream, instance.Address);
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

		public static void Serialize(Stream stream, GetFacebookBnetFriendsRequest instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				bnet.protocol.account.v1.Identity.Serialize(stream, instance.Identity);
			}
			if (instance.HasProfilePictureType)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.ProfilePictureType);
			}
			if (instance.HasToken)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Token);
			}
			if (instance.HasAddress)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Address.GetSerializedSize());
				ObjectAddress.Serialize(stream, instance.Address);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasIdentity)
			{
				num++;
				uint serializedSize = Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasProfilePictureType)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)ProfilePictureType);
			}
			if (HasToken)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Token);
			}
			if (HasAddress)
			{
				num++;
				uint serializedSize2 = Address.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
