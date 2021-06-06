using System.IO;

namespace bnet.protocol.account.v1
{
	public class AccountFieldOptions : IProtoBuf
	{
		public bool HasAllFields;

		private bool _AllFields;

		public bool HasFieldAccountLevelInfo;

		private bool _FieldAccountLevelInfo;

		public bool HasFieldPrivacyInfo;

		private bool _FieldPrivacyInfo;

		public bool HasFieldParentalControlInfo;

		private bool _FieldParentalControlInfo;

		public bool HasFieldGameLevelInfo;

		private bool _FieldGameLevelInfo;

		public bool HasFieldGameStatus;

		private bool _FieldGameStatus;

		public bool HasFieldGameAccounts;

		private bool _FieldGameAccounts;

		public bool AllFields
		{
			get
			{
				return _AllFields;
			}
			set
			{
				_AllFields = value;
				HasAllFields = true;
			}
		}

		public bool FieldAccountLevelInfo
		{
			get
			{
				return _FieldAccountLevelInfo;
			}
			set
			{
				_FieldAccountLevelInfo = value;
				HasFieldAccountLevelInfo = true;
			}
		}

		public bool FieldPrivacyInfo
		{
			get
			{
				return _FieldPrivacyInfo;
			}
			set
			{
				_FieldPrivacyInfo = value;
				HasFieldPrivacyInfo = true;
			}
		}

		public bool FieldParentalControlInfo
		{
			get
			{
				return _FieldParentalControlInfo;
			}
			set
			{
				_FieldParentalControlInfo = value;
				HasFieldParentalControlInfo = true;
			}
		}

		public bool FieldGameLevelInfo
		{
			get
			{
				return _FieldGameLevelInfo;
			}
			set
			{
				_FieldGameLevelInfo = value;
				HasFieldGameLevelInfo = true;
			}
		}

		public bool FieldGameStatus
		{
			get
			{
				return _FieldGameStatus;
			}
			set
			{
				_FieldGameStatus = value;
				HasFieldGameStatus = true;
			}
		}

		public bool FieldGameAccounts
		{
			get
			{
				return _FieldGameAccounts;
			}
			set
			{
				_FieldGameAccounts = value;
				HasFieldGameAccounts = true;
			}
		}

		public bool IsInitialized => true;

		public void SetAllFields(bool val)
		{
			AllFields = val;
		}

		public void SetFieldAccountLevelInfo(bool val)
		{
			FieldAccountLevelInfo = val;
		}

		public void SetFieldPrivacyInfo(bool val)
		{
			FieldPrivacyInfo = val;
		}

		public void SetFieldParentalControlInfo(bool val)
		{
			FieldParentalControlInfo = val;
		}

		public void SetFieldGameLevelInfo(bool val)
		{
			FieldGameLevelInfo = val;
		}

		public void SetFieldGameStatus(bool val)
		{
			FieldGameStatus = val;
		}

