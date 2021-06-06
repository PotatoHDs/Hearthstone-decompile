using System.IO;
using System.Text;
using bnet.protocol.friends.v2.client.Types;

namespace bnet.protocol.friends.v2.client
{
	public class FriendOfFriend : IProtoBuf
	{
		public bool HasId;

		private ulong _Id;

		public bool HasLevel;

		private FriendLevel _Level;

		public bool HasFullName;

		private string _FullName;

		public bool HasBattleTag;

		private string _BattleTag;

		public ulong Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				HasId = true;
			}
		}

		public FriendLevel Level
		{
			get
			{
				return _Level;
			}
			set
			{
				_Level = value;
				HasLevel = true;
			}
		}

		public string FullName
		{
			get
			{
				return _FullName;
			}
			set
			{
				_FullName = value;
				HasFullName = value != null;
			}
		}

		public string BattleTag
		{
			get
			{
				return _BattleTag;
			}
			set
			{
				_BattleTag = value;
				HasBattleTag = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetId(ulong val)
		{
			Id = val;
		}

		public void SetLevel(FriendLevel val)
		{
			Level = val;
		}

		public void SetFullName(string val)
		{
			FullName = val;
		}

		public void SetBattleTag(string val)
		{
			BattleTag = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			if (HasLevel)
			{
				num ^= Level.GetHashCode();
			}
			if (HasFullName)
			{
				num ^= FullName.GetHashCode();
			}
			if (HasBattleTag)
			{
				num ^= BattleTag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FriendOfFriend friendOfFriend = obj as FriendOfFriend;
			if (friendOfFriend == null)
			{
				return false;
			}
			if (HasId != friendOfFriend.HasId || (HasId && !Id.Equals(friendOfFriend.Id)))
			{
				return false;
			}
			if (HasLevel != friendOfFriend.HasLevel || (HasLevel && !Level.Equals(friendOfFriend.Level)))
			{
				return false;
			}
			if (HasFullName != friendOfFriend.HasFullName || (HasFullName && !FullName.Equals(friendOfFriend.FullName)))
			{
				return false;
			}
			if (HasBattleTag != friendOfFriend.HasBattleTag || (HasBattleTag && !BattleTag.Equals(friendOfFriend.BattleTag)))
			{
				return false;
			}
			return true;
		}

		public static FriendOfFriend ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendOfFriend>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FriendOfFriend Deserialize(Stream stream, FriendOfFriend instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FriendOfFriend DeserializeLengthDelimited(Stream stream)
		{
			FriendOfFriend friendOfFriend = new FriendOfFriend();
			DeserializeLengthDelimited(stream, friendOfFriend);
			return friendOfFriend;
		}

		public static FriendOfFriend DeserializeLengthDelimited(Stream stream, FriendOfFriend instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FriendOfFriend Deserialize(Stream stream, FriendOfFriend instance, long limit)
		{
			instance.Level = FriendLevel.FRIEND_LEVEL_BATTLE_TAG;
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
					instance.Id = ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Level = (FriendLevel)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.FullName = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.BattleTag = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, FriendOfFriend instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.Id);
			}
			if (instance.HasLevel)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Level);
			}
			if (instance.HasFullName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FullName));
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Id);
			}
			if (HasLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Level);
			}
			if (HasFullName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(FullName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasBattleTag)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
