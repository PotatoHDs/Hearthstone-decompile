using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.sns.v1
{
	public class FacebookBnetFriend : IProtoBuf
	{
		public bool HasBnetId;

		private AccountId _BnetId;

		public bool HasFbId;

		private string _FbId;

		public bool HasLastName;

		private string _LastName;

		public bool HasFirstName;

		private string _FirstName;

		public bool HasProfilePicture;

		private string _ProfilePicture;

		public bool HasDisplayName;

		private string _DisplayName;

		public AccountId BnetId
		{
			get
			{
				return _BnetId;
			}
			set
			{
				_BnetId = value;
				HasBnetId = value != null;
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

		public string LastName
		{
			get
			{
				return _LastName;
			}
			set
			{
				_LastName = value;
				HasLastName = value != null;
			}
		}

		public string FirstName
		{
			get
			{
				return _FirstName;
			}
			set
			{
				_FirstName = value;
				HasFirstName = value != null;
			}
		}

		public string ProfilePicture
		{
			get
			{
				return _ProfilePicture;
			}
			set
			{
				_ProfilePicture = value;
				HasProfilePicture = value != null;
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

		public void SetBnetId(AccountId val)
		{
			BnetId = val;
		}

		public void SetFbId(string val)
		{
			FbId = val;
		}

		public void SetLastName(string val)
		{
			LastName = val;
		}

		public void SetFirstName(string val)
		{
			FirstName = val;
		}

		public void SetProfilePicture(string val)
		{
			ProfilePicture = val;
		}

		public void SetDisplayName(string val)
		{
			DisplayName = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasBnetId)
			{
				num ^= BnetId.GetHashCode();
			}
			if (HasFbId)
			{
				num ^= FbId.GetHashCode();
			}
			if (HasLastName)
			{
				num ^= LastName.GetHashCode();
			}
			if (HasFirstName)
			{
				num ^= FirstName.GetHashCode();
			}
			if (HasProfilePicture)
			{
				num ^= ProfilePicture.GetHashCode();
			}
			if (HasDisplayName)
			{
				num ^= DisplayName.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FacebookBnetFriend facebookBnetFriend = obj as FacebookBnetFriend;
			if (facebookBnetFriend == null)
			{
				return false;
			}
			if (HasBnetId != facebookBnetFriend.HasBnetId || (HasBnetId && !BnetId.Equals(facebookBnetFriend.BnetId)))
			{
				return false;
			}
			if (HasFbId != facebookBnetFriend.HasFbId || (HasFbId && !FbId.Equals(facebookBnetFriend.FbId)))
			{
				return false;
			}
			if (HasLastName != facebookBnetFriend.HasLastName || (HasLastName && !LastName.Equals(facebookBnetFriend.LastName)))
			{
				return false;
			}
			if (HasFirstName != facebookBnetFriend.HasFirstName || (HasFirstName && !FirstName.Equals(facebookBnetFriend.FirstName)))
			{
				return false;
			}
			if (HasProfilePicture != facebookBnetFriend.HasProfilePicture || (HasProfilePicture && !ProfilePicture.Equals(facebookBnetFriend.ProfilePicture)))
			{
				return false;
			}
			if (HasDisplayName != facebookBnetFriend.HasDisplayName || (HasDisplayName && !DisplayName.Equals(facebookBnetFriend.DisplayName)))
			{
				return false;
			}
			return true;
		}

		public static FacebookBnetFriend ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FacebookBnetFriend>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FacebookBnetFriend Deserialize(Stream stream, FacebookBnetFriend instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FacebookBnetFriend DeserializeLengthDelimited(Stream stream)
		{
			FacebookBnetFriend facebookBnetFriend = new FacebookBnetFriend();
			DeserializeLengthDelimited(stream, facebookBnetFriend);
			return facebookBnetFriend;
		}

		public static FacebookBnetFriend DeserializeLengthDelimited(Stream stream, FacebookBnetFriend instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FacebookBnetFriend Deserialize(Stream stream, FacebookBnetFriend instance, long limit)
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
					if (instance.BnetId == null)
					{
						instance.BnetId = AccountId.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountId.DeserializeLengthDelimited(stream, instance.BnetId);
					}
					continue;
				case 18:
					instance.FbId = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.LastName = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.FirstName = ProtocolParser.ReadString(stream);
					continue;
				case 42:
					instance.ProfilePicture = ProtocolParser.ReadString(stream);
					continue;
				case 50:
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

		public static void Serialize(Stream stream, FacebookBnetFriend instance)
		{
			if (instance.HasBnetId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.BnetId.GetSerializedSize());
				AccountId.Serialize(stream, instance.BnetId);
			}
			if (instance.HasFbId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FbId));
			}
			if (instance.HasLastName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.LastName));
			}
			if (instance.HasFirstName)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FirstName));
			}
			if (instance.HasProfilePicture)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProfilePicture));
			}
			if (instance.HasDisplayName)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DisplayName));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasBnetId)
			{
				num++;
				uint serializedSize = BnetId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasFbId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(FbId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasLastName)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(LastName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasFirstName)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(FirstName);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasProfilePicture)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(ProfilePicture);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasDisplayName)
			{
				num++;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(DisplayName);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			return num;
		}
	}
}
