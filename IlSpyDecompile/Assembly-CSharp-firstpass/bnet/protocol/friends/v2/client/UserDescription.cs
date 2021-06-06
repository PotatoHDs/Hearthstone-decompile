using System.IO;
using System.Text;

namespace bnet.protocol.friends.v2.client
{
	public class UserDescription : IProtoBuf
	{
		public bool HasAccountId;

		private ulong _AccountId;

		public bool HasBattleTag;

		private string _BattleTag;

		public bool HasFullName;

		private string _FullName;

		public ulong AccountId
		{
			get
			{
				return _AccountId;
			}
			set
			{
				_AccountId = value;
				HasAccountId = true;
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

		public bool IsInitialized => true;

		public void SetAccountId(ulong val)
		{
			AccountId = val;
		}

		public void SetBattleTag(string val)
		{
			BattleTag = val;
		}

		public void SetFullName(string val)
		{
			FullName = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccountId)
			{
				num ^= AccountId.GetHashCode();
			}
			if (HasBattleTag)
			{
				num ^= BattleTag.GetHashCode();
			}
			if (HasFullName)
			{
				num ^= FullName.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UserDescription userDescription = obj as UserDescription;
			if (userDescription == null)
			{
				return false;
			}
			if (HasAccountId != userDescription.HasAccountId || (HasAccountId && !AccountId.Equals(userDescription.AccountId)))
			{
				return false;
			}
			if (HasBattleTag != userDescription.HasBattleTag || (HasBattleTag && !BattleTag.Equals(userDescription.BattleTag)))
			{
				return false;
			}
			if (HasFullName != userDescription.HasFullName || (HasFullName && !FullName.Equals(userDescription.FullName)))
			{
				return false;
			}
			return true;
		}

		public static UserDescription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UserDescription>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UserDescription Deserialize(Stream stream, UserDescription instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UserDescription DeserializeLengthDelimited(Stream stream)
		{
			UserDescription userDescription = new UserDescription();
			DeserializeLengthDelimited(stream, userDescription);
			return userDescription;
		}

		public static UserDescription DeserializeLengthDelimited(Stream stream, UserDescription instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UserDescription Deserialize(Stream stream, UserDescription instance, long limit)
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
					instance.AccountId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.BattleTag = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.FullName = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, UserDescription instance)
		{
			if (instance.HasAccountId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.AccountId);
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasFullName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FullName));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAccountId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(AccountId);
			}
			if (HasBattleTag)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasFullName)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(FullName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
