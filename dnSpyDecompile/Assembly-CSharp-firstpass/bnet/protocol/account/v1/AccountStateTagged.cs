using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200053B RID: 1339
	public class AccountStateTagged : IProtoBuf
	{
		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x0600609D RID: 24733 RVA: 0x00124821 File Offset: 0x00122A21
		// (set) Token: 0x0600609E RID: 24734 RVA: 0x00124829 File Offset: 0x00122A29
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

		// Token: 0x0600609F RID: 24735 RVA: 0x0012483C File Offset: 0x00122A3C
		public void SetAccountState(AccountState val)
		{
			this.AccountState = val;
		}

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x060060A0 RID: 24736 RVA: 0x00124845 File Offset: 0x00122A45
		// (set) Token: 0x060060A1 RID: 24737 RVA: 0x0012484D File Offset: 0x00122A4D
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

		// Token: 0x060060A2 RID: 24738 RVA: 0x00124860 File Offset: 0x00122A60
		public void SetAccountTags(AccountFieldTags val)
		{
			this.AccountTags = val;
		}

		// Token: 0x060060A3 RID: 24739 RVA: 0x0012486C File Offset: 0x00122A6C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountState)
			{
				num ^= this.AccountState.GetHashCode();
			}
			if (this.HasAccountTags)
			{
				num ^= this.AccountTags.GetHashCode();
			}
			return num;
		}

		// Token: 0x060060A4 RID: 24740 RVA: 0x001248B4 File Offset: 0x00122AB4
		public override bool Equals(object obj)
		{
			AccountStateTagged accountStateTagged = obj as AccountStateTagged;
			return accountStateTagged != null && this.HasAccountState == accountStateTagged.HasAccountState && (!this.HasAccountState || this.AccountState.Equals(accountStateTagged.AccountState)) && this.HasAccountTags == accountStateTagged.HasAccountTags && (!this.HasAccountTags || this.AccountTags.Equals(accountStateTagged.AccountTags));
		}

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x060060A5 RID: 24741 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060060A6 RID: 24742 RVA: 0x00124924 File Offset: 0x00122B24
		public static AccountStateTagged ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountStateTagged>(bs, 0, -1);
		}

		// Token: 0x060060A7 RID: 24743 RVA: 0x0012492E File Offset: 0x00122B2E
		public void Deserialize(Stream stream)
		{
			AccountStateTagged.Deserialize(stream, this);
		}

		// Token: 0x060060A8 RID: 24744 RVA: 0x00124938 File Offset: 0x00122B38
		public static AccountStateTagged Deserialize(Stream stream, AccountStateTagged instance)
		{
			return AccountStateTagged.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060060A9 RID: 24745 RVA: 0x00124944 File Offset: 0x00122B44
		public static AccountStateTagged DeserializeLengthDelimited(Stream stream)
		{
			AccountStateTagged accountStateTagged = new AccountStateTagged();
			AccountStateTagged.DeserializeLengthDelimited(stream, accountStateTagged);
			return accountStateTagged;
		}

		// Token: 0x060060AA RID: 24746 RVA: 0x00124960 File Offset: 0x00122B60
		public static AccountStateTagged DeserializeLengthDelimited(Stream stream, AccountStateTagged instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountStateTagged.Deserialize(stream, instance, num);
		}

		// Token: 0x060060AB RID: 24747 RVA: 0x00124988 File Offset: 0x00122B88
		public static AccountStateTagged Deserialize(Stream stream, AccountStateTagged instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.AccountTags == null)
					{
						instance.AccountTags = AccountFieldTags.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountFieldTags.DeserializeLengthDelimited(stream, instance.AccountTags);
					}
				}
				else if (instance.AccountState == null)
				{
					instance.AccountState = AccountState.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountState.DeserializeLengthDelimited(stream, instance.AccountState);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060060AC RID: 24748 RVA: 0x00124A5A File Offset: 0x00122C5A
		public void Serialize(Stream stream)
		{
			AccountStateTagged.Serialize(stream, this);
		}

		// Token: 0x060060AD RID: 24749 RVA: 0x00124A64 File Offset: 0x00122C64
		public static void Serialize(Stream stream, AccountStateTagged instance)
		{
			if (instance.HasAccountState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountState.GetSerializedSize());
				AccountState.Serialize(stream, instance.AccountState);
			}
			if (instance.HasAccountTags)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.AccountTags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.AccountTags);
			}
		}

		// Token: 0x060060AE RID: 24750 RVA: 0x00124ACC File Offset: 0x00122CCC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccountState)
			{
				num += 1U;
				uint serializedSize = this.AccountState.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasAccountTags)
			{
				num += 1U;
				uint serializedSize2 = this.AccountTags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001DBF RID: 7615
		public bool HasAccountState;

		// Token: 0x04001DC0 RID: 7616
		private AccountState _AccountState;

		// Token: 0x04001DC1 RID: 7617
		public bool HasAccountTags;

		// Token: 0x04001DC2 RID: 7618
		private AccountFieldTags _AccountTags;
	}
}
