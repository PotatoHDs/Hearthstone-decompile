using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004F2 RID: 1266
	public class AccountSettingsNotification : IProtoBuf
	{
		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x060059E2 RID: 23010 RVA: 0x00112D28 File Offset: 0x00110F28
		// (set) Token: 0x060059E3 RID: 23011 RVA: 0x00112D30 File Offset: 0x00110F30
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

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x060059E4 RID: 23012 RVA: 0x00112D28 File Offset: 0x00110F28
		public List<AccountLicense> LicensesList
		{
			get
			{
				return this._Licenses;
			}
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x060059E5 RID: 23013 RVA: 0x00112D39 File Offset: 0x00110F39
		public int LicensesCount
		{
			get
			{
				return this._Licenses.Count;
			}
		}

		// Token: 0x060059E6 RID: 23014 RVA: 0x00112D46 File Offset: 0x00110F46
		public void AddLicenses(AccountLicense val)
		{
			this._Licenses.Add(val);
		}

		// Token: 0x060059E7 RID: 23015 RVA: 0x00112D54 File Offset: 0x00110F54
		public void ClearLicenses()
		{
			this._Licenses.Clear();
		}

		// Token: 0x060059E8 RID: 23016 RVA: 0x00112D61 File Offset: 0x00110F61
		public void SetLicenses(List<AccountLicense> val)
		{
			this.Licenses = val;
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x060059E9 RID: 23017 RVA: 0x00112D6A File Offset: 0x00110F6A
		// (set) Token: 0x060059EA RID: 23018 RVA: 0x00112D72 File Offset: 0x00110F72
		public bool IsUsingRid
		{
			get
			{
				return this._IsUsingRid;
			}
			set
			{
				this._IsUsingRid = value;
				this.HasIsUsingRid = true;
			}
		}

		// Token: 0x060059EB RID: 23019 RVA: 0x00112D82 File Offset: 0x00110F82
		public void SetIsUsingRid(bool val)
		{
			this.IsUsingRid = val;
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x060059EC RID: 23020 RVA: 0x00112D8B File Offset: 0x00110F8B
		// (set) Token: 0x060059ED RID: 23021 RVA: 0x00112D93 File Offset: 0x00110F93
		public bool IsPlayingFromIgr
		{
			get
			{
				return this._IsPlayingFromIgr;
			}
			set
			{
				this._IsPlayingFromIgr = value;
				this.HasIsPlayingFromIgr = true;
			}
		}

		// Token: 0x060059EE RID: 23022 RVA: 0x00112DA3 File Offset: 0x00110FA3
		public void SetIsPlayingFromIgr(bool val)
		{
			this.IsPlayingFromIgr = val;
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x060059EF RID: 23023 RVA: 0x00112DAC File Offset: 0x00110FAC
		// (set) Token: 0x060059F0 RID: 23024 RVA: 0x00112DB4 File Offset: 0x00110FB4
		public bool CanReceiveVoice
		{
			get
			{
				return this._CanReceiveVoice;
			}
			set
			{
				this._CanReceiveVoice = value;
				this.HasCanReceiveVoice = true;
			}
		}

		// Token: 0x060059F1 RID: 23025 RVA: 0x00112DC4 File Offset: 0x00110FC4
		public void SetCanReceiveVoice(bool val)
		{
			this.CanReceiveVoice = val;
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x060059F2 RID: 23026 RVA: 0x00112DCD File Offset: 0x00110FCD
		// (set) Token: 0x060059F3 RID: 23027 RVA: 0x00112DD5 File Offset: 0x00110FD5
		public bool CanSendVoice
		{
			get
			{
				return this._CanSendVoice;
			}
			set
			{
				this._CanSendVoice = value;
				this.HasCanSendVoice = true;
			}
		}

		// Token: 0x060059F4 RID: 23028 RVA: 0x00112DE5 File Offset: 0x00110FE5
		public void SetCanSendVoice(bool val)
		{
			this.CanSendVoice = val;
		}

		// Token: 0x060059F5 RID: 23029 RVA: 0x00112DF0 File Offset: 0x00110FF0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (AccountLicense accountLicense in this.Licenses)
			{
				num ^= accountLicense.GetHashCode();
			}
			if (this.HasIsUsingRid)
			{
				num ^= this.IsUsingRid.GetHashCode();
			}
			if (this.HasIsPlayingFromIgr)
			{
				num ^= this.IsPlayingFromIgr.GetHashCode();
			}
			if (this.HasCanReceiveVoice)
			{
				num ^= this.CanReceiveVoice.GetHashCode();
			}
			if (this.HasCanSendVoice)
			{
				num ^= this.CanSendVoice.GetHashCode();
			}
			return num;
		}

		// Token: 0x060059F6 RID: 23030 RVA: 0x00112EB8 File Offset: 0x001110B8
		public override bool Equals(object obj)
		{
			AccountSettingsNotification accountSettingsNotification = obj as AccountSettingsNotification;
			if (accountSettingsNotification == null)
			{
				return false;
			}
			if (this.Licenses.Count != accountSettingsNotification.Licenses.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Licenses.Count; i++)
			{
				if (!this.Licenses[i].Equals(accountSettingsNotification.Licenses[i]))
				{
					return false;
				}
			}
			return this.HasIsUsingRid == accountSettingsNotification.HasIsUsingRid && (!this.HasIsUsingRid || this.IsUsingRid.Equals(accountSettingsNotification.IsUsingRid)) && this.HasIsPlayingFromIgr == accountSettingsNotification.HasIsPlayingFromIgr && (!this.HasIsPlayingFromIgr || this.IsPlayingFromIgr.Equals(accountSettingsNotification.IsPlayingFromIgr)) && this.HasCanReceiveVoice == accountSettingsNotification.HasCanReceiveVoice && (!this.HasCanReceiveVoice || this.CanReceiveVoice.Equals(accountSettingsNotification.CanReceiveVoice)) && this.HasCanSendVoice == accountSettingsNotification.HasCanSendVoice && (!this.HasCanSendVoice || this.CanSendVoice.Equals(accountSettingsNotification.CanSendVoice));
		}

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x060059F7 RID: 23031 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060059F8 RID: 23032 RVA: 0x00112FDB File Offset: 0x001111DB
		public static AccountSettingsNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountSettingsNotification>(bs, 0, -1);
		}

		// Token: 0x060059F9 RID: 23033 RVA: 0x00112FE5 File Offset: 0x001111E5
		public void Deserialize(Stream stream)
		{
			AccountSettingsNotification.Deserialize(stream, this);
		}

		// Token: 0x060059FA RID: 23034 RVA: 0x00112FEF File Offset: 0x001111EF
		public static AccountSettingsNotification Deserialize(Stream stream, AccountSettingsNotification instance)
		{
			return AccountSettingsNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060059FB RID: 23035 RVA: 0x00112FFC File Offset: 0x001111FC
		public static AccountSettingsNotification DeserializeLengthDelimited(Stream stream)
		{
			AccountSettingsNotification accountSettingsNotification = new AccountSettingsNotification();
			AccountSettingsNotification.DeserializeLengthDelimited(stream, accountSettingsNotification);
			return accountSettingsNotification;
		}

		// Token: 0x060059FC RID: 23036 RVA: 0x00113018 File Offset: 0x00111218
		public static AccountSettingsNotification DeserializeLengthDelimited(Stream stream, AccountSettingsNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountSettingsNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x060059FD RID: 23037 RVA: 0x00113040 File Offset: 0x00111240
		public static AccountSettingsNotification Deserialize(Stream stream, AccountSettingsNotification instance, long limit)
		{
			if (instance.Licenses == null)
			{
				instance.Licenses = new List<AccountLicense>();
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
					if (num <= 16)
					{
						if (num == 10)
						{
							instance.Licenses.Add(AccountLicense.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 16)
						{
							instance.IsUsingRid = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.IsPlayingFromIgr = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 32)
						{
							instance.CanReceiveVoice = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 40)
						{
							instance.CanSendVoice = ProtocolParser.ReadBool(stream);
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

		// Token: 0x060059FE RID: 23038 RVA: 0x00113142 File Offset: 0x00111342
		public void Serialize(Stream stream)
		{
			AccountSettingsNotification.Serialize(stream, this);
		}

		// Token: 0x060059FF RID: 23039 RVA: 0x0011314C File Offset: 0x0011134C
		public static void Serialize(Stream stream, AccountSettingsNotification instance)
		{
			if (instance.Licenses.Count > 0)
			{
				foreach (AccountLicense accountLicense in instance.Licenses)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, accountLicense.GetSerializedSize());
					AccountLicense.Serialize(stream, accountLicense);
				}
			}
			if (instance.HasIsUsingRid)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.IsUsingRid);
			}
			if (instance.HasIsPlayingFromIgr)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsPlayingFromIgr);
			}
			if (instance.HasCanReceiveVoice)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.CanReceiveVoice);
			}
			if (instance.HasCanSendVoice)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.CanSendVoice);
			}
		}

		// Token: 0x06005A00 RID: 23040 RVA: 0x00113234 File Offset: 0x00111434
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
			if (this.HasIsUsingRid)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasIsPlayingFromIgr)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanReceiveVoice)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasCanSendVoice)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001C0A RID: 7178
		private List<AccountLicense> _Licenses = new List<AccountLicense>();

		// Token: 0x04001C0B RID: 7179
		public bool HasIsUsingRid;

		// Token: 0x04001C0C RID: 7180
		private bool _IsUsingRid;

		// Token: 0x04001C0D RID: 7181
		public bool HasIsPlayingFromIgr;

		// Token: 0x04001C0E RID: 7182
		private bool _IsPlayingFromIgr;

		// Token: 0x04001C0F RID: 7183
		public bool HasCanReceiveVoice;

		// Token: 0x04001C10 RID: 7184
		private bool _CanReceiveVoice;

		// Token: 0x04001C11 RID: 7185
		public bool HasCanSendVoice;

		// Token: 0x04001C12 RID: 7186
		private bool _CanSendVoice;
	}
}
