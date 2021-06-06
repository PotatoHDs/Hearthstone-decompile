using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1.Types;

namespace bnet.protocol.account.v1
{
	public class AccountLevelInfo : IProtoBuf
	{
		private List<AccountLicense> _Licenses = new List<AccountLicense>();

		public bool HasDefaultCurrency;

		private uint _DefaultCurrency;

		public bool HasCountry;

		private string _Country;

		public bool HasPreferredRegion;

		private uint _PreferredRegion;

		public bool HasFullName;

		private string _FullName;

		public bool HasBattleTag;

		private string _BattleTag;

		public bool HasMuted;

		private bool _Muted;

		public bool HasManualReview;

		private bool _ManualReview;

		public bool HasAccountPaidAny;

		private bool _AccountPaidAny;

		public bool HasIdentityCheckStatus;

		private IdentityVerificationStatus _IdentityCheckStatus;

		public bool HasEmail;

		private string _Email;

		public bool HasHeadlessAccount;

		private bool _HeadlessAccount;

		public bool HasTestAccount;

		private bool _TestAccount;

		private List<AccountRestriction> _Restriction = new List<AccountRestriction>();

		public bool HasIsSmsProtected;

		private bool _IsSmsProtected;

		public List<AccountLicense> Licenses
		{
			get
			{
				return _Licenses;
			}
			set
			{
				_Licenses = value;
			}
		}

		public List<AccountLicense> LicensesList => _Licenses;

		public int LicensesCount => _Licenses.Count;

		public uint DefaultCurrency
		{
			get
			{
				return _DefaultCurrency;
			}
			set
			{
				_DefaultCurrency = value;
				HasDefaultCurrency = true;
			}
		}

		public string Country
		{
			get
			{
				return _Country;
			}
			set
			{
				_Country = value;
				HasCountry = value != null;
			}
		}

