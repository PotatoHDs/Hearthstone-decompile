using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1.Types;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200052D RID: 1325
	public class AccountLevelInfo : IProtoBuf
	{
		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06005EEC RID: 24300 RVA: 0x0011F855 File Offset: 0x0011DA55
		// (set) Token: 0x06005EED RID: 24301 RVA: 0x0011F85D File Offset: 0x0011DA5D
		public List<AccountLicense> Licenses
		{
			get
			{
				return this._Licenses;
			}
			set
			{
				this._Licenses = value;
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06005EEE RID: 24302 RVA: 0x0011F855 File Offset: 0x0011DA55
		public List<AccountLicense> LicensesList
		{
			get
			{
				return this._Licenses;
			}
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06005EEF RID: 24303 RVA: 0x0011F866 File Offset: 0x0011DA66
		public int LicensesCount
		{
			get
			{
				return this._Licenses.Count;
			}
		}

		// Token: 0x06005EF0 RID: 24304 RVA: 0x0011F873 File Offset: 0x0011DA73
		public void AddLicenses(AccountLicense val)
		{
			this._Licenses.Add(val);
		}

		// Token: 0x06005EF1 RID: 24305 RVA: 0x0011F881 File Offset: 0x0011DA81
		public void ClearLicenses()
		{
			this._Licenses.Clear();
		}

		// Token: 0x06005EF2 RID: 24306 RVA: 0x0011F88E File Offset: 0x0011DA8E
		public void SetLicenses(List<AccountLicense> val)
		{
			this.Licenses = val;
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x06005EF3 RID: 24307 RVA: 0x0011F897 File Offset: 0x0011DA97
		// (set) Token: 0x06005EF4 RID: 24308 RVA: 0x0011F89F File Offset: 0x0011DA9F
		public uint DefaultCurrency
		{
			get
			{
				return this._DefaultCurrency;
			}
			set
			{
				this._DefaultCurrency = value;
				this.HasDefaultCurrency = true;
			}
		}

		// Token: 0x06005EF5 RID: 24309 RVA: 0x0011F8AF File Offset: 0x0011DAAF
		public void SetDefaultCurrency(uint val)
		{
			this.DefaultCurrency = val;
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06005EF6 RID: 24310 RVA: 0x0011F8B8 File Offset: 0x0011DAB8
		// (set) Token: 0x06005EF7 RID: 24311 RVA: 0x0011F8C0 File Offset: 0x0011DAC0
		public string Country
		{
			get
			{
				return this._Country;
			}
			set
			{
				this._Country = value;
				this.HasCountry = (value != null);
			}
		}

		// Token: 0x06005EF8 RID: 24312 RVA: 0x0011F8D3 File Offset: 0x0011DAD3
		public void SetCountry(string val)
		{
			this.Country = val;
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06005EF9 RID: 24313 RVA: 0x0011F8DC File Offset: 0x0011DADC
		// (set) Token: 0x06005EFA RID: 24314 RVA: 0x0011F8E4 File Offset: 0x0011DAE4
		public uint PreferredRegion
		{
			get
			{
				return this._PreferredRegion;
			}
			set
			{
				this._PreferredRegion = value;
				this.HasPreferredRegion = true;
			}
		}

		// Token: 0x06005EFB RID: 24315 RVA: 0x0011F8F4 File Offset: 0x0011DAF4
		public void SetPreferredRegion(uint val)
		{
			this.PreferredRegion = val;
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x06005EFC RID: 24316 RVA: 0x0011F8FD File Offset: 0x0011DAFD
		// (set) Token: 0x06005EFD RID: 24317 RVA: 0x0011F905 File Offset: 0x0011DB05
		public string FullName
		{
			get
			{
				return this._FullName;
			}
			set
			{
				this._FullName = value;
				this.HasFullName = (value != null);
			}
		}

		// Token: 0x06005EFE RID: 24318 RVA: 0x0011F918 File Offset: 0x0011DB18
		public void SetFullName(string val)
		{
			this.FullName = val;
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06005EFF RID: 24319 RVA: 0x0011F921 File Offset: 0x0011DB21
		// (set) Token: 0x06005F00 RID: 24320 RVA: 0x0011F929 File Offset: 0x0011DB29
		public string BattleTag
		{
			get
			{
				return this._BattleTag;
			}
			set
			{
				this._BattleTag = value;
				this.HasBattleTag = (value != null);
			}
		}

		// Token: 0x06005F01 RID: 24321 RVA: 0x0011F93C File Offset: 0x0011DB3C
		public void SetBattleTag(string val)
		{
			this.BattleTag = val;
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x06005F02 RID: 24322 RVA: 0x0011F945 File Offset: 0x0011DB45
		// (set) Token: 0x06005F03 RID: 24323 RVA: 0x0011F94D File Offset: 0x0011DB4D
		public bool Muted
		{
			get
			{
				return this._Muted;
			}
			set
			{
				this._Muted = value;
				this.HasMuted = true;
			}
		}

		// Token: 0x06005F04 RID: 24324 RVA: 0x0011F95D File Offset: 0x0011DB5D
		public void SetMuted(bool val)
		{
			this.Muted = val;
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06005F05 RID: 24325 RVA: 0x0011F966 File Offset: 0x0011DB66
		// (set) Token: 0x06005F06 RID: 24326 RVA: 0x0011F96E File Offset: 0x0011DB6E
		public bool ManualReview
		{
			get
			{
				return this._ManualReview;
			}
			set
			{
				this._ManualReview = value;
				this.HasManualReview = true;
			}
		}

		// Token: 0x06005F07 RID: 24327 RVA: 0x0011F97E File Offset: 0x0011DB7E
		public void SetManualReview(bool val)
		{
			this.ManualReview = val;
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06005F08 RID: 24328 RVA: 0x0011F987 File Offset: 0x0011DB87
		// (set) Token: 0x06005F09 RID: 24329 RVA: 0x0011F98F File Offset: 0x0011DB8F
		public bool AccountPaidAny
		{
			get
			{
				return this._AccountPaidAny;
			}
			set
			{
				this._AccountPaidAny = value;
				this.HasAccountPaidAny = true;
			}
		}

		// Token: 0x06005F0A RID: 24330 RVA: 0x0011F99F File Offset: 0x0011DB9F
		public void SetAccountPaidAny(bool val)
		{
			this.AccountPaidAny = val;
		}

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06005F0B RID: 24331 RVA: 0x0011F9A8 File Offset: 0x0011DBA8
		// (set) Token: 0x06005F0C RID: 24332 RVA: 0x0011F9B0 File Offset: 0x0011DBB0
		public IdentityVerificationStatus IdentityCheckStatus
		{
			get
			{
				return this._IdentityCheckStatus;
			}
			set
			{
				this._IdentityCheckStatus = value;
				this.HasIdentityCheckStatus = true;
			}
		}

		// Token: 0x06005F0D RID: 24333 RVA: 0x0011F9C0 File Offset: 0x0011DBC0
		public void SetIdentityCheckStatus(IdentityVerificationStatus val)
		{
			this.IdentityCheckStatus = val;
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06005F0E RID: 24334 RVA: 0x0011F9C9 File Offset: 0x0011DBC9
		// (set) Token: 0x06005F0F RID: 24335 RVA: 0x0011F9D1 File Offset: 0x0011DBD1
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				this._Email = value;
				this.HasEmail = (value != null);
			}
		}

		// Token: 0x06005F10 RID: 24336 RVA: 0x0011F9E4 File Offset: 0x0011DBE4
		public void SetEmail(string val)
		{
			this.Email = val;
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06005F11 RID: 24337 RVA: 0x0011F9ED File Offset: 0x0011DBED
		// (set) Token: 0x06005F12 RID: 24338 RVA: 0x0011F9F5 File Offset: 0x0011DBF5
		public bool HeadlessAccount
		{
			get
			{
				return this._HeadlessAccount;
			}
			set
			{
				this._HeadlessAccount = value;
				this.HasHeadlessAccount = true;
			}
		}

		// Token: 0x06005F13 RID: 24339 RVA: 0x0011FA05 File Offset: 0x0011DC05
		public void SetHeadlessAccount(bool val)
		{
			this.HeadlessAccount = val;
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06005F14 RID: 24340 RVA: 0x0011FA0E File Offset: 0x0011DC0E
		// (set) Token: 0x06005F15 RID: 24341 RVA: 0x0011FA16 File Offset: 0x0011DC16
		public bool TestAccount
		{
			get
			{
				return this._TestAccount;
			}
			set
			{
				this._TestAccount = value;
				this.HasTestAccount = true;
			}
		}

		// Token: 0x06005F16 RID: 24342 RVA: 0x0011FA26 File Offset: 0x0011DC26
		public void SetTestAccount(bool val)
		{
			this.TestAccount = val;
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06005F17 RID: 24343 RVA: 0x0011FA2F File Offset: 0x0011DC2F
		// (set) Token: 0x06005F18 RID: 24344 RVA: 0x0011FA37 File Offset: 0x0011DC37
		public List<AccountRestriction> Restriction
		{
			get
			{
				return this._Restriction;
			}
			set
			{
				this._Restriction = value;
			}
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06005F19 RID: 24345 RVA: 0x0011FA2F File Offset: 0x0011DC2F
		public List<AccountRestriction> RestrictionList
		{
			get
			{
				return this._Restriction;
			}
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06005F1A RID: 24346 RVA: 0x0011FA40 File Offset: 0x0011DC40
		public int RestrictionCount
		{
			get
			{
				return this._Restriction.Count;
			}
		}

		// Token: 0x06005F1B RID: 24347 RVA: 0x0011FA4D File Offset: 0x0011DC4D
		public void AddRestriction(AccountRestriction val)
		{
			this._Restriction.Add(val);
		}

		// Token: 0x06005F1C RID: 24348 RVA: 0x0011FA5B File Offset: 0x0011DC5B
		public void ClearRestriction()
		{
			this._Restriction.Clear();
		}

		// Token: 0x06005F1D RID: 24349 RVA: 0x0011FA68 File Offset: 0x0011DC68
		public void SetRestriction(List<AccountRestriction> val)
		{
			this.Restriction = val;
		}

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06005F1E RID: 24350 RVA: 0x0011FA71 File Offset: 0x0011DC71
		// (set) Token: 0x06005F1F RID: 24351 RVA: 0x0011FA79 File Offset: 0x0011DC79
		public bool IsSmsProtected
		{
			get
			{
				return this._IsSmsProtected;
			}
			set
			{
				this._IsSmsProtected = value;
				this.HasIsSmsProtected = true;
			}
		}

		// Token: 0x06005F20 RID: 24352 RVA: 0x0011FA89 File Offset: 0x0011DC89
		public void SetIsSmsProtected(bool val)
		{
			this.IsSmsProtected = val;
		}

		// Token: 0x06005F21 RID: 24353 RVA: 0x0011FA94 File Offset: 0x0011DC94
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (AccountLicense accountLicense in this.Licenses)
			{
				num ^= accountLicense.GetHashCode();
			}
			if (this.HasDefaultCurrency)
			{
				num ^= this.DefaultCurrency.GetHashCode();
			}
			if (this.HasCountry)
			{
				num ^= this.Country.GetHashCode();
			}
			if (this.HasPreferredRegion)
			{
				num ^= this.PreferredRegion.GetHashCode();
			}
			if (this.HasFullName)
			{
				num ^= this.FullName.GetHashCode();
			}
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			if (this.HasMuted)
			{
				num ^= this.Muted.GetHashCode();
			}
			if (this.HasManualReview)
			{
				num ^= this.ManualReview.GetHashCode();
			}
			if (this.HasAccountPaidAny)
			{
				num ^= this.AccountPaidAny.GetHashCode();
			}
			if (this.HasIdentityCheckStatus)
			{
				num ^= this.IdentityCheckStatus.GetHashCode();
			}
			if (this.HasEmail)
			{
				num ^= this.Email.GetHashCode();
			}
			if (this.HasHeadlessAccount)
			{
				num ^= this.HeadlessAccount.GetHashCode();
			}
			if (this.HasTestAccount)
			{
				num ^= this.TestAccount.GetHashCode();
			}
			foreach (AccountRestriction accountRestriction in this.Restriction)
			{
				num ^= accountRestriction.GetHashCode();
			}
			if (this.HasIsSmsProtected)
			{
				num ^= this.IsSmsProtected.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005F22 RID: 24354 RVA: 0x0011FC84 File Offset: 0x0011DE84
		public override bool Equals(object obj)
		{
			AccountLevelInfo accountLevelInfo = obj as AccountLevelInfo;
			if (accountLevelInfo == null)
			{
				return false;
			}
			if (this.Licenses.Count != accountLevelInfo.Licenses.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Licenses.Count; i++)
			{
				if (!this.Licenses[i].Equals(accountLevelInfo.Licenses[i]))
				{
					return false;
				}
			}
			if (this.HasDefaultCurrency != accountLevelInfo.HasDefaultCurrency || (this.HasDefaultCurrency && !this.DefaultCurrency.Equals(accountLevelInfo.DefaultCurrency)))
			{
				return false;
			}
			if (this.HasCountry != accountLevelInfo.HasCountry || (this.HasCountry && !this.Country.Equals(accountLevelInfo.Country)))
			{
				return false;
			}
			if (this.HasPreferredRegion != accountLevelInfo.HasPreferredRegion || (this.HasPreferredRegion && !this.PreferredRegion.Equals(accountLevelInfo.PreferredRegion)))
			{
				return false;
			}
			if (this.HasFullName != accountLevelInfo.HasFullName || (this.HasFullName && !this.FullName.Equals(accountLevelInfo.FullName)))
			{
				return false;
			}
			if (this.HasBattleTag != accountLevelInfo.HasBattleTag || (this.HasBattleTag && !this.BattleTag.Equals(accountLevelInfo.BattleTag)))
			{
				return false;
			}
			if (this.HasMuted != accountLevelInfo.HasMuted || (this.HasMuted && !this.Muted.Equals(accountLevelInfo.Muted)))
			{
				return false;
			}
			if (this.HasManualReview != accountLevelInfo.HasManualReview || (this.HasManualReview && !this.ManualReview.Equals(accountLevelInfo.ManualReview)))
			{
				return false;
			}
			if (this.HasAccountPaidAny != accountLevelInfo.HasAccountPaidAny || (this.HasAccountPaidAny && !this.AccountPaidAny.Equals(accountLevelInfo.AccountPaidAny)))
			{
				return false;
			}
			if (this.HasIdentityCheckStatus != accountLevelInfo.HasIdentityCheckStatus || (this.HasIdentityCheckStatus && !this.IdentityCheckStatus.Equals(accountLevelInfo.IdentityCheckStatus)))
			{
				return false;
			}
			if (this.HasEmail != accountLevelInfo.HasEmail || (this.HasEmail && !this.Email.Equals(accountLevelInfo.Email)))
			{
				return false;
			}
			if (this.HasHeadlessAccount != accountLevelInfo.HasHeadlessAccount || (this.HasHeadlessAccount && !this.HeadlessAccount.Equals(accountLevelInfo.HeadlessAccount)))
			{
				return false;
			}
			if (this.HasTestAccount != accountLevelInfo.HasTestAccount || (this.HasTestAccount && !this.TestAccount.Equals(accountLevelInfo.TestAccount)))
			{
				return false;
			}
			if (this.Restriction.Count != accountLevelInfo.Restriction.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Restriction.Count; j++)
			{
				if (!this.Restriction[j].Equals(accountLevelInfo.Restriction[j]))
				{
					return false;
				}
			}
			return this.HasIsSmsProtected == accountLevelInfo.HasIsSmsProtected && (!this.HasIsSmsProtected || this.IsSmsProtected.Equals(accountLevelInfo.IsSmsProtected));
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06005F23 RID: 24355 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005F24 RID: 24356 RVA: 0x0011FF9C File Offset: 0x0011E19C
		public static AccountLevelInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountLevelInfo>(bs, 0, -1);
		}

		// Token: 0x06005F25 RID: 24357 RVA: 0x0011FFA6 File Offset: 0x0011E1A6
		public void Deserialize(Stream stream)
		{
			AccountLevelInfo.Deserialize(stream, this);
		}

		// Token: 0x06005F26 RID: 24358 RVA: 0x0011FFB0 File Offset: 0x0011E1B0
		public static AccountLevelInfo Deserialize(Stream stream, AccountLevelInfo instance)
		{
			return AccountLevelInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005F27 RID: 24359 RVA: 0x0011FFBC File Offset: 0x0011E1BC
		public static AccountLevelInfo DeserializeLengthDelimited(Stream stream)
		{
			AccountLevelInfo accountLevelInfo = new AccountLevelInfo();
			AccountLevelInfo.DeserializeLengthDelimited(stream, accountLevelInfo);
			return accountLevelInfo;
		}

		// Token: 0x06005F28 RID: 24360 RVA: 0x0011FFD8 File Offset: 0x0011E1D8
		public static AccountLevelInfo DeserializeLengthDelimited(Stream stream, AccountLevelInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountLevelInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06005F29 RID: 24361 RVA: 0x00120000 File Offset: 0x0011E200
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
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else
				{
					if (num <= 66)
					{
						if (num <= 42)
						{
							if (num == 26)
							{
								instance.Licenses.Add(AccountLicense.DeserializeLengthDelimited(stream));
								continue;
							}
							if (num == 37)
							{
								instance.DefaultCurrency = binaryReader.ReadUInt32();
								continue;
							}
							if (num == 42)
							{
								instance.Country = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (num == 48)
							{
								instance.PreferredRegion = ProtocolParser.ReadUInt32(stream);
								continue;
							}
							if (num == 58)
							{
								instance.FullName = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 66)
							{
								instance.BattleTag = ProtocolParser.ReadString(stream);
								continue;
							}
						}
					}
					else if (num <= 88)
					{
						if (num == 72)
						{
							instance.Muted = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 80)
						{
							instance.ManualReview = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 88)
						{
							instance.AccountPaidAny = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else if (num <= 106)
					{
						if (num == 96)
						{
							instance.IdentityCheckStatus = (IdentityVerificationStatus)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 106)
						{
							instance.Email = ProtocolParser.ReadString(stream);
							continue;
						}
					}
					else
					{
						if (num == 112)
						{
							instance.HeadlessAccount = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 120)
						{
							instance.TestAccount = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					if (field != 16U)
					{
						if (field != 17U)
						{
							ProtocolParser.SkipKey(stream, key);
						}
						else if (key.WireType == Wire.Varint)
						{
							instance.IsSmsProtected = ProtocolParser.ReadBool(stream);
						}
					}
					else if (key.WireType == Wire.LengthDelimited)
					{
						instance.Restriction.Add(AccountRestriction.DeserializeLengthDelimited(stream));
					}
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005F2A RID: 24362 RVA: 0x0012026C File Offset: 0x0011E46C
		public void Serialize(Stream stream)
		{
			AccountLevelInfo.Serialize(stream, this);
		}

		// Token: 0x06005F2B RID: 24363 RVA: 0x00120278 File Offset: 0x0011E478
		public static void Serialize(Stream stream, AccountLevelInfo instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Licenses.Count > 0)
			{
				foreach (AccountLicense accountLicense in instance.Licenses)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, accountLicense.GetSerializedSize());
					AccountLicense.Serialize(stream, accountLicense);
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
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.IdentityCheckStatus));
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
				foreach (AccountRestriction accountRestriction in instance.Restriction)
				{
					stream.WriteByte(130);
					stream.WriteByte(1);
					ProtocolParser.WriteUInt32(stream, accountRestriction.GetSerializedSize());
					AccountRestriction.Serialize(stream, accountRestriction);
				}
			}
			if (instance.HasIsSmsProtected)
			{
				stream.WriteByte(136);
				stream.WriteByte(1);
				ProtocolParser.WriteBool(stream, instance.IsSmsProtected);
			}
		}

		// Token: 0x06005F2C RID: 24364 RVA: 0x00120504 File Offset: 0x0011E704
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Licenses.Count > 0)
			{
				foreach (AccountLicense accountLicense in this.Licenses)
				{
					num += 1U;
					uint serializedSize = accountLicense.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasDefaultCurrency)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasCountry)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Country);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasPreferredRegion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.PreferredRegion);
			}
			if (this.HasFullName)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.FullName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasBattleTag)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasMuted)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasManualReview)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasAccountPaidAny)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIdentityCheckStatus)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.IdentityCheckStatus));
			}
			if (this.HasEmail)
			{
				num += 1U;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.Email);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasHeadlessAccount)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasTestAccount)
			{
				num += 1U;
				num += 1U;
			}
			if (this.Restriction.Count > 0)
			{
				foreach (AccountRestriction accountRestriction in this.Restriction)
				{
					num += 2U;
					uint serializedSize2 = accountRestriction.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasIsSmsProtected)
			{
				num += 2U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001D35 RID: 7477
		private List<AccountLicense> _Licenses = new List<AccountLicense>();

		// Token: 0x04001D36 RID: 7478
		public bool HasDefaultCurrency;

		// Token: 0x04001D37 RID: 7479
		private uint _DefaultCurrency;

		// Token: 0x04001D38 RID: 7480
		public bool HasCountry;

		// Token: 0x04001D39 RID: 7481
		private string _Country;

		// Token: 0x04001D3A RID: 7482
		public bool HasPreferredRegion;

		// Token: 0x04001D3B RID: 7483
		private uint _PreferredRegion;

		// Token: 0x04001D3C RID: 7484
		public bool HasFullName;

		// Token: 0x04001D3D RID: 7485
		private string _FullName;

		// Token: 0x04001D3E RID: 7486
		public bool HasBattleTag;

		// Token: 0x04001D3F RID: 7487
		private string _BattleTag;

		// Token: 0x04001D40 RID: 7488
		public bool HasMuted;

		// Token: 0x04001D41 RID: 7489
		private bool _Muted;

		// Token: 0x04001D42 RID: 7490
		public bool HasManualReview;

		// Token: 0x04001D43 RID: 7491
		private bool _ManualReview;

		// Token: 0x04001D44 RID: 7492
		public bool HasAccountPaidAny;

		// Token: 0x04001D45 RID: 7493
		private bool _AccountPaidAny;

		// Token: 0x04001D46 RID: 7494
		public bool HasIdentityCheckStatus;

		// Token: 0x04001D47 RID: 7495
		private IdentityVerificationStatus _IdentityCheckStatus;

		// Token: 0x04001D48 RID: 7496
		public bool HasEmail;

		// Token: 0x04001D49 RID: 7497
		private string _Email;

		// Token: 0x04001D4A RID: 7498
		public bool HasHeadlessAccount;

		// Token: 0x04001D4B RID: 7499
		private bool _HeadlessAccount;

		// Token: 0x04001D4C RID: 7500
		public bool HasTestAccount;

		// Token: 0x04001D4D RID: 7501
		private bool _TestAccount;

		// Token: 0x04001D4E RID: 7502
		private List<AccountRestriction> _Restriction = new List<AccountRestriction>();

		// Token: 0x04001D4F RID: 7503
		public bool HasIsSmsProtected;

		// Token: 0x04001D50 RID: 7504
		private bool _IsSmsProtected;
	}
}
