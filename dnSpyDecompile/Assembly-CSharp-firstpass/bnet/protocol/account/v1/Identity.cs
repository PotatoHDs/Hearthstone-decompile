using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000525 RID: 1317
	public class Identity : IProtoBuf
	{
		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06005E0F RID: 24079 RVA: 0x0011D181 File Offset: 0x0011B381
		// (set) Token: 0x06005E10 RID: 24080 RVA: 0x0011D189 File Offset: 0x0011B389
		public AccountId Account
		{
			get
			{
				return this._Account;
			}
			set
			{
				this._Account = value;
				this.HasAccount = (value != null);
			}
		}

		// Token: 0x06005E11 RID: 24081 RVA: 0x0011D19C File Offset: 0x0011B39C
		public void SetAccount(AccountId val)
		{
			this.Account = val;
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06005E12 RID: 24082 RVA: 0x0011D1A5 File Offset: 0x0011B3A5
		// (set) Token: 0x06005E13 RID: 24083 RVA: 0x0011D1AD File Offset: 0x0011B3AD
		public GameAccountHandle GameAccount
		{
			get
			{
				return this._GameAccount;
			}
			set
			{
				this._GameAccount = value;
				this.HasGameAccount = (value != null);
			}
		}

		// Token: 0x06005E14 RID: 24084 RVA: 0x0011D1C0 File Offset: 0x0011B3C0
		public void SetGameAccount(GameAccountHandle val)
		{
			this.GameAccount = val;
		}

		// Token: 0x06005E15 RID: 24085 RVA: 0x0011D1CC File Offset: 0x0011B3CC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccount)
			{
				num ^= this.Account.GetHashCode();
			}
			if (this.HasGameAccount)
			{
				num ^= this.GameAccount.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005E16 RID: 24086 RVA: 0x0011D214 File Offset: 0x0011B414
		public override bool Equals(object obj)
		{
			Identity identity = obj as Identity;
			return identity != null && this.HasAccount == identity.HasAccount && (!this.HasAccount || this.Account.Equals(identity.Account)) && this.HasGameAccount == identity.HasGameAccount && (!this.HasGameAccount || this.GameAccount.Equals(identity.GameAccount));
		}

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x06005E17 RID: 24087 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005E18 RID: 24088 RVA: 0x0011D284 File Offset: 0x0011B484
		public static Identity ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Identity>(bs, 0, -1);
		}

		// Token: 0x06005E19 RID: 24089 RVA: 0x0011D28E File Offset: 0x0011B48E
		public void Deserialize(Stream stream)
		{
			Identity.Deserialize(stream, this);
		}

		// Token: 0x06005E1A RID: 24090 RVA: 0x0011D298 File Offset: 0x0011B498
		public static Identity Deserialize(Stream stream, Identity instance)
		{
			return Identity.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005E1B RID: 24091 RVA: 0x0011D2A4 File Offset: 0x0011B4A4
		public static Identity DeserializeLengthDelimited(Stream stream)
		{
			Identity identity = new Identity();
			Identity.DeserializeLengthDelimited(stream, identity);
			return identity;
		}

		// Token: 0x06005E1C RID: 24092 RVA: 0x0011D2C0 File Offset: 0x0011B4C0
		public static Identity DeserializeLengthDelimited(Stream stream, Identity instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Identity.Deserialize(stream, instance, num);
		}

		// Token: 0x06005E1D RID: 24093 RVA: 0x0011D2E8 File Offset: 0x0011B4E8
		public static Identity Deserialize(Stream stream, Identity instance, long limit)
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
					else if (instance.GameAccount == null)
					{
						instance.GameAccount = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.GameAccount);
					}
				}
				else if (instance.Account == null)
				{
					instance.Account = AccountId.DeserializeLengthDelimited(stream);
				}
				else
				{
					AccountId.DeserializeLengthDelimited(stream, instance.Account);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005E1E RID: 24094 RVA: 0x0011D3BA File Offset: 0x0011B5BA
		public void Serialize(Stream stream)
		{
			Identity.Serialize(stream, this);
		}

		// Token: 0x06005E1F RID: 24095 RVA: 0x0011D3C4 File Offset: 0x0011B5C4
		public static void Serialize(Stream stream, Identity instance)
		{
			if (instance.HasAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Account.GetSerializedSize());
				AccountId.Serialize(stream, instance.Account);
			}
			if (instance.HasGameAccount)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
		}

		// Token: 0x06005E20 RID: 24096 RVA: 0x0011D42C File Offset: 0x0011B62C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccount)
			{
				num += 1U;
				uint serializedSize = this.Account.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasGameAccount)
			{
				num += 1U;
				uint serializedSize2 = this.GameAccount.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001CF2 RID: 7410
		public bool HasAccount;

		// Token: 0x04001CF3 RID: 7411
		private AccountId _Account;

		// Token: 0x04001CF4 RID: 7412
		public bool HasGameAccount;

		// Token: 0x04001CF5 RID: 7413
		private GameAccountHandle _GameAccount;
	}
}
