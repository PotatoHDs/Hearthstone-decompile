using System.IO;

namespace bnet.protocol.account.v1
{
	public class PrivacyInfo : IProtoBuf
	{
		public static class Types
		{
			public enum GameInfoPrivacy
			{
				PRIVACY_ME,
				PRIVACY_FRIENDS,
				PRIVACY_EVERYONE
			}
		}

		public bool HasIsUsingRid;

		private bool _IsUsingRid;

		public bool HasIsVisibleForViewFriends;

		private bool _IsVisibleForViewFriends;

		public bool HasIsHiddenFromFriendFinder;

		private bool _IsHiddenFromFriendFinder;

		public bool HasGameInfoPrivacy;

		private Types.GameInfoPrivacy _GameInfoPrivacy;

		public bool HasOnlyAllowFriendWhispers;

		private bool _OnlyAllowFriendWhispers;

		public bool IsUsingRid
		{
			get
			{
				return _IsUsingRid;
			}
			set
			{
				_IsUsingRid = value;
				HasIsUsingRid = true;
			}
		}

		public bool IsVisibleForViewFriends
		{
			get
			{
				return _IsVisibleForViewFriends;
			}
			set
			{
				_IsVisibleForViewFriends = value;
				HasIsVisibleForViewFriends = true;
			}
		}

		public bool IsHiddenFromFriendFinder
		{
			get
			{
				return _IsHiddenFromFriendFinder;
			}
			set
			{
				_IsHiddenFromFriendFinder = value;
				HasIsHiddenFromFriendFinder = true;
			}
		}

		public Types.GameInfoPrivacy GameInfoPrivacy
		{
			get
			{
				return _GameInfoPrivacy;
			}
			set
			{
				_GameInfoPrivacy = value;
				HasGameInfoPrivacy = true;
			}
		}

		public bool OnlyAllowFriendWhispers
		{
			get
			{
				return _OnlyAllowFriendWhispers;
			}
			set
			{
				_OnlyAllowFriendWhispers = value;
				HasOnlyAllowFriendWhispers = true;
			}
		}

		public bool IsInitialized => true;

		public void SetIsUsingRid(bool val)
		{
			IsUsingRid = val;
		}

		public void SetIsVisibleForViewFriends(bool val)
		{
			IsVisibleForViewFriends = val;
		}

		public void SetIsHiddenFromFriendFinder(bool val)
		{
			IsHiddenFromFriendFinder = val;
		}

		public void SetGameInfoPrivacy(Types.GameInfoPrivacy val)
		{
			GameInfoPrivacy = val;
		}

		public void SetOnlyAllowFriendWhispers(bool val)
		{
			OnlyAllowFriendWhispers = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasIsUsingRid)
			{
				num ^= IsUsingRid.GetHashCode();
			}
			if (HasIsVisibleForViewFriends)
			{
				num ^= IsVisibleForViewFriends.GetHashCode();
			}
			if (HasIsHiddenFromFriendFinder)
			{
				num ^= IsHiddenFromFriendFinder.GetHashCode();
			}
			if (HasGameInfoPrivacy)
			{
				num ^= GameInfoPrivacy.GetHashCode();
			}
			if (HasOnlyAllowFriendWhispers)
			{
				num ^= OnlyAllowFriendWhispers.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PrivacyInfo privacyInfo = obj as PrivacyInfo;
			if (privacyInfo == null)
			{
				return false;
			}
			if (HasIsUsingRid != privacyInfo.HasIsUsingRid || (HasIsUsingRid && !IsUsingRid.Equals(privacyInfo.IsUsingRid)))
			{
				return false;
			}
			if (HasIsVisibleForViewFriends != privacyInfo.HasIsVisibleForViewFriends || (HasIsVisibleForViewFriends && !IsVisibleForViewFriends.Equals(privacyInfo.IsVisibleForViewFriends)))
			{
				return false;
			}
			if (HasIsHiddenFromFriendFinder != privacyInfo.HasIsHiddenFromFriendFinder || (HasIsHiddenFromFriendFinder && !IsHiddenFromFriendFinder.Equals(privacyInfo.IsHiddenFromFriendFinder)))
			{
				return false;
			}
			if (HasGameInfoPrivacy != privacyInfo.HasGameInfoPrivacy || (HasGameInfoPrivacy && !GameInfoPrivacy.Equals(privacyInfo.GameInfoPrivacy)))
			{
				return false;
			}
			if (HasOnlyAllowFriendWhispers != privacyInfo.HasOnlyAllowFriendWhispers || (HasOnlyAllowFriendWhispers && !OnlyAllowFriendWhispers.Equals(privacyInfo.OnlyAllowFriendWhispers)))
			{
				return false;
			}
			return true;
		}

		public static PrivacyInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PrivacyInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PrivacyInfo Deserialize(Stream stream, PrivacyInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PrivacyInfo DeserializeLengthDelimited(Stream stream)
		{
			PrivacyInfo privacyInfo = new PrivacyInfo();
			DeserializeLengthDelimited(stream, privacyInfo);
			return privacyInfo;
		}

		public static PrivacyInfo DeserializeLengthDelimited(Stream stream, PrivacyInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PrivacyInfo Deserialize(Stream stream, PrivacyInfo instance, long limit)
		{
			instance.GameInfoPrivacy = Types.GameInfoPrivacy.PRIVACY_FRIENDS;
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
				case 24:
					instance.IsUsingRid = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.IsVisibleForViewFriends = ProtocolParser.ReadBool(stream);
					continue;
				case 40:
					instance.IsHiddenFromFriendFinder = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.GameInfoPrivacy = (Types.GameInfoPrivacy)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.OnlyAllowFriendWhispers = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, PrivacyInfo instance)
		{
			if (instance.HasIsUsingRid)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsUsingRid);
			}
			if (instance.HasIsVisibleForViewFriends)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.IsVisibleForViewFriends);
			}
			if (instance.HasIsHiddenFromFriendFinder)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.IsHiddenFromFriendFinder);
			}
			if (instance.HasGameInfoPrivacy)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.GameInfoPrivacy);
			}
			if (instance.HasOnlyAllowFriendWhispers)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.OnlyAllowFriendWhispers);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasIsUsingRid)
			{
				num++;
				num++;
			}
			if (HasIsVisibleForViewFriends)
			{
				num++;
				num++;
			}
			if (HasIsHiddenFromFriendFinder)
			{
				num++;
				num++;
			}
			if (HasGameInfoPrivacy)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)GameInfoPrivacy);
			}
			if (HasOnlyAllowFriendWhispers)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
