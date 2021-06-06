using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200051D RID: 1309
	public class AccountStateNotification : IProtoBuf
	{
		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x06005D5B RID: 23899 RVA: 0x0011B568 File Offset: 0x00119768
		// (set) Token: 0x06005D5C RID: 23900 RVA: 0x0011B570 File Offset: 0x00119770
		public AccountState AccountState
		{
			get
			{
				return this._AccountState;
			}
			set
			{
				this._AccountState = value;
				this.HasAccountState = (value != null);
			}
		}

		// Token: 0x06005D5D RID: 23901 RVA: 0x0011B583 File Offset: 0x00119783
		public void SetAccountState(AccountState val)
		{
			this.AccountState = val;
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06005D5E RID: 23902 RVA: 0x0011B58C File Offset: 0x0011978C
		// (set) Token: 0x06005D5F RID: 23903 RVA: 0x0011B594 File Offset: 0x00119794
		public ulong SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = true;
			}
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x0011B5A4 File Offset: 0x001197A4
		public void SetSubscriberId(ulong val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06005D61 RID: 23905 RVA: 0x0011B5AD File Offset: 0x001197AD
		// (set) Token: 0x06005D62 RID: 23906 RVA: 0x0011B5B5 File Offset: 0x001197B5
		public AccountFieldTags AccountTags
		{
			get
			{
				return this._AccountTags;
			}
			set
			{
				this._AccountTags = value;
				this.HasAccountTags = (value != null);
			}
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x0011B5C8 File Offset: 0x001197C8
		public void SetAccountTags(AccountFieldTags val)
		{
			this.AccountTags = val;
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x06005D64 RID: 23908 RVA: 0x0011B5D1 File Offset: 0x001197D1
		// (set) Token: 0x06005D65 RID: 23909 RVA: 0x0011B5D9 File Offset: 0x001197D9
		public bool SubscriptionCompleted
		{
			get
			{
				return this._SubscriptionCompleted;
			}
			set
			{
				this._SubscriptionCompleted = value;
				this.HasSubscriptionCompleted = true;
			}
		}

		// Token: 0x06005D66 RID: 23910 RVA: 0x0011B5E9 File Offset: 0x001197E9
		public void SetSubscriptionCompleted(bool val)
		{
			this.SubscriptionCompleted = val;
		}

		// Token: 0x06005D67 RID: 23911 RVA: 0x0011B5F4 File Offset: 0x001197F4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountState)
			{
				num ^= this.AccountState.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			if (this.HasAccountTags)
			{
				num ^= this.AccountTags.GetHashCode();
			}
			if (this.HasSubscriptionCompleted)
			{
				num ^= this.SubscriptionCompleted.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x0011B66C File Offset: 0x0011986C
		public override bool Equals(object obj)
		{
			AccountStateNotification accountStateNotification = obj as AccountStateNotification;
			return accountStateNotification != null && this.HasAccountState == accountStateNotification.HasAccountState && (!this.HasAccountState || this.AccountState.Equals(accountStateNotification.AccountState)) && this.HasSubscriberId == accountStateNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(accountStateNotification.SubscriberId)) && this.HasAccountTags == accountStateNotification.HasAccountTags && (!this.HasAccountTags || this.AccountTags.Equals(accountStateNotification.AccountTags)) && this.HasSubscriptionCompleted == accountStateNotification.HasSubscriptionCompleted && (!this.HasSubscriptionCompleted || this.SubscriptionCompleted.Equals(accountStateNotification.SubscriptionCompleted));
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x06005D69 RID: 23913 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x0011B738 File Offset: 0x00119938
		public static AccountStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountStateNotification>(bs, 0, -1);
		}

		// Token: 0x06005D6B RID: 23915 RVA: 0x0011B742 File Offset: 0x00119942
		public void Deserialize(Stream stream)
		{
			AccountStateNotification.Deserialize(stream, this);
		}

		// Token: 0x06005D6C RID: 23916 RVA: 0x0011B74C File Offset: 0x0011994C
		public static AccountStateNotification Deserialize(Stream stream, AccountStateNotification instance)
		{
			return AccountStateNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005D6D RID: 23917 RVA: 0x0011B758 File Offset: 0x00119958
		public static AccountStateNotification DeserializeLengthDelimited(Stream stream)
		{
			AccountStateNotification accountStateNotification = new AccountStateNotification();
			AccountStateNotification.DeserializeLengthDelimited(stream, accountStateNotification);
			return accountStateNotification;
		}

		// Token: 0x06005D6E RID: 23918 RVA: 0x0011B774 File Offset: 0x00119974
		public static AccountStateNotification DeserializeLengthDelimited(Stream stream, AccountStateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountStateNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x0011B79C File Offset: 0x0011999C
		public static AccountStateNotification Deserialize(Stream stream, AccountStateNotification instance, long limit)
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
					if (num <= 16)
					{
						if (num != 10)
						{
							if (num == 16)
							{
								instance.SubscriberId = ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (instance.AccountState == null)
							{
								instance.AccountState = AccountState.DeserializeLengthDelimited(stream);
								continue;
							}
							AccountState.DeserializeLengthDelimited(stream, instance.AccountState);
							continue;
						}
					}
					else if (num != 26)
					{
						if (num == 32)
						{
							instance.SubscriptionCompleted = ProtocolParser.ReadBool(stream);
							continue;
						}
					}
					else
					{
						if (instance.AccountTags == null)
						{
							instance.AccountTags = AccountFieldTags.DeserializeLengthDelimited(stream);
							continue;
						}
						AccountFieldTags.DeserializeLengthDelimited(stream, instance.AccountTags);
						continue;
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

		// Token: 0x06005D70 RID: 23920 RVA: 0x0011B8A7 File Offset: 0x00119AA7
		public void Serialize(Stream stream)
		{
			AccountStateNotification.Serialize(stream, this);
		}

		// Token: 0x06005D71 RID: 23921 RVA: 0x0011B8B0 File Offset: 0x00119AB0
		public static void Serialize(Stream stream, AccountStateNotification instance)
		{
			if (instance.HasAccountState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountState.GetSerializedSize());
				AccountState.Serialize(stream, instance.AccountState);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.SubscriberId);
			}
			if (instance.HasAccountTags)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AccountTags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.AccountTags);
			}
			if (instance.HasSubscriptionCompleted)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.SubscriptionCompleted);
			}
		}

		// Token: 0x06005D72 RID: 23922 RVA: 0x0011B950 File Offset: 0x00119B50
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccountState)
			{
				num += 1U;
				uint serializedSize = this.AccountState.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.SubscriberId);
			}
			if (this.HasAccountTags)
			{
				num += 1U;
				uint serializedSize2 = this.AccountTags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasSubscriptionCompleted)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001CC8 RID: 7368
		public bool HasAccountState;

		// Token: 0x04001CC9 RID: 7369
		private AccountState _AccountState;

		// Token: 0x04001CCA RID: 7370
		public bool HasSubscriberId;

		// Token: 0x04001CCB RID: 7371
		private ulong _SubscriberId;

		// Token: 0x04001CCC RID: 7372
		public bool HasAccountTags;

		// Token: 0x04001CCD RID: 7373
		private AccountFieldTags _AccountTags;

		// Token: 0x04001CCE RID: 7374
		public bool HasSubscriptionCompleted;

		// Token: 0x04001CCF RID: 7375
		private bool _SubscriptionCompleted;
	}
}
