using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	public class AccountReference : IProtoBuf
	{
		public bool HasId;

		private uint _Id;

		public bool HasEmail;

		private string _Email;

		public bool HasHandle;

		private GameAccountHandle _Handle;

		public bool HasBattleTag;

		private string _BattleTag;

		public bool HasRegion;

		private uint _Region;

		public uint Id
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

		public string Email
		{
			get
			{
				return _Email;
			}
			set
			{
				_Email = value;
				HasEmail = value != null;
			}
		}

		public GameAccountHandle Handle
		{
			get
			{
				return _Handle;
			}
			set
			{
				_Handle = value;
				HasHandle = value != null;
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

		public uint Region
		{
			get
			{
				return _Region;
			}
			set
			{
				_Region = value;
				HasRegion = true;
			}
		}

		public bool IsInitialized => true;

		public void SetId(uint val)
		{
			Id = val;
		}

		public void SetEmail(string val)
		{
			Email = val;
		}

		public void SetHandle(GameAccountHandle val)
		{
			Handle = val;
		}

		public void SetBattleTag(string val)
		{
			BattleTag = val;
		}

		public void SetRegion(uint val)
		{
			Region = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasId)
			{
				num ^= Id.GetHashCode();
			}
			if (HasEmail)
			{
				num ^= Email.GetHashCode();
			}
			if (HasHandle)
			{
				num ^= Handle.GetHashCode();
			}
			if (HasBattleTag)
			{
				num ^= BattleTag.GetHashCode();
			}
			if (HasRegion)
			{
				num ^= Region.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountReference accountReference = obj as AccountReference;
			if (accountReference == null)
			{
				return false;
			}
			if (HasId != accountReference.HasId || (HasId && !Id.Equals(accountReference.Id)))
			{
				return false;
			}
			if (HasEmail != accountReference.HasEmail || (HasEmail && !Email.Equals(accountReference.Email)))
			{
				return false;
			}
			if (HasHandle != accountReference.HasHandle || (HasHandle && !Handle.Equals(accountReference.Handle)))
			{
				return false;
			}
			if (HasBattleTag != accountReference.HasBattleTag || (HasBattleTag && !BattleTag.Equals(accountReference.BattleTag)))
			{
				return false;
			}
			if (HasRegion != accountReference.HasRegion || (HasRegion && !Region.Equals(accountReference.Region)))
			{
				return false;
			}
			return true;
		}

		public static AccountReference ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountReference>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AccountReference Deserialize(Stream stream, AccountReference instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AccountReference DeserializeLengthDelimited(Stream stream)
		{
			AccountReference accountReference = new AccountReference();
			DeserializeLengthDelimited(stream, accountReference);
			return accountReference;
		}

		public static AccountReference DeserializeLengthDelimited(Stream stream, AccountReference instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AccountReference Deserialize(Stream stream, AccountReference instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Region = 0u;
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
				case 13:
					instance.Id = binaryReader.ReadUInt32();
					continue;
				case 18:
					instance.Email = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					if (instance.Handle == null)
					{
						instance.Handle = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.Handle);
					}
					continue;
				case 34:
					instance.BattleTag = ProtocolParser.ReadString(stream);
					continue;
				case 80:
					instance.Region = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, AccountReference instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.Id);
			}
			if (instance.HasEmail)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Email));
			}
			if (instance.HasHandle)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Handle.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Handle);
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasRegion)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt32(stream, instance.Region);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasId)
			{
				num++;
				num += 4;
			}
			if (HasEmail)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Email);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasHandle)
			{
				num++;
				uint serializedSize = Handle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasBattleTag)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasRegion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Region);
			}
			return num;
		}
	}
}
