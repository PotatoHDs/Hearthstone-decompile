using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x02000125 RID: 293
	public class PlayerIdentity : IProtoBuf
	{
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x000431B2 File Offset: 0x000413B2
		// (set) Token: 0x06001346 RID: 4934 RVA: 0x000431BA File Offset: 0x000413BA
		public long PlayerId { get; set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x000431C3 File Offset: 0x000413C3
		// (set) Token: 0x06001348 RID: 4936 RVA: 0x000431CB File Offset: 0x000413CB
		public BnetId GameAccount
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

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x000431DE File Offset: 0x000413DE
		// (set) Token: 0x0600134A RID: 4938 RVA: 0x000431E6 File Offset: 0x000413E6
		public BnetId Account
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

		// Token: 0x0600134B RID: 4939 RVA: 0x000431FC File Offset: 0x000413FC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.PlayerId.GetHashCode();
			if (this.HasGameAccount)
			{
				num ^= this.GameAccount.GetHashCode();
			}
			if (this.HasAccount)
			{
				num ^= this.Account.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00043254 File Offset: 0x00041454
		public override bool Equals(object obj)
		{
			PlayerIdentity playerIdentity = obj as PlayerIdentity;
			return playerIdentity != null && this.PlayerId.Equals(playerIdentity.PlayerId) && this.HasGameAccount == playerIdentity.HasGameAccount && (!this.HasGameAccount || this.GameAccount.Equals(playerIdentity.GameAccount)) && this.HasAccount == playerIdentity.HasAccount && (!this.HasAccount || this.Account.Equals(playerIdentity.Account));
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x000432DC File Offset: 0x000414DC
		public void Deserialize(Stream stream)
		{
			PlayerIdentity.Deserialize(stream, this);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x000432E6 File Offset: 0x000414E6
		public static PlayerIdentity Deserialize(Stream stream, PlayerIdentity instance)
		{
			return PlayerIdentity.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x000432F4 File Offset: 0x000414F4
		public static PlayerIdentity DeserializeLengthDelimited(Stream stream)
		{
			PlayerIdentity playerIdentity = new PlayerIdentity();
			PlayerIdentity.DeserializeLengthDelimited(stream, playerIdentity);
			return playerIdentity;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00043310 File Offset: 0x00041510
		public static PlayerIdentity DeserializeLengthDelimited(Stream stream, PlayerIdentity instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerIdentity.Deserialize(stream, instance, num);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00043338 File Offset: 0x00041538
		public static PlayerIdentity Deserialize(Stream stream, PlayerIdentity instance, long limit)
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
				else if (num != 8)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.Account == null)
						{
							instance.Account = BnetId.DeserializeLengthDelimited(stream);
						}
						else
						{
							BnetId.DeserializeLengthDelimited(stream, instance.Account);
						}
					}
					else if (instance.GameAccount == null)
					{
						instance.GameAccount = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.GameAccount);
					}
				}
				else
				{
					instance.PlayerId = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0004341F File Offset: 0x0004161F
		public void Serialize(Stream stream)
		{
			PlayerIdentity.Serialize(stream, this);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00043428 File Offset: 0x00041628
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

		// Token: 0x06001354 RID: 4948 RVA: 0x000434A4 File Offset: 0x000416A4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.PlayerId);
			if (this.HasGameAccount)
			{
				num += 1U;
				uint serializedSize = this.GameAccount.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasAccount)
			{
				num += 1U;
				uint serializedSize2 = this.Account.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1U;
		}

		// Token: 0x040005F4 RID: 1524
		public bool HasGameAccount;

		// Token: 0x040005F5 RID: 1525
		private BnetId _GameAccount;

		// Token: 0x040005F6 RID: 1526
		public bool HasAccount;

		// Token: 0x040005F7 RID: 1527
		private BnetId _Account;
	}
}
