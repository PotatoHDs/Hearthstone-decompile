using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.sns.v1
{
	public class FacebookBnetFriendListNotification : IProtoBuf
	{
		public bool HasErrorCode;

		private uint _ErrorCode;

		public bool HasIdentity;

		private bnet.protocol.account.v1.Identity _Identity;

		private List<FacebookBnetFriend> _FriendList = new List<FacebookBnetFriend>();

		public bool HasListEnd;

		private bool _ListEnd;

		public bool HasToken;

		private uint _Token;

		public bool HasFbId;

		private string _FbId;

		public bool HasAddress;

		private ObjectAddress _Address;

		public uint ErrorCode
		{
			get
			{
				return _ErrorCode;
			}
			set
			{
				_ErrorCode = value;
				HasErrorCode = true;
			}
		}

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

		public List<FacebookBnetFriend> FriendList
		{
			get
			{
				return _FriendList;
			}
			set
			{
				_FriendList = value;
			}
		}

		public List<FacebookBnetFriend> FriendListList => _FriendList;

		public int FriendListCount => _FriendList.Count;

		public bool ListEnd
		{
			get
			{
				return _ListEnd;
			}
			set
			{
				_ListEnd = value;
				HasListEnd = true;
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

		public void SetErrorCode(uint val)
		{
			ErrorCode = val;
		}

		public void SetIdentity(bnet.protocol.account.v1.Identity val)
		{
			Identity = val;
		}

		public void AddFriendList(FacebookBnetFriend val)
		{
			_FriendList.Add(val);
		}

		public void ClearFriendList()
		{
			_FriendList.Clear();
		}

		public void SetFriendList(List<FacebookBnetFriend> val)
		{
			FriendList = val;
		}

		public void SetListEnd(bool val)
		{
			ListEnd = val;
		}

		public void SetToken(uint val)
		{
			Token = val;
		}

		public void SetFbId(string val)
		{
			FbId = val;
		}

		public void SetAddress(ObjectAddress val)
		{
			Address = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasErrorCode)
			{
				num ^= ErrorCode.GetHashCode();
			}
			if (HasIdentity)
			{
				num ^= Identity.GetHashCode();
			}
			foreach (FacebookBnetFriend friend in FriendList)
			{
				num ^= friend.GetHashCode();
			}
			if (HasListEnd)
			{
				num ^= ListEnd.GetHashCode();
			}
			if (HasToken)
			{
				num ^= Token.GetHashCode();
			}
			if (HasFbId)
			{
				num ^= FbId.GetHashCode();
			}
			if (HasAddress)
			{
				num ^= Address.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FacebookBnetFriendListNotification facebookBnetFriendListNotification = obj as FacebookBnetFriendListNotification;
			if (facebookBnetFriendListNotification == null)
			{
				return false;
			}
			if (HasErrorCode != facebookBnetFriendListNotification.HasErrorCode || (HasErrorCode && !ErrorCode.Equals(facebookBnetFriendListNotification.ErrorCode)))
			{
				return false;
			}
			if (HasIdentity != facebookBnetFriendListNotification.HasIdentity || (HasIdentity && !Identity.Equals(facebookBnetFriendListNotification.Identity)))
			{
				return false;
			}
			if (FriendList.Count != facebookBnetFriendListNotification.FriendList.Count)
			{
				return false;
			}
			for (int i = 0; i < FriendList.Count; i++)
			{
				if (!FriendList[i].Equals(facebookBnetFriendListNotification.FriendList[i]))
				{
					return false;
				}
			}
			if (HasListEnd != facebookBnetFriendListNotification.HasListEnd || (HasListEnd && !ListEnd.Equals(facebookBnetFriendListNotification.ListEnd)))
			{
				return false;
			}
			if (HasToken != facebookBnetFriendListNotification.HasToken || (HasToken && !Token.Equals(facebookBnetFriendListNotification.Token)))
			{
				return false;
			}
			if (HasFbId != facebookBnetFriendListNotification.HasFbId || (HasFbId && !FbId.Equals(facebookBnetFriendListNotification.FbId)))
			{
				return false;
			}
			if (HasAddress != facebookBnetFriendListNotification.HasAddress || (HasAddress && !Address.Equals(facebookBnetFriendListNotification.Address)))
			{
				return false;
			}
			return true;
		}

		public static FacebookBnetFriendListNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FacebookBnetFriendListNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FacebookBnetFriendListNotification Deserialize(Stream stream, FacebookBnetFriendListNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FacebookBnetFriendListNotification DeserializeLengthDelimited(Stream stream)
		{
			FacebookBnetFriendListNotification facebookBnetFriendListNotification = new FacebookBnetFriendListNotification();
			DeserializeLengthDelimited(stream, facebookBnetFriendListNotification);
			return facebookBnetFriendListNotification;
		}

		public static FacebookBnetFriendListNotification DeserializeLengthDelimited(Stream stream, FacebookBnetFriendListNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FacebookBnetFriendListNotification Deserialize(Stream stream, FacebookBnetFriendListNotification instance, long limit)
		{
			if (instance.FriendList == null)
			{
				instance.FriendList = new List<FacebookBnetFriend>();
			}
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
					instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
					continue;
				case 18:
					if (instance.Identity == null)
					{
						instance.Identity = bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream, instance.Identity);
					}
					continue;
				case 26:
					instance.FriendList.Add(FacebookBnetFriend.DeserializeLengthDelimited(stream));
					continue;
				case 32:
					instance.ListEnd = ProtocolParser.ReadBool(stream);
					continue;
				case 40:
					instance.Token = ProtocolParser.ReadUInt32(stream);
					continue;
				case 50:
					instance.FbId = ProtocolParser.ReadString(stream);
					continue;
				case 58:
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

		public static void Serialize(Stream stream, FacebookBnetFriendListNotification instance)
		{
			if (instance.HasErrorCode)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
			}
			if (instance.HasIdentity)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				bnet.protocol.account.v1.Identity.Serialize(stream, instance.Identity);
			}
			if (instance.FriendList.Count > 0)
			{
				foreach (FacebookBnetFriend friend in instance.FriendList)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, friend.GetSerializedSize());
					FacebookBnetFriend.Serialize(stream, friend);
				}
			}
			if (instance.HasListEnd)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.ListEnd);
			}
			if (instance.HasToken)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.Token);
			}
			if (instance.HasFbId)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FbId));
			}
			if (instance.HasAddress)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.Address.GetSerializedSize());
				ObjectAddress.Serialize(stream, instance.Address);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasErrorCode)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ErrorCode);
			}
			if (HasIdentity)
			{
				num++;
				uint serializedSize = Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (FriendList.Count > 0)
			{
				foreach (FacebookBnetFriend friend in FriendList)
				{
					num++;
					uint serializedSize2 = friend.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasListEnd)
			{
				num++;
				num++;
			}
			if (HasToken)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Token);
			}
			if (HasFbId)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(FbId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasAddress)
			{
				num++;
				uint serializedSize3 = Address.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
