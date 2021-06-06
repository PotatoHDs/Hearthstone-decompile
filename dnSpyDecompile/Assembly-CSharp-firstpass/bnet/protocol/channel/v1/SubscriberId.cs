using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	// Token: 0x020004DA RID: 1242
	public class SubscriberId : IProtoBuf
	{
		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x060057A5 RID: 22437 RVA: 0x0010CE56 File Offset: 0x0010B056
		// (set) Token: 0x060057A6 RID: 22438 RVA: 0x0010CE5E File Offset: 0x0010B05E
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

		// Token: 0x060057A7 RID: 22439 RVA: 0x0010CE71 File Offset: 0x0010B071
		public void SetAccount(AccountId val)
		{
			this.Account = val;
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x060057A8 RID: 22440 RVA: 0x0010CE7A File Offset: 0x0010B07A
		// (set) Token: 0x060057A9 RID: 22441 RVA: 0x0010CE82 File Offset: 0x0010B082
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

		// Token: 0x060057AA RID: 22442 RVA: 0x0010CE95 File Offset: 0x0010B095
		public void SetGameAccount(GameAccountHandle val)
		{
			this.GameAccount = val;
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x060057AB RID: 22443 RVA: 0x0010CE9E File Offset: 0x0010B09E
		// (set) Token: 0x060057AC RID: 22444 RVA: 0x0010CEA6 File Offset: 0x0010B0A6
		public ProcessId Process
		{
			get
			{
				return this._Process;
			}
			set
			{
				this._Process = value;
				this.HasProcess = (value != null);
			}
		}

		// Token: 0x060057AD RID: 22445 RVA: 0x0010CEB9 File Offset: 0x0010B0B9
		public void SetProcess(ProcessId val)
		{
			this.Process = val;
		}

		// Token: 0x060057AE RID: 22446 RVA: 0x0010CEC4 File Offset: 0x0010B0C4
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
			if (this.HasProcess)
			{
				num ^= this.Process.GetHashCode();
			}
			return num;
		}

		// Token: 0x060057AF RID: 22447 RVA: 0x0010CF20 File Offset: 0x0010B120
		public override bool Equals(object obj)
		{
			SubscriberId subscriberId = obj as SubscriberId;
			return subscriberId != null && this.HasAccount == subscriberId.HasAccount && (!this.HasAccount || this.Account.Equals(subscriberId.Account)) && this.HasGameAccount == subscriberId.HasGameAccount && (!this.HasGameAccount || this.GameAccount.Equals(subscriberId.GameAccount)) && this.HasProcess == subscriberId.HasProcess && (!this.HasProcess || this.Process.Equals(subscriberId.Process));
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x060057B0 RID: 22448 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060057B1 RID: 22449 RVA: 0x0010CFBB File Offset: 0x0010B1BB
		public static SubscriberId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscriberId>(bs, 0, -1);
		}

		// Token: 0x060057B2 RID: 22450 RVA: 0x0010CFC5 File Offset: 0x0010B1C5
		public void Deserialize(Stream stream)
		{
			SubscriberId.Deserialize(stream, this);
		}

		// Token: 0x060057B3 RID: 22451 RVA: 0x0010CFCF File Offset: 0x0010B1CF
		public static SubscriberId Deserialize(Stream stream, SubscriberId instance)
		{
			return SubscriberId.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060057B4 RID: 22452 RVA: 0x0010CFDC File Offset: 0x0010B1DC
		public static SubscriberId DeserializeLengthDelimited(Stream stream)
		{
			SubscriberId subscriberId = new SubscriberId();
			SubscriberId.DeserializeLengthDelimited(stream, subscriberId);
			return subscriberId;
		}

		// Token: 0x060057B5 RID: 22453 RVA: 0x0010CFF8 File Offset: 0x0010B1F8
		public static SubscriberId DeserializeLengthDelimited(Stream stream, SubscriberId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscriberId.Deserialize(stream, instance, num);
		}

		// Token: 0x060057B6 RID: 22454 RVA: 0x0010D020 File Offset: 0x0010B220
		public static SubscriberId Deserialize(Stream stream, SubscriberId instance, long limit)
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
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.Process == null)
						{
							instance.Process = ProcessId.DeserializeLengthDelimited(stream);
						}
						else
						{
							ProcessId.DeserializeLengthDelimited(stream, instance.Process);
						}
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

		// Token: 0x060057B7 RID: 22455 RVA: 0x0010D122 File Offset: 0x0010B322
		public void Serialize(Stream stream)
		{
			SubscriberId.Serialize(stream, this);
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x0010D12C File Offset: 0x0010B32C
		public static void Serialize(Stream stream, SubscriberId instance)
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
			if (instance.HasProcess)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Process.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Process);
			}
		}

		// Token: 0x060057B9 RID: 22457 RVA: 0x0010D1C0 File Offset: 0x0010B3C0
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
			if (this.HasProcess)
			{
				num += 1U;
				uint serializedSize3 = this.Process.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x04001B85 RID: 7045
		public bool HasAccount;

		// Token: 0x04001B86 RID: 7046
		private AccountId _Account;

		// Token: 0x04001B87 RID: 7047
		public bool HasGameAccount;

		// Token: 0x04001B88 RID: 7048
		private GameAccountHandle _GameAccount;

		// Token: 0x04001B89 RID: 7049
		public bool HasProcess;

		// Token: 0x04001B8A RID: 7050
		private ProcessId _Process;
	}
}
