using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200052A RID: 1322
	public class AccountFieldOptions : IProtoBuf
	{
		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06005E8C RID: 24204 RVA: 0x0011E680 File Offset: 0x0011C880
		// (set) Token: 0x06005E8D RID: 24205 RVA: 0x0011E688 File Offset: 0x0011C888
		public bool AllFields
		{
			get
			{
				return this._AllFields;
			}
			set
			{
				this._AllFields = value;
				this.HasAllFields = true;
			}
		}

		// Token: 0x06005E8E RID: 24206 RVA: 0x0011E698 File Offset: 0x0011C898
		public void SetAllFields(bool val)
		{
			this.AllFields = val;
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06005E8F RID: 24207 RVA: 0x0011E6A1 File Offset: 0x0011C8A1
		// (set) Token: 0x06005E90 RID: 24208 RVA: 0x0011E6A9 File Offset: 0x0011C8A9
		public bool FieldAccountLevelInfo
		{
			get
			{
				return this._FieldAccountLevelInfo;
			}
			set
			{
				this._FieldAccountLevelInfo = value;
				this.HasFieldAccountLevelInfo = true;
			}
		}

		// Token: 0x06005E91 RID: 24209 RVA: 0x0011E6B9 File Offset: 0x0011C8B9
		public void SetFieldAccountLevelInfo(bool val)
		{
			this.FieldAccountLevelInfo = val;
		}

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06005E92 RID: 24210 RVA: 0x0011E6C2 File Offset: 0x0011C8C2
		// (set) Token: 0x06005E93 RID: 24211 RVA: 0x0011E6CA File Offset: 0x0011C8CA
		public bool FieldPrivacyInfo
		{
			get
			{
				return this._FieldPrivacyInfo;
			}
			set
			{
				this._FieldPrivacyInfo = value;
				this.HasFieldPrivacyInfo = true;
			}
		}

		// Token: 0x06005E94 RID: 24212 RVA: 0x0011E6DA File Offset: 0x0011C8DA
		public void SetFieldPrivacyInfo(bool val)
		{
			this.FieldPrivacyInfo = val;
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06005E95 RID: 24213 RVA: 0x0011E6E3 File Offset: 0x0011C8E3
		// (set) Token: 0x06005E96 RID: 24214 RVA: 0x0011E6EB File Offset: 0x0011C8EB
		public bool FieldParentalControlInfo
		{
			get
			{
				return this._FieldParentalControlInfo;
			}
			set
			{
				this._FieldParentalControlInfo = value;
				this.HasFieldParentalControlInfo = true;
			}
		}

		// Token: 0x06005E97 RID: 24215 RVA: 0x0011E6FB File Offset: 0x0011C8FB
		public void SetFieldParentalControlInfo(bool val)
		{
			this.FieldParentalControlInfo = val;
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06005E98 RID: 24216 RVA: 0x0011E704 File Offset: 0x0011C904
		// (set) Token: 0x06005E99 RID: 24217 RVA: 0x0011E70C File Offset: 0x0011C90C
		public bool FieldGameLevelInfo
		{
			get
			{
				return this._FieldGameLevelInfo;
			}
			set
			{
				this._FieldGameLevelInfo = value;
				this.HasFieldGameLevelInfo = true;
			}
		}

		// Token: 0x06005E9A RID: 24218 RVA: 0x0011E71C File Offset: 0x0011C91C
		public void SetFieldGameLevelInfo(bool val)
		{
			this.FieldGameLevelInfo = val;
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06005E9B RID: 24219 RVA: 0x0011E725 File Offset: 0x0011C925
		// (set) Token: 0x06005E9C RID: 24220 RVA: 0x0011E72D File Offset: 0x0011C92D
		public bool FieldGameStatus
		{
			get
			{
				return this._FieldGameStatus;
			}
			set
			{
				this._FieldGameStatus = value;
				this.HasFieldGameStatus = true;
			}
		}

		// Token: 0x06005E9D RID: 24221 RVA: 0x0011E73D File Offset: 0x0011C93D
		public void SetFieldGameStatus(bool val)
		{
			this.FieldGameStatus = val;
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06005E9E RID: 24222 RVA: 0x0011E746 File Offset: 0x0011C946
		// (set) Token: 0x06005E9F RID: 24223 RVA: 0x0011E74E File Offset: 0x0011C94E
		public bool FieldGameAccounts
		{
			get
			{
				return this._FieldGameAccounts;
			}
			set
			{
				this._FieldGameAccounts = value;
				this.HasFieldGameAccounts = true;
			}
		}

		// Token: 0x06005EA0 RID: 24224 RVA: 0x0011E75E File Offset: 0x0011C95E
		public void SetFieldGameAccounts(bool val)
		{
			this.FieldGameAccounts = val;
		}

		// Token: 0x06005EA1 RID: 24225 RVA: 0x0011E768 File Offset: 0x0011C968
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAllFields)
			{
				num ^= this.AllFields.GetHashCode();
			}
			if (this.HasFieldAccountLevelInfo)
			{
				num ^= this.FieldAccountLevelInfo.GetHashCode();
			}
			if (this.HasFieldPrivacyInfo)
			{
				num ^= this.FieldPrivacyInfo.GetHashCode();
			}
			if (this.HasFieldParentalControlInfo)
			{
				num ^= this.FieldParentalControlInfo.GetHashCode();
			}
			if (this.HasFieldGameLevelInfo)
			{
				num ^= this.FieldGameLevelInfo.GetHashCode();
			}
			if (this.HasFieldGameStatus)
			{
				num ^= this.FieldGameStatus.GetHashCode();
			}
			if (this.HasFieldGameAccounts)
			{
				num ^= this.FieldGameAccounts.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005EA2 RID: 24226 RVA: 0x0011E834 File Offset: 0x0011CA34
		public override bool Equals(object obj)
		{
			AccountFieldOptions accountFieldOptions = obj as AccountFieldOptions;
			return accountFieldOptions != null && this.HasAllFields == accountFieldOptions.HasAllFields && (!this.HasAllFields || this.AllFields.Equals(accountFieldOptions.AllFields)) && this.HasFieldAccountLevelInfo == accountFieldOptions.HasFieldAccountLevelInfo && (!this.HasFieldAccountLevelInfo || this.FieldAccountLevelInfo.Equals(accountFieldOptions.FieldAccountLevelInfo)) && this.HasFieldPrivacyInfo == accountFieldOptions.HasFieldPrivacyInfo && (!this.HasFieldPrivacyInfo || this.FieldPrivacyInfo.Equals(accountFieldOptions.FieldPrivacyInfo)) && this.HasFieldParentalControlInfo == accountFieldOptions.HasFieldParentalControlInfo && (!this.HasFieldParentalControlInfo || this.FieldParentalControlInfo.Equals(accountFieldOptions.FieldParentalControlInfo)) && this.HasFieldGameLevelInfo == accountFieldOptions.HasFieldGameLevelInfo && (!this.HasFieldGameLevelInfo || this.FieldGameLevelInfo.Equals(accountFieldOptions.FieldGameLevelInfo)) && this.HasFieldGameStatus == accountFieldOptions.HasFieldGameStatus && (!this.HasFieldGameStatus || this.FieldGameStatus.Equals(accountFieldOptions.FieldGameStatus)) && this.HasFieldGameAccounts == accountFieldOptions.HasFieldGameAccounts && (!this.HasFieldGameAccounts || this.FieldGameAccounts.Equals(accountFieldOptions.FieldGameAccounts));
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06005EA3 RID: 24227 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005EA4 RID: 24228 RVA: 0x0011E990 File Offset: 0x0011CB90
		public static AccountFieldOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountFieldOptions>(bs, 0, -1);
		}

		// Token: 0x06005EA5 RID: 24229 RVA: 0x0011E99A File Offset: 0x0011CB9A
		public void Deserialize(Stream stream)
		{
			AccountFieldOptions.Deserialize(stream, this);
		}

		// Token: 0x06005EA6 RID: 24230 RVA: 0x0011E9A4 File Offset: 0x0011CBA4
		public static AccountFieldOptions Deserialize(Stream stream, AccountFieldOptions instance)
		{
			return AccountFieldOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005EA7 RID: 24231 RVA: 0x0011E9B0 File Offset: 0x0011CBB0
		public static AccountFieldOptions DeserializeLengthDelimited(Stream stream)
		{
			AccountFieldOptions accountFieldOptions = new AccountFieldOptions();
			AccountFieldOptions.DeserializeLengthDelimited(stream, accountFieldOptions);
			return accountFieldOptions;
		}

		// Token: 0x06005EA8 RID: 24232 RVA: 0x0011E9CC File Offset: 0x0011CBCC
		public static AccountFieldOptions DeserializeLengthDelimited(Stream stream, AccountFieldOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountFieldOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06005EA9 RID: 24233 RVA: 0x0011E9F4 File Offset: 0x0011CBF4
		public static AccountFieldOptions Deserialize(Stream stream, AccountFieldOptions instance, long limit)
		{
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
					if (num <= 24)
					{
						if (num == 8)
						{
							instance.AllFields = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 16)
						{
							instance.FieldAccountLevelInfo = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 24)
						{
							instance.FieldPrivacyInfo = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else if (num <= 48)
					{
						if (num == 32)
						{
							instance.FieldParentalControlInfo = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 48)
						{
							instance.FieldGameLevelInfo = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.FieldGameStatus = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 64)
						{
							instance.FieldGameAccounts = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005EAA RID: 24234 RVA: 0x0011EB19 File Offset: 0x0011CD19
		public void Serialize(Stream stream)
		{
			AccountFieldOptions.Serialize(stream, this);
		}

		// Token: 0x06005EAB RID: 24235 RVA: 0x0011EB24 File Offset: 0x0011CD24
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

		// Token: 0x06005EAC RID: 24236 RVA: 0x0011EBF4 File Offset: 0x0011CDF4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAllFields)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFieldAccountLevelInfo)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFieldPrivacyInfo)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFieldParentalControlInfo)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFieldGameLevelInfo)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFieldGameStatus)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFieldGameAccounts)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001D0F RID: 7439
		public bool HasAllFields;

		// Token: 0x04001D10 RID: 7440
		private bool _AllFields;

		// Token: 0x04001D11 RID: 7441
		public bool HasFieldAccountLevelInfo;

		// Token: 0x04001D12 RID: 7442
		private bool _FieldAccountLevelInfo;

		// Token: 0x04001D13 RID: 7443
		public bool HasFieldPrivacyInfo;

		// Token: 0x04001D14 RID: 7444
		private bool _FieldPrivacyInfo;

		// Token: 0x04001D15 RID: 7445
		public bool HasFieldParentalControlInfo;

		// Token: 0x04001D16 RID: 7446
		private bool _FieldParentalControlInfo;

		// Token: 0x04001D17 RID: 7447
		public bool HasFieldGameLevelInfo;

		// Token: 0x04001D18 RID: 7448
		private bool _FieldGameLevelInfo;

		// Token: 0x04001D19 RID: 7449
		public bool HasFieldGameStatus;

		// Token: 0x04001D1A RID: 7450
		private bool _FieldGameStatus;

		// Token: 0x04001D1B RID: 7451
		public bool HasFieldGameAccounts;

		// Token: 0x04001D1C RID: 7452
		private bool _FieldGameAccounts;
	}
}
