using System.IO;
using System.Text;

namespace bnet.protocol.friends.v2.client
{
	public class SendInvitationTarget : IProtoBuf
	{
		public bool HasName;

		private string _Name;

		public bool HasAccountId;

		private ulong _AccountId;

		public bool HasEmail;

		private string _Email;

		public bool HasBattleTag;

		private string _BattleTag;

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
				HasName = value != null;
			}
		}

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

		public void SetName(string val)
		{
			Name = val;
		}

		public void SetAccountId(ulong val)
		{
			AccountId = val;
		}

		public void SetEmail(string val)
		{
			Email = val;
		}

		public void SetBattleTag(string val)
		{
			BattleTag = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasName)
			{
				num ^= Name.GetHashCode();
			}
			if (HasAccountId)
			{
				num ^= AccountId.GetHashCode();
			}
			if (HasEmail)
			{
				num ^= Email.GetHashCode();
			}
			if (HasBattleTag)
			{
				num ^= BattleTag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SendInvitationTarget sendInvitationTarget = obj as SendInvitationTarget;
			if (sendInvitationTarget == null)
			{
				return false;
			}
			if (HasName != sendInvitationTarget.HasName || (HasName && !Name.Equals(sendInvitationTarget.Name)))
			{
				return false;
			}
			if (HasAccountId != sendInvitationTarget.HasAccountId || (HasAccountId && !AccountId.Equals(sendInvitationTarget.AccountId)))
			{
				return false;
			}
			if (HasEmail != sendInvitationTarget.HasEmail || (HasEmail && !Email.Equals(sendInvitationTarget.Email)))
			{
				return false;
			}
			if (HasBattleTag != sendInvitationTarget.HasBattleTag || (HasBattleTag && !BattleTag.Equals(sendInvitationTarget.BattleTag)))
			{
				return false;
			}
			return true;
		}

		public static SendInvitationTarget ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendInvitationTarget>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SendInvitationTarget Deserialize(Stream stream, SendInvitationTarget instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SendInvitationTarget DeserializeLengthDelimited(Stream stream)
		{
			SendInvitationTarget sendInvitationTarget = new SendInvitationTarget();
			DeserializeLengthDelimited(stream, sendInvitationTarget);
			return sendInvitationTarget;
		}

		public static SendInvitationTarget DeserializeLengthDelimited(Stream stream, SendInvitationTarget instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SendInvitationTarget Deserialize(Stream stream, SendInvitationTarget instance, long limit)
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
					instance.Name = ProtocolParser.ReadString(stream);
					continue;
				case 80:
					instance.AccountId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 90:
					instance.Email = ProtocolParser.ReadString(stream);
					continue;
				case 98:
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

		public static void Serialize(Stream stream, SendInvitationTarget instance)
		{
			if (instance.HasName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasAccountId)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, instance.AccountId);
			}
			if (instance.HasEmail)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Email));
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(98);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasAccountId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(AccountId);
			}
			if (HasEmail)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Email);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasBattleTag)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}
	}
}