		public void SetFieldGameAccounts(bool val)
		{
			FieldGameAccounts = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAllFields)
			{
				num ^= AllFields.GetHashCode();
			}
			if (HasFieldAccountLevelInfo)
			{
				num ^= FieldAccountLevelInfo.GetHashCode();
			}
			if (HasFieldPrivacyInfo)
			{
				num ^= FieldPrivacyInfo.GetHashCode();
			}
			if (HasFieldParentalControlInfo)
			{
				num ^= FieldParentalControlInfo.GetHashCode();
			}
			if (HasFieldGameLevelInfo)
			{
				num ^= FieldGameLevelInfo.GetHashCode();
			}
			if (HasFieldGameStatus)
			{
				num ^= FieldGameStatus.GetHashCode();
			}
			if (HasFieldGameAccounts)
			{
				num ^= FieldGameAccounts.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountFieldOptions accountFieldOptions = obj as AccountFieldOptions;
			if (accountFieldOptions == null)
			{
				return false;
			}
			if (HasAllFields != accountFieldOptions.HasAllFields || (HasAllFields && !AllFields.Equals(accountFieldOptions.AllFields)))
			{
				return false;
			}
			if (HasFieldAccountLevelInfo != accountFieldOptions.HasFieldAccountLevelInfo || (HasFieldAccountLevelInfo && !FieldAccountLevelInfo.Equals(accountFieldOptions.FieldAccountLevelInfo)))
			{
				return false;
			}
			if (HasFieldPrivacyInfo != accountFieldOptions.HasFieldPrivacyInfo || (HasFieldPrivacyInfo && !FieldPrivacyInfo.Equals(accountFieldOptions.FieldPrivacyInfo)))
			{
				return false;
			}
			if (HasFieldParentalControlInfo != accountFieldOptions.HasFieldParentalControlInfo || (HasFieldParentalControlInfo && !FieldParentalControlInfo.Equals(accountFieldOptions.FieldParentalControlInfo)))
			{
				return false;
			}
			if (HasFieldGameLevelInfo != accountFieldOptions.HasFieldGameLevelInfo || (HasFieldGameLevelInfo && !FieldGameLevelInfo.Equals(accountFieldOptions.FieldGameLevelInfo)))
			{
				return false;
			}
			if (HasFieldGameStatus != accountFieldOptions.HasFieldGameStatus || (HasFieldGameStatus && !FieldGameStatus.Equals(accountFieldOptions.FieldGameStatus)))
			{
				return false;
			}
			if (HasFieldGameAccounts != accountFieldOptions.HasFieldGameAccounts || (HasFieldGameAccounts && !FieldGameAccounts.Equals(accountFieldOptions.FieldGameAccounts)))
			{
				return false;
			}
			return true;
		}

		public static AccountFieldOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountFieldOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AccountFieldOptions Deserialize(Stream stream, AccountFieldOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AccountFieldOptions DeserializeLengthDelimited(Stream stream)
		{
			AccountFieldOptions accountFieldOptions = new AccountFieldOptions();
			DeserializeLengthDelimited(stream, accountFieldOptions);
			return accountFieldOptions;
		}

		public static AccountFieldOptions DeserializeLengthDelimited(Stream stream, AccountFieldOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AccountFieldOptions Deserialize(Stream stream, AccountFieldOptions instance, long limit)
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
					instance.AllFields = ProtocolParser.ReadBool(stream);
					continue;
				case 16:
					instance.FieldAccountLevelInfo = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.FieldPrivacyInfo = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.FieldParentalControlInfo = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.FieldGameLevelInfo = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
					instance.FieldGameStatus = ProtocolParser.ReadBool(stream);
					continue;
				case 64:
					instance.FieldGameAccounts = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, AccountFieldOptions instance)
		{
			if (instance.HasAllFields)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.AllFields);
			}
			if (instance.HasFieldAccountLevelInfo)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.FieldAccountLevelInfo);
			}
			if (instance.HasFieldPrivacyInfo)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.FieldPrivacyInfo);
			}
			if (instance.HasFieldParentalControlInfo)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.FieldParentalControlInfo);
			}
			if (instance.HasFieldGameLevelInfo)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.FieldGameLevelInfo);
			}
			if (instance.HasFieldGameStatus)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.FieldGameStatus);
			}
			if (instance.HasFieldGameAccounts)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.FieldGameAccounts);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAllFields)
			{
				num++;
				num++;
			}
			if (HasFieldAccountLevelInfo)
			{
				num++;
				num++;
			}
			if (HasFieldPrivacyInfo)
			{
				num++;
				num++;
			}
			if (HasFieldParentalControlInfo)
			{
				num++;
				num++;
			}
			if (HasFieldGameLevelInfo)
			{
				num++;
				num++;
			}
			if (HasFieldGameStatus)
			{
				num++;
				num++;
			}
			if (HasFieldGameAccounts)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
