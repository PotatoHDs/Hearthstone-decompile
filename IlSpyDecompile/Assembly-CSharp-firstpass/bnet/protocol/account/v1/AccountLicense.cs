using System.IO;

namespace bnet.protocol.account.v1
{
	public class AccountLicense : IProtoBuf
	{
		public bool HasExpires;

		private ulong _Expires;

		public uint Id { get; set; }

		public ulong Expires
		{
			get
			{
				return _Expires;
			}
			set
			{
				_Expires = value;
				HasExpires = true;
			}
		}

		public bool IsInitialized => true;

		public void SetId(uint val)
		{
			Id = val;
		}

		public void SetExpires(ulong val)
		{
			Expires = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Id.GetHashCode();
			if (HasExpires)
			{
				hashCode ^= Expires.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			AccountLicense accountLicense = obj as AccountLicense;
			if (accountLicense == null)
			{
				return false;
			}
			if (!Id.Equals(accountLicense.Id))
			{
				return false;
			}
			if (HasExpires != accountLicense.HasExpires || (HasExpires && !Expires.Equals(accountLicense.Expires)))
			{
				return false;
			}
			return true;
		}

		public static AccountLicense ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountLicense>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AccountLicense Deserialize(Stream stream, AccountLicense instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AccountLicense DeserializeLengthDelimited(Stream stream)
		{
			AccountLicense accountLicense = new AccountLicense();
			DeserializeLengthDelimited(stream, accountLicense);
			return accountLicense;
		}

		public static AccountLicense DeserializeLengthDelimited(Stream stream, AccountLicense instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AccountLicense Deserialize(Stream stream, AccountLicense instance, long limit)
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
					instance.Id = ProtocolParser.ReadUInt32(stream);
					continue;
				case 16:
					instance.Expires = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, AccountLicense instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Id);
			if (instance.HasExpires)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Expires);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(Id);
			if (HasExpires)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Expires);
			}
			return num + 1;
		}
	}
}
