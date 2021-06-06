using System.IO;

namespace PegasusShared
{
	public class PlayerIdentity : IProtoBuf
	{
		public bool HasGameAccount;

		private BnetId _GameAccount;

		public bool HasAccount;

		private BnetId _Account;

		public long PlayerId { get; set; }

		public BnetId GameAccount
		{
			get
			{
				return _GameAccount;
			}
			set
			{
				_GameAccount = value;
				HasGameAccount = value != null;
			}
		}

		public BnetId Account
		{
			get
			{
				return _Account;
			}
			set
			{
				_Account = value;
				HasAccount = value != null;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= PlayerId.GetHashCode();
			if (HasGameAccount)
			{
				hashCode ^= GameAccount.GetHashCode();
			}
			if (HasAccount)
			{
				hashCode ^= Account.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PlayerIdentity playerIdentity = obj as PlayerIdentity;
			if (playerIdentity == null)
			{
				return false;
			}
			if (!PlayerId.Equals(playerIdentity.PlayerId))
			{
				return false;
			}
			if (HasGameAccount != playerIdentity.HasGameAccount || (HasGameAccount && !GameAccount.Equals(playerIdentity.GameAccount)))
			{
				return false;
			}
			if (HasAccount != playerIdentity.HasAccount || (HasAccount && !Account.Equals(playerIdentity.Account)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PlayerIdentity Deserialize(Stream stream, PlayerIdentity instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerIdentity DeserializeLengthDelimited(Stream stream)
		{
			PlayerIdentity playerIdentity = new PlayerIdentity();
			DeserializeLengthDelimited(stream, playerIdentity);
			return playerIdentity;
		}

		public static PlayerIdentity DeserializeLengthDelimited(Stream stream, PlayerIdentity instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerIdentity Deserialize(Stream stream, PlayerIdentity instance, long limit)
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
					instance.PlayerId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.GameAccount == null)
					{
						instance.GameAccount = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.GameAccount);
					}
					continue;
				case 26:
					if (instance.Account == null)
					{
						instance.Account = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.Account);
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

		public static void Serialize(Stream stream, PlayerIdentity instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			if (instance.HasGameAccount)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				BnetId.Serialize(stream, instance.GameAccount);
			}
			if (instance.HasAccount)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Account.GetSerializedSize());
				BnetId.Serialize(stream, instance.Account);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)PlayerId);
			if (HasGameAccount)
			{
				num++;
				uint serializedSize = GameAccount.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasAccount)
			{
				num++;
				uint serializedSize2 = Account.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1;
		}
	}
}
