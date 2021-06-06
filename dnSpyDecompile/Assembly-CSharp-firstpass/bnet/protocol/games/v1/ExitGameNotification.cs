using System;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.games.v2;
using bnet.protocol.games.v2.Types;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200039D RID: 925
	public class ExitGameNotification : IProtoBuf
	{
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06003B84 RID: 15236 RVA: 0x000C060F File Offset: 0x000BE80F
		// (set) Token: 0x06003B85 RID: 15237 RVA: 0x000C0617 File Offset: 0x000BE817
		public GameHandle GameHandle
		{
			get
			{
				return this._GameHandle;
			}
			set
			{
				this._GameHandle = value;
				this.HasGameHandle = (value != null);
			}
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x000C062A File Offset: 0x000BE82A
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06003B87 RID: 15239 RVA: 0x000C0633 File Offset: 0x000BE833
		// (set) Token: 0x06003B88 RID: 15240 RVA: 0x000C063B File Offset: 0x000BE83B
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

		// Token: 0x06003B89 RID: 15241 RVA: 0x000C064E File Offset: 0x000BE84E
		public void SetGameAccount(GameAccountHandle val)
		{
			this.GameAccount = val;
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06003B8A RID: 15242 RVA: 0x000C0657 File Offset: 0x000BE857
		// (set) Token: 0x06003B8B RID: 15243 RVA: 0x000C065F File Offset: 0x000BE85F
		public PlayerLeaveReason Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000C066F File Offset: 0x000BE86F
		public void SetReason(PlayerLeaveReason val)
		{
			this.Reason = val;
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x000C0678 File Offset: 0x000BE878
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			if (this.HasGameAccount)
			{
				num ^= this.GameAccount.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x000C06E0 File Offset: 0x000BE8E0
		public override bool Equals(object obj)
		{
			ExitGameNotification exitGameNotification = obj as ExitGameNotification;
			return exitGameNotification != null && this.HasGameHandle == exitGameNotification.HasGameHandle && (!this.HasGameHandle || this.GameHandle.Equals(exitGameNotification.GameHandle)) && this.HasGameAccount == exitGameNotification.HasGameAccount && (!this.HasGameAccount || this.GameAccount.Equals(exitGameNotification.GameAccount)) && this.HasReason == exitGameNotification.HasReason && (!this.HasReason || this.Reason.Equals(exitGameNotification.Reason));
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06003B8F RID: 15247 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x000C0789 File Offset: 0x000BE989
		public static ExitGameNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ExitGameNotification>(bs, 0, -1);
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x000C0793 File Offset: 0x000BE993
		public void Deserialize(Stream stream)
		{
			ExitGameNotification.Deserialize(stream, this);
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x000C079D File Offset: 0x000BE99D
		public static ExitGameNotification Deserialize(Stream stream, ExitGameNotification instance)
		{
			return ExitGameNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x000C07A8 File Offset: 0x000BE9A8
		public static ExitGameNotification DeserializeLengthDelimited(Stream stream)
		{
			ExitGameNotification exitGameNotification = new ExitGameNotification();
			ExitGameNotification.DeserializeLengthDelimited(stream, exitGameNotification);
			return exitGameNotification;
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x000C07C4 File Offset: 0x000BE9C4
		public static ExitGameNotification DeserializeLengthDelimited(Stream stream, ExitGameNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ExitGameNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x000C07EC File Offset: 0x000BE9EC
		public static ExitGameNotification Deserialize(Stream stream, ExitGameNotification instance, long limit)
		{
			instance.Reason = PlayerLeaveReason.PLAYER_LEAVE_REASON_PLAYER_REMOVED_BY_GAME_SERVER;
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
						if (num != 24)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.Reason = (PlayerLeaveReason)ProtocolParser.ReadUInt64(stream);
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
				else if (instance.GameHandle == null)
				{
					instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003B96 RID: 15254 RVA: 0x000C08DC File Offset: 0x000BEADC
		public void Serialize(Stream stream)
		{
			ExitGameNotification.Serialize(stream, this);
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x000C08E8 File Offset: 0x000BEAE8
		public static void Serialize(Stream stream, ExitGameNotification instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasGameAccount)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Reason));
			}
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x000C096C File Offset: 0x000BEB6C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize = this.GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasGameAccount)
			{
				num += 1U;
				uint serializedSize2 = this.GameAccount.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Reason));
			}
			return num;
		}

		// Token: 0x0400157A RID: 5498
		public bool HasGameHandle;

		// Token: 0x0400157B RID: 5499
		private GameHandle _GameHandle;

		// Token: 0x0400157C RID: 5500
		public bool HasGameAccount;

		// Token: 0x0400157D RID: 5501
		private GameAccountHandle _GameAccount;

		// Token: 0x0400157E RID: 5502
		public bool HasReason;

		// Token: 0x0400157F RID: 5503
		private PlayerLeaveReason _Reason;
	}
}