		public uint PreferredRegion
		{
			get
			{
				return _PreferredRegion;
			}
			set
			{
				_PreferredRegion = value;
				HasPreferredRegion = true;
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

		public bool Muted
		{
			get
			{
				return _Muted;
			}
			set
			{
				_Muted = value;
				HasMuted = true;
			}
		}

		public bool ManualReview
		{
			get
			{
				return _ManualReview;
			}
			set
			{
				_ManualReview = value;
				HasManualReview = true;
			}
		}

		public bool AccountPaidAny
		{
			get
			{
				return _AccountPaidAny;
			}
			set
			{
				_AccountPaidAny = value;
				HasAccountPaidAny = true;
			}
		}

		public IdentityVerificationStatus IdentityCheckStatus
		{
			get
			{
				return _IdentityCheckStatus;
			}
			set
			{
				_IdentityCheckStatus = value;
				HasIdentityCheckStatus = true;
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

		public bool HeadlessAccount
		{
			get
			{
				return _HeadlessAccount;
			}
			set
			{
				_HeadlessAccount = value;
				HasHeadlessAccount = true;
			}
		}

		public bool TestAccount
		{
			get
			{
				return _TestAccount;
			}
			set
			{
				_TestAccount = value;
				HasTestAccount = true;
			}
		}

		public List<AccountRestriction> Restriction
		{
			get
			{
				return _Restriction;
			}
			set
			{
				_Restriction = value;
			}
		}

		public List<AccountRestriction> RestrictionList => _Restriction;

		public int RestrictionCount => _Restriction.Count;

		public bool IsSmsProtected
		{
			get
			{
				return _IsSmsProtected;
			}
			set
			{
				_IsSmsProtected = value;
				HasIsSmsProtected = true;
			}
		}

		public bool IsInitialized => true;

		public void AddLicenses(AccountLicense val)
		{
			_Licenses.Add(val);
		}

		public void ClearLicenses()
		{
			_Licenses.Clear();
		}

		public void SetLicenses(List<AccountLicense> val)
		{
			Licenses = val;
		}

		public void SetDefaultCurrency(uint val)
		{
			DefaultCurrency = val;
		}

		public void SetCountry(string val)
		{
			Country = val;
		}

		public void SetPreferredRegion(uint val)
		{
			PreferredRegion = val;
		}

		public void SetFullName(string val)
		{
			FullName = val;
		}

		public void SetBattleTag(string val)
		{
			BattleTag = val;
		}

		public void SetMuted(bool val)
		{
			Muted = val;
		}

		public void SetManualReview(bool val)
		{
			ManualReview = val;
		}

		public void SetAccountPaidAny(bool val)
		{
			AccountPaidAny = val;
		}

		public void SetIdentityCheckStatus(IdentityVerificationStatus val)
		{
			IdentityCheckStatus = val;
		}

		public void SetEmail(string val)
		{
			Email = val;
		}

		public void SetHeadlessAccount(bool val)
		{
			HeadlessAccount = val;
		}

		public void SetTestAccount(bool val)
		{
			TestAccount = val;
		}

		public void AddRestriction(AccountRestriction val)
		{
			_Restriction.Add(val);
		}

		public void ClearRestriction()
		{
			_Restriction.Clear();
		}

		public void SetRestriction(List<AccountRestriction> val)
		{
			Restriction = val;
		}

		public void SetIsSmsProtected(bool val)
		{
			IsSmsProtected = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (AccountLicense license in Licenses)
			{
				num ^= license.GetHashCode();
			}
			if (HasDefaultCurrency)
			{
				num ^= DefaultCurrency.GetHashCode();
			}
			if (HasCountry)
			{
				num ^= Country.GetHashCode();
			}
			if (HasPreferredRegion)
			{
				num ^= PreferredRegion.GetHashCode();
			}
			if (HasFullName)
			{
				num ^= FullName.GetHashCode();
			}
			if (HasBattleTag)
			{
				num ^= BattleTag.GetHashCode();
			}
			if (HasMuted)
			{
				num ^= Muted.GetHashCode();
			}
			if (HasManualReview)
			{
				num ^= ManualReview.GetHashCode();
			}
			if (HasAccountPaidAny)
			{
				num ^= AccountPaidAny.GetHashCode();
			}
			if (HasIdentityCheckStatus)
			{
				num ^= IdentityCheckStatus.GetHashCode();
			}
			if (HasEmail)
			{
				num ^= Email.GetHashCode();
			}
			if (HasHeadlessAccount)
			{
				num ^= HeadlessAccount.GetHashCode();
			}
			if (HasTestAccount)
			{
				num ^= TestAccount.GetHashCode();
			}
			foreach (AccountRestriction item in Restriction)
			{
				num ^= item.GetHashCode();
			}
			if (HasIsSmsProtected)
			{
				num ^= IsSmsProtected.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AccountLevelInfo accountLevelInfo = obj as AccountLevelInfo;
			if (accountLevelInfo == null)
			{
				return false;
			}
			if (Licenses.Count != accountLevelInfo.Licenses.Count)
			{
				return false;
			}
			for (int i = 0; i < Licenses.Count; i++)
			{
				if (!Licenses[i].Equals(accountLevelInfo.Licenses[i]))
				{
					return false;
				}
			}
			if (HasDefaultCurrency != accountLevelInfo.HasDefaultCurrency || (HasDefaultCurrency && !DefaultCurrency.Equals(accountLevelInfo.DefaultCurrency)))
			{
				return false;
			}
			if (HasCountry != accountLevelInfo.HasCountry || (HasCountry && !Country.Equals(accountLevelInfo.Country)))
			{
				return false;
			}
			if (HasPreferredRegion != accountLevelInfo.HasPreferredRegion || (HasPreferredRegion && !PreferredRegion.Equals(accountLevelInfo.PreferredRegion)))
			{
				return false;
			}
			if (HasFullName != accountLevelInfo.HasFullName || (HasFullName && !FullName.Equals(accountLevelInfo.FullName)))
			{
				return false;
			}
			if (HasBattleTag != accountLevelInfo.HasBattleTag || (HasBattleTag && !BattleTag.Equals(accountLevelInfo.BattleTag)))
			{
				return false;
			}
			if (HasMuted != accountLevelInfo.HasMuted || (HasMuted && !Muted.Equals(accountLevelInfo.Muted)))
			{
				return false;
			}
			if (HasManualReview != accountLevelInfo.HasManualReview || (HasManualReview && !ManualReview.Equals(accountLevelInfo.ManualReview)))
			{
				return false;
			}
			if (HasAccountPaidAny != accountLevelInfo.HasAccountPaidAny || (HasAccountPaidAny && !AccountPaidAny.Equals(accountLevelInfo.AccountPaidAny)))
			{
				return false;
			}
			if (HasIdentityCheckStatus != accountLevelInfo.HasIdentityCheckStatus || (HasIdentityCheckStatus && !IdentityCheckStatus.Equals(accountLevelInfo.IdentityCheckStatus)))
			{
				return false;
			}
			if (HasEmail != accountLevelInfo.HasEmail || (HasEmail && !Email.Equals(accountLevelInfo.Email)))
			{
				return false;
			}
			if (HasHeadlessAccount != accountLevelInfo.HasHeadlessAccount || (HasHeadlessAccount && !HeadlessAccount.Equals(accountLevelInfo.HeadlessAccount)))
			{
				return false;
			}
			if (HasTestAccount != accountLevelInfo.HasTestAccount || (HasTestAccount && !TestAccount.Equals(accountLevelInfo.TestAccount)))
			{
				return false;
			}
			if (Restriction.Count != accountLevelInfo.Restriction.Count)
			{
				return false;
			}
			for (int j = 0; j < Restriction.Count; j++)
			{
				if (!Restriction[j].Equals(accountLevelInfo.Restriction[j]))
				{
					return false;
				}
			}
			if (HasIsSmsProtected != accountLevelInfo.HasIsSmsProtected || (HasIsSmsProtected && !IsSmsProtected.Equals(accountLevelInfo.IsSmsProtected)))
			{
				return false;
			}
			return true;
		}

		public static AccountLevelInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountLevelInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AccountLevelInfo Deserialize(Stream stream, AccountLevelInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AccountLevelInfo DeserializeLengthDelimited(Stream stream)
		{
			AccountLevelInfo accountLevelInfo = new AccountLevelInfo();
			DeserializeLengthDelimited(stream, accountLevelInfo);
			return accountLevelInfo;
		}

		public static AccountLevelInfo DeserializeLengthDelimited(Stream stream, AccountLevelInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AccountLevelInfo Deserialize(Stream stream, AccountLevelInfo instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Licenses == null)
			{
				instance.Licenses = new List<AccountLicense>();
			}
			instance.IdentityCheckStatus = IdentityVerificationStatus.IDENT_NO_DATA;
			if (instance.Restriction == null)
			{
				instance.Restriction = new List<AccountRestriction>();
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
				case 26:
					instance.Licenses.Add(AccountLicense.DeserializeLengthDelimited(stream));
					continue;
				case 37:
					instance.DefaultCurrency = binaryReader.ReadUInt32();
					continue;
				case 42:
					instance.Country = ProtocolParser.ReadString(stream);
					continue;
				case 48:
					instance.PreferredRegion = ProtocolParser.ReadUInt32(stream);
					continue;
				case 58:
					instance.FullName = ProtocolParser.ReadString(stream);
					continue;
				case 66:
					instance.BattleTag = ProtocolParser.ReadString(stream);
					continue;
				case 72:
					instance.Muted = ProtocolParser.ReadBool(stream);
					continue;
				case 80:
					instance.ManualReview = ProtocolParser.ReadBool(stream);
					continue;
				case 88:
					instance.AccountPaidAny = ProtocolParser.ReadBool(stream);
					continue;
				case 96:
					instance.IdentityCheckStatus = (IdentityVerificationStatus)ProtocolParser.ReadUInt64(stream);
					continue;
				case 106:
					instance.Email = ProtocolParser.ReadString(stream);
					continue;
				case 112:
					instance.HeadlessAccount = ProtocolParser.ReadBool(stream);
					continue;
				case 120:
					instance.TestAccount = ProtocolParser.ReadBool(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 16u:
						if (key.WireType == Wire.LengthDelimited)
						{
							instance.Restriction.Add(AccountRestriction.DeserializeLengthDelimited(stream));
						}
						break;
					case 17u:
						if (key.WireType == Wire.Varint)
						{
							instance.IsSmsProtected = ProtocolParser.ReadBool(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
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

		public static void Serialize(Stream stream, AccountLevelInfo instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Licenses.Count > 0)
			{
				foreach (AccountLicense license in instance.Licenses)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, license.GetSerializedSize());
					AccountLicense.Serialize(stream, license);
				}
			}
			if (instance.HasDefaultCurrency)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.DefaultCurrency);
			}
			if (instance.HasCountry)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Country));
			}
			if (instance.HasPreferredRegion)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.PreferredRegion);
			}
			if (instance.HasFullName)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FullName));
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
			if (instance.HasMuted)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.Muted);
			}
			if (instance.HasManualReview)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteBool(stream, instance.ManualReview);
			}
			if (instance.HasAccountPaidAny)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteBool(stream, instance.AccountPaidAny);
			}
			if (instance.HasIdentityCheckStatus)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.IdentityCheckStatus);
			}
			if (instance.HasEmail)
			{
				stream.WriteByte(106);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Email));
			}
			if (instance.HasHeadlessAccount)
			{
				stream.WriteByte(112);
				ProtocolParser.WriteBool(stream, instance.HeadlessAccount);
			}
			if (instance.HasTestAccount)
			{
				stream.WriteByte(120);
				ProtocolParser.WriteBool(stream, instance.TestAccount);
			}
			if (instance.Restriction.Count > 0)
			{
				foreach (AccountRestriction item in instance.Restriction)
				{
					stream.WriteByte(130);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					AccountRestriction.Serialize(stream, item);
				}
			}
			if (instance.HasIsSmsProtected)
			{
				stream.WriteByte(136);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.IsSmsProtected);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Licenses.Count > 0)
			{
				foreach (AccountLicense license in Licenses)
				{
					num++;
					uint serializedSize = license.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasDefaultCurrency)
			{
				num++;
				num += 4;
			}
			if (HasCountry)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Country);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasPreferredRegion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(PreferredRegion);
			}
			if (HasFullName)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(FullName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasBattleTag)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasMuted)
			{
				num++;
				num++;
			}
			if (HasManualReview)
			{
				num++;
				num++;
			}
			if (HasAccountPaidAny)
			{
				num++;
				num++;
			}
			if (HasIdentityCheckStatus)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)IdentityCheckStatus);
			}
			if (HasEmail)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(Email);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (HasHeadlessAccount)
			{
				num++;
				num++;
			}
			if (HasTestAccount)
			{
				num++;
				num++;
			}
			if (Restriction.Count > 0)
			{
				foreach (AccountRestriction item in Restriction)
				{
					num += 2;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasIsSmsProtected)
			{
				num += 2;
				num++;
			}
			return num;
		}
	}
}
